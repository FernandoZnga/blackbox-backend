﻿// <auto-generated />
using System;
using Blackbox.Server.DataConn;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blackbox.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190323022502_NewExchangeRate")]
    partial class NewExchangeRate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blackbox.Server.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Balance");

                    b.Property<int>("CcTypeId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CcTypeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("CcNumber");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("PinNumber");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Enee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<double>("BillAmount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Enee");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Compra");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Currency");

                    b.Property<double>("Venta");

                    b.HasKey("Id");

                    b.ToTable("Exchange");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Hondutel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<double>("BillAmount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Hondutel");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Sanaa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<double>("BillAmount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Sanaa");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<string>("AccountTypeName");

                    b.Property<double>("Amount");

                    b.Property<string>("AtmId");

                    b.Property<double>("BalanceAfter");

                    b.Property<double>("BalanceBefore");

                    b.Property<int>("BillingId");

                    b.Property<string>("BillingName");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("TxTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TxTypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.TxType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("TxTypes");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.__TextLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AtmId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("DesText");

                    b.Property<string>("Direction");

                    b.Property<string>("Md5IN");

                    b.Property<string>("Md5OUT");

                    b.Property<string>("Transaction");

                    b.Property<string>("XmlText");

                    b.HasKey("Id");

                    b.ToTable("__TextLogs");
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Account", b =>
                {
                    b.HasOne("Blackbox.Server.Domain.AccountType", "CcType")
                        .WithMany("Accounts")
                        .HasForeignKey("CcTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blackbox.Server.Domain.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blackbox.Server.Domain.CreditCard", b =>
                {
                    b.HasOne("Blackbox.Server.Domain.Account", "Account")
                        .WithOne("CreditCard")
                        .HasForeignKey("Blackbox.Server.Domain.CreditCard", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blackbox.Server.Domain.Transaction", b =>
                {
                    b.HasOne("Blackbox.Server.Domain.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blackbox.Server.Domain.TxType", "TxType")
                        .WithMany("Transactions")
                        .HasForeignKey("TxTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
