using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class BillboardConfiguration : IEntityTypeConfiguration<Billboard>
{
    public void Configure(EntityTypeBuilder<Billboard> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(64);
        builder.Property(e => e.Address).HasMaxLength(64);
        builder.Property(e => e.Description).HasMaxLength(64 * 64 * 2);
        builder.Property(e => e.Height).HasPrecision(5, 2);
        builder.Property(e => e.Width).HasPrecision(5, 2);
        builder.Property(e => e.Penalty).HasPrecision(12, 2);
        builder.HasOne(e => e.BillboardType)
            .WithMany()
            .HasForeignKey(e => e.TypeId);
        builder.HasOne(e => e.BillboardSurface);
        builder.HasMany(e => e.Pictures);
        builder.HasOne(e => e.GroupOfTariffs);
        builder.HasOne(e => e.ArchiveStatus)
            .WithMany()
            .HasForeignKey(e => e.ArchiveStatusId);

    }
}