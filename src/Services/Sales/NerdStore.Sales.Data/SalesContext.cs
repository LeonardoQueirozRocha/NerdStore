using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Data;

public class SalesContext : DbContext, IUnitOfWork
{
    public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

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

        return await base.SaveChangesAsync() > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

        foreach (var relationship in modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.HasSequence<int>("MySequence").StartsAt(1000).IncrementsBy(1);
        base.OnModelCreating(modelBuilder);
    }
}