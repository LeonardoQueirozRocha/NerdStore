﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStore.Payments.Data;

#nullable disable

namespace NerdStore.Payments.Data.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20231208022332_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NerdStore.Payments.Business.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreditCardCvv")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("CreditCardExpirationDate")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CreditCardName")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("NerdStore.Payments.Business.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("NerdStore.Payments.Business.Models.Transaction", b =>
                {
                    b.HasOne("NerdStore.Payments.Business.Models.Payment", "Payment")
                        .WithOne("Transaction")
                        .HasForeignKey("NerdStore.Payments.Business.Models.Transaction", "PaymentId")
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("NerdStore.Payments.Business.Models.Payment", b =>
                {
                    b.Navigation("Transaction");
                });
#pragma warning restore 612, 618
        }
    }
}