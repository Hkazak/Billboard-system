using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class BillboardSurfaceConfiguration : IEntityTypeConfiguration<BillboardSurface>
{
    public void Configure(EntityTypeBuilder<BillboardSurface> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Surface).HasMaxLength(64);
    }
}