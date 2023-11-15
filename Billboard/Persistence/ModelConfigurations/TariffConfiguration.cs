using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Price).HasPrecision(12,2);
        builder.Property(e => e.StartTime);
        builder.Property(e => e.EndTime);
        builder.Property(e => e.Title).HasMaxLength(64);
        builder.HasOne(e => e.ArchiveStatus)
            .WithMany()
            .HasForeignKey(e => e.ArchiveStatusId);
    }
}