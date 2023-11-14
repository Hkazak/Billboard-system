namespace Contracts.Responses;

public class GroupOfTariffsResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required ICollection<TariffResponse> Tariffs { get; init; }
}