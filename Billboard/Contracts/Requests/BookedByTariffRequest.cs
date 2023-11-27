namespace Contracts.Requests;

public record BookedByTariffRequest
{
    public required Guid TariffId { get; init; }
}