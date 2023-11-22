namespace Contracts.Requests;

public record AddGroupOfTariffsRequest
{
    public required string Name { get; init; }
    public required ICollection<Guid> TariffsId { get; init; }
}