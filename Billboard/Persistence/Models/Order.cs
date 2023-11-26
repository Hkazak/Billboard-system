using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required decimal ProductPrice { get; init; }
    public required Billboard Billboard { get; init; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public IEnumerable<Discount> Discounts { get; set; }
    public OrderStatusId StatusId { get; init; }
    public OrderStatus? OrderStatus { get; private set; }
}