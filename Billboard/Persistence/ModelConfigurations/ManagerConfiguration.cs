using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName).HasMaxLength(64);
        builder.Property(e => e.MiddleName).HasMaxLength(64);
        builder.Property(e => e.LastName).HasMaxLength(64);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).HasMaxLength(64);
        builder.HasIndex(e => e.Phone).IsUnique();
        builder.Property(e => e.Phone).HasMaxLength(16);
        builder.Property(e => e.Password).HasMaxLength(64);
        builder.HasOne(e => e.ManagerStatus)
            .WithMany()
            .HasForeignKey(e => e.StatusId);
    }
}