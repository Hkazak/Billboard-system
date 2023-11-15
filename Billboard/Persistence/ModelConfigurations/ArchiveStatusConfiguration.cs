using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class ArchiveStatusConfiguration : IEntityTypeConfiguration<ArchiveStatus>
{
    public void Configure(EntityTypeBuilder<ArchiveStatus> builder)
    {
        var roles = new List<ArchiveStatus>
        {
            new ArchiveStatus
            {
                Id = ArchiveStatusId.Archived, Status = ArchiveStatusId.Archived.ToString()
            },
            new ArchiveStatus
            {
                Id = ArchiveStatusId.NonArchived, Status = ArchiveStatusId.NonArchived.ToString()
            }
        };
        builder.HasKey(e => e.Id);
        builder.HasData(roles);
    }
}