using Persistence.Enums;

namespace Persistence.Models;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public required decimal ProductPrice { get; set; }
    public required Guid BillboardId { get; init; }
    public Billboard? Billboard { get; init; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public Guid? DiscountId { get; set; }
    public Discount? Discount { get; set; }
    public OrderStatusId StatusId { get; set; }
    public OrderStatus? OrderStatus { get; private set; }
}