using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ProductPrice).HasPrecision(12, 2);
        builder.Property(e => e.RentPrice).HasPrecision(12, 2);
        builder.Property(e => e.PenaltyPrice).HasPrecision(12, 2);
        builder.HasOne(e => e.Billboard)
            .WithMany()
            .HasForeignKey(e => e.BillboardId);
        builder.HasOne(e => e.OrderStatus)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
        builder.HasOne(e => e.Discount)
            .WithMany()
            .HasForeignKey(e => e.DiscountId);
        builder.HasOne(e => e.SelectedTariff)
            .WithMany()
            .HasForeignKey(e => e.TariffId);
        builder.HasOne(e => e.User)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.UserId);
    }
}