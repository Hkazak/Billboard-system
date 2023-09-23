using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var admin = new User
        {
            Email = "admin@Billboard.com", 
            Name = "Admin", 
            Password = "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342",
            RoleId = UserRoleId.Administrator
        };
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(64);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).HasMaxLength(64);
        builder.Property(e => e.Password).HasMaxLength(64);
        builder.HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey(e => e.RoleId);
        builder.HasData(admin);
    }
}