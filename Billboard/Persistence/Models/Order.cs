using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required double ProductPrice { get; init; }
    public OrderStatusId StatusId { get; init; }
    public OrderStatus? OrderStatus { get; private set; }
}