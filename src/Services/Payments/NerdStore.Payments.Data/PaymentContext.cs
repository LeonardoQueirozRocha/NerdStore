using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Payments.Business.Models;
using NerdStore.Payments.Data.Extensions;

namespace NerdStore.Payments.Data;

public class PaymentContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public PaymentContext(
        DbContextOptions<PaymentContext> options,
        IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker
            .Entries()
            .Where(entry => entry.Entity
                .GetType()
                .GetProperty("RegistrationDate") is not null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("RegistrationDate").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("RegistrationDate").IsModified = false;
        }

        var success = await base.SaveChangesAsync() > 0;

        if (success) await _mediatorHandler.PublishEventsAsync(this);

        return success;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

        foreach (var relationship in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            
        base.OnModelCreating(modelBuilder);
    }
}