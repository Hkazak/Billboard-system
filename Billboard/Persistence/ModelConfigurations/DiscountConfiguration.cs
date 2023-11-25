using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(64);
        builder.Property(e => e.SalesOf).HasPrecision(5, 2);
        builder.Property(e => e.EndDate).IsRequired();
        builder.HasOne(e => e.ArchiveStatus)
            .WithMany()
            .HasForeignKey(e => e.ArchiveStatusId);
        builder.HasMany(e => e.Billboards)
            .WithMany(e => e.Discounts);
    }
}