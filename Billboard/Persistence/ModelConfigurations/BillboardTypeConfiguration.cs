using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class BillboardTypeConfiguration : IEntityTypeConfiguration<BillboardType>
{
    public void Configure(EntityTypeBuilder<BillboardType> builder)
    {
        var roles = new List<BillboardType>
        {
            new BillboardType
            {
                Id = BillboardTypeId.SingleSide, Type = BillboardTypeId.SingleSide.ToString()
            },
            new BillboardType
            {
                Id = BillboardTypeId.DoubleSide, Type = BillboardTypeId.DoubleSide.ToString()
            },
            new BillboardType
            {
                Id = BillboardTypeId.TripleSide, Type = BillboardTypeId.TripleSide.ToString()
            }
        };
        builder.HasKey(e => e.Id);
        builder.HasData(roles);
    }
}