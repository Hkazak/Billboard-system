namespace Contracts.Requests;

public record AddTariffRequest
{
    public required string Title { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required decimal Price { get; init; }
}