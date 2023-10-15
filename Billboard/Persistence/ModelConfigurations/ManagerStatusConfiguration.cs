using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class ManagerStatusConfiguration : IEntityTypeConfiguration<ManagerStatus>
{
    public void Configure(EntityTypeBuilder<ManagerStatus> builder)
    {
        var roles = new List<ManagerStatus>
        {
            new ManagerStatus
            {
                Id = ManagerStatusId.Active,Status = ManagerStatusId.Active.ToString()
            },
            new ManagerStatus
            {
                Id = ManagerStatusId.Inactive,Status = ManagerStatusId.Inactive.ToString()
            },
        };
        builder.HasKey(e => e.Id);
        builder.HasData(roles);
    }
}