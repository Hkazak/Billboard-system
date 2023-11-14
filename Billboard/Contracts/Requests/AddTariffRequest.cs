namespace Contracts.Requests;

public record AddTariffRequest
{
    public required string Title { get; init; }
    public required TimeSpan StartTime { get; init; }
    public required TimeSpan EndTime { get; init; }
    public required decimal Price { get; init; }
}