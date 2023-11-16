﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BillboardContext))]
    partial class BillboardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.ToTable("ArchiveStatus");

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

                    b.ToTable("BillboardType");
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
                            Id = new Guid("5e35a183-46c7-4325-816e-85be677ee13a"),
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

            modelBuilder.Entity("Persistence.Models.Picture", b =>
                {
                    b.HasOne("Persistence.Models.Billboard", null)
                        .WithMany("Pictures")
                        .HasForeignKey("BillboardId");
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
#pragma warning restore 612, 618
        }
    }
}
