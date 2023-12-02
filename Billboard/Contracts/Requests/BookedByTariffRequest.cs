namespace Contracts.Requests;

public record BookedByTariffRequest
{
    public required Guid TariffId { get; init; } = Guid.Empty;
    public required Guid BillboardId { get; init; } = Guid.Empty;
}