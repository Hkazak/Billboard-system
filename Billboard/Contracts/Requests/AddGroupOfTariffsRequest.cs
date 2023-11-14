namespace Contracts.Requests;

public class AddGroupOfTariffsRequest
{
    public required string Name { get; init; }
    public required ICollection<Guid> TariffsId { get; init; }
}