﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ProductPrice).HasPrecision(12, 2);
        builder.HasOne(e => e.OrderStatus)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
        builder.HasMany(e => e.Discounts);
    }
}