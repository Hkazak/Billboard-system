﻿using Microsoft.EntityFrameworkCore;
using Persistence.ModelConfigurations;
using Persistence.Models;

namespace Persistence.Context;

public class BillboardContext : DbContext
{
    protected BillboardContext()
    {
    }

    public BillboardContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<Manager> Managers { get; set; } = default!;
    public DbSet<ManagerStatus> ManagerStatus { get; set; } = default!;
    public DbSet<Tariff> Tariffs { get; set; } = default!;
    public DbSet<Billboard> Billboards { get; set; } = default!;
    public DbSet<BillboardSurface> BillboardSurfaces { get; set; } = default!;
    public DbSet<GroupOfTariffs> GroupOfTariffs { get; set; } = default!;
    public DbSet<BillboardType> BillboardTypes { get; set; } = default!;
    public DbSet<ArchiveStatus> ArchiveStatusEnumerable { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderStatus> OrderStatusEnumerable { get; set; } = default!;
    public DbSet<Discount> Discounts { get; set; } = default!;
    public DbSet<PriceRule> PriceRules { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerStatusConfiguration());
        modelBuilder.ApplyConfiguration(new TariffConfiguration());
        modelBuilder.ApplyConfiguration(new BillboardConfiguration());
        modelBuilder.ApplyConfiguration(new GroupOfTariffsConfiguration());
        modelBuilder.ApplyConfiguration(new ArchiveStatusConfiguration());
        modelBuilder.ApplyConfiguration(new BillboardTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
        modelBuilder.ApplyConfiguration(new DiscountConfiguration());
        modelBuilder.ApplyConfiguration(new PriceRuleConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentStatusConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }
}