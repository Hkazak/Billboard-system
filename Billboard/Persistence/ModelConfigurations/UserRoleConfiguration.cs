using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var roles = new List<UserRole>
        {
            new UserRole
            {
                Id = UserRoleId.Administrator,Role = UserRoleId.Administrator.ToString()
            },
            new UserRole
            {
                Id = UserRoleId.Client,Role = UserRoleId.Client.ToString()
            },
            new UserRole
            {
                Id = UserRoleId.Manager,Role = UserRoleId.Manager.ToString()
            },
        };
        builder.HasKey(e => e.Id);
        builder.HasData(roles);
    }
}