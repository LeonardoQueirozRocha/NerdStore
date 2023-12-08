using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Data.Mappings;

public class PaymentMapping : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.CreditCardName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder
            .Property(c => c.CreditCardNumber)
            .IsRequired()
            .HasColumnType("varchar(16)");

        builder
            .Property(c => c.CreditCardExpirationDate)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder
            .Property(c => c.CreditCardCvv)
            .IsRequired()
            .HasColumnType("varchar(4)");

        // 1 : 1 => Payment : Transaction
        builder
            .HasOne(c => c.Transaction)
            .WithOne(c => c.Payment);

        builder.ToTable("Payments");
    }
}