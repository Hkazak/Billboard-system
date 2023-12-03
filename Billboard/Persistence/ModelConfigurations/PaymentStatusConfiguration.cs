using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        builder.HasKey(e => e.Id);
        var data = Enum.GetValues<PaymentStatusId>()
            .Select(id => new PaymentStatus
            {
                Id = id,
                Name = id.ToString()
            });
        builder.HasData(data);
    }
}