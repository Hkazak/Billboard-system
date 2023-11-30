namespace Contracts.DataTransferObjects;

public record AddOrder
{
    public required Guid BillboardId { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required Guid TariffId { get; init; }
    public required Guid UserId { get; init; }
    public ICollection<string> Files { get; init; } = new List<string>();
}