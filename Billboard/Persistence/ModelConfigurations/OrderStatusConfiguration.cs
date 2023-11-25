using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Enums;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        var status = new List<OrderStatus>
        {
            new OrderStatus
            {
                Id = OrderStatusId.Submitted, Status = OrderStatusId.Submitted.ToString()
            },
            new OrderStatus
            {
                Id = OrderStatusId.InProgress, Status = OrderStatusId.InProgress.ToString()
            },
            new OrderStatus
            {
                Id = OrderStatusId.Completed, Status = OrderStatusId.Completed.ToString()
            }
        };
        builder.HasKey(e => e.Id);
        builder.HasData(status);
    }
}