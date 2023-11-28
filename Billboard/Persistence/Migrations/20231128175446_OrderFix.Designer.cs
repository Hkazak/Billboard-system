﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BillboardContext))]
    [Migration("20231128175446_OrderFix")]
    partial class OrderFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BillboardDiscount", b =>
                {
                    b.Property<Guid>("BillboardsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DiscountsId")
                        .HasColumnType("uuid");

                    b.HasKey("BillboardsId", "DiscountsId");

                    b.HasIndex("DiscountsId");

                    b.ToTable("BillboardDiscount");
                });

            modelBuilder.Entity("GroupOfTariffsTariff", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TariffsId")
                        .HasColumnType("uuid");

                    b.HasKey("GroupsId", "TariffsId");

                    b.HasIndex("TariffsId");

                    b.ToTable("GroupOfTariffsTariff");
                });

            modelBuilder.Entity("Persistence.Models.ArchiveStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ArchiveStatusEnumerable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Status = "Archived"
                        },
                        new
                        {
                            Id = 0,
                            Status = "NonArchived"
                        });
                });

            modelBuilder.Entity("Persistence.Models.Billboard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("ArchiveStatusId")
                        .HasColumnType("integer");

                    b.Property<Guid>("BillboardSurfaceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<Guid>("GroupOfTariffsId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Height")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("Penalty")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Width")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveStatusId");

                    b.HasIndex("BillboardSurfaceId");

                    b.HasIndex("GroupOfTariffsId");

                    b.HasIndex("TypeId");

                    b.ToTable("Billboards");
                });

            modelBuilder.Entity("Persistence.Models.BillboardSurface", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Surface")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BillboardSurfaces");
                });

            modelBuilder.Entity("Persistence.Models.BillboardType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BillboardTypes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Type = "SingleSide"
                        },
                        new
                        {
                            Id = 1,
                            Type = "DoubleSide"
                        },
                        new
                        {
                            Id = 2,
                            Type = "TripleSide"
                        });
                });

            modelBuilder.Entity("Persistence.Models.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ArchiveStatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MinRentCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("SalesOf")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveStatusId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Persistence.Models.GroupOfTariffs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ArchiveStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveStatusId");

                    b.ToTable("GroupOfTariffs");
                });

            modelBuilder.Entity("Persistence.Models.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("StatusId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Persistence.Models.ManagerStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ManagerStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Status = "Active"
                        },
                        new
                        {
                            Id = 1,
                            Status = "Inactive"
                        });
                });

            modelBuilder.Entity("Persistence.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BillboardId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PenaltyPrice")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<decimal>("ProductPrice")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<decimal>("RentPrice")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BillboardId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TariffId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Persistence.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderStatusEnumerable");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Status = "Submitted"
                        },
                        new
                        {
                            Id = 1,
                            Status = "InProgress"
                        },
                        new
                        {
                            Id = 2,
                            Status = "Completed"
                        });
                });

            modelBuilder.Entity("Persistence.Models.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BillboardId")
                        .HasColumnType("uuid");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BillboardId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("Persistence.Models.PriceRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BillboardSurfaceId")
                        .HasColumnType("uuid");

                    b.Property<int>("BillboardTypeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("BillboardTypeId");

                    b.HasIndex(new[] { "BillboardSurfaceId", "BillboardTypeId" }, "UX_SurfaceId_TypeId")
                        .IsUnique();

                    b.ToTable("PriceRules");
                });

            modelBuilder.Entity("Persistence.Models.Tariff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ArchiveStatusId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("interval");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveStatusId");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("Persistence.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("64656322-9b5b-4245-8485-52fac5647c36"),
                            Email = "admin@Billboard.com",
                            Name = "Admin",
                            Password = "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Persistence.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "Administrator"
                        },
                        new
                        {
                            Id = 0,
                            Role = "Client"
                        });
                });

            modelBuilder.Entity("BillboardDiscount", b =>
                {
                    b.HasOne("Persistence.Models.Billboard", null)
                        .WithMany()
                        .HasForeignKey("BillboardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupOfTariffsTariff", b =>
                {
                    b.HasOne("Persistence.Models.GroupOfTariffs", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Tariff", null)
                        .WithMany()
                        .HasForeignKey("TariffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Persistence.Models.Billboard", b =>
                {
                    b.HasOne("Persistence.Models.ArchiveStatus", "ArchiveStatus")
                        .WithMany()
                        .HasForeignKey("ArchiveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.BillboardSurface", "BillboardSurface")
                        .WithMany()
                        .HasForeignKey("BillboardSurfaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.GroupOfTariffs", "GroupOfTariffs")
                        .WithMany()
                        .HasForeignKey("GroupOfTariffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.BillboardType", "BillboardType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchiveStatus");

                    b.Navigation("BillboardSurface");

                    b.Navigation("BillboardType");

                    b.Navigation("GroupOfTariffs");
                });

            modelBuilder.Entity("Persistence.Models.Discount", b =>
                {
                    b.HasOne("Persistence.Models.ArchiveStatus", "ArchiveStatus")
                        .WithMany()
                        .HasForeignKey("ArchiveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchiveStatus");
                });

            modelBuilder.Entity("Persistence.Models.GroupOfTariffs", b =>
                {
                    b.HasOne("Persistence.Models.ArchiveStatus", "ArchiveStatus")
                        .WithMany()
                        .HasForeignKey("ArchiveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchiveStatus");
                });

            modelBuilder.Entity("Persistence.Models.Manager", b =>
                {
                    b.HasOne("Persistence.Models.ManagerStatus", "ManagerStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManagerStatus");
                });

            modelBuilder.Entity("Persistence.Models.Order", b =>
                {
                    b.HasOne("Persistence.Models.Billboard", "Billboard")
                        .WithMany()
                        .HasForeignKey("BillboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("Persistence.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.Tariff", "SelectedTariff")
                        .WithMany()
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Billboard");

                    b.Navigation("Discount");

                    b.Navigation("OrderStatus");

                    b.Navigation("SelectedTariff");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Persistence.Models.Picture", b =>
                {
                    b.HasOne("Persistence.Models.Billboard", null)
                        .WithMany("Pictures")
                        .HasForeignKey("BillboardId");
                });

            modelBuilder.Entity("Persistence.Models.PriceRule", b =>
                {
                    b.HasOne("Persistence.Models.BillboardSurface", "BillboardSurface")
                        .WithMany()
                        .HasForeignKey("BillboardSurfaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.BillboardType", "BillboardType")
                        .WithMany()
                        .HasForeignKey("BillboardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillboardSurface");

                    b.Navigation("BillboardType");
                });

            modelBuilder.Entity("Persistence.Models.Tariff", b =>
                {
                    b.HasOne("Persistence.Models.ArchiveStatus", "ArchiveStatus")
                        .WithMany()
                        .HasForeignKey("ArchiveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArchiveStatus");
                });

            modelBuilder.Entity("Persistence.Models.User", b =>
                {
                    b.HasOne("Persistence.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Persistence.Models.Billboard", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Persistence.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
