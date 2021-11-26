﻿// <auto-generated />
using System;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211126171510_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Coin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Coin");
                });

            modelBuilder.Entity("DataAccess.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<Guid>("CoinId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ValidationRuleId")
                        .HasColumnType("uuid");

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.HasIndex("CoinId");

                    b.HasIndex("ValidationRuleId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccess.Models.Validation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Validations");
                });

            modelBuilder.Entity("DataAccess.Models.ValidationRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Confirmations")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<float>("End")
                        .HasColumnType("real");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Start")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ValidationRules");
                });

            modelBuilder.Entity("DataAccess.Models.Order", b =>
                {
                    b.HasOne("DataAccess.Models.Coin", "Coin")
                        .WithMany("Orders")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.ValidationRule", "ValidationRule")
                        .WithMany()
                        .HasForeignKey("ValidationRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coin");

                    b.Navigation("ValidationRule");
                });

            modelBuilder.Entity("DataAccess.Models.Validation", b =>
                {
                    b.HasOne("DataAccess.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DataAccess.Models.Coin", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
