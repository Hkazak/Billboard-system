using Contracts.DataTransferObjects;

namespace Contracts.Requests;

public record AddOrderRequest
{
    public required Guid BillboardId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required Guid TariffId { get; init; }
    public ICollection<OrderFile> Files { get; init; } = new List<OrderFile>();
}