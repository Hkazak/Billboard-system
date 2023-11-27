using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class PriceRuleConfiguration : IEntityTypeConfiguration<PriceRule>
{
    public void Configure(EntityTypeBuilder<PriceRule> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.BillboardSurface)
            .WithMany()
            .HasForeignKey(e => e.BillboardSurfaceId);
        builder.HasOne(e => e.BillboardType)
            .WithMany()
            .HasForeignKey(e => e.BillboardTypeId);
        builder.Property(e => e.Price)
            .HasPrecision(12, 2);
        var coverIndexProperties = new[] { nameof(PriceRule.BillboardSurfaceId), nameof(PriceRule.BillboardTypeId) };
        builder.HasIndex(coverIndexProperties, "UX_SurfaceId_TypeId")
            .IsUnique();
    }
}