using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class GroupOfTariffsConfiguration : IEntityTypeConfiguration<GroupOfTariffs>
{
    public void Configure(EntityTypeBuilder<GroupOfTariffs> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(64);
        builder.HasMany(e => e.Tariffs)
            .WithMany(e => e.Groups);
        builder.HasOne(e => e.ArchiveStatus)
            .WithMany()
            .HasForeignKey(e => e.ArchiveStatusId);
    }
}