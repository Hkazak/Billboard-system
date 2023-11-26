namespace Contracts.DataTransferObjects;

public record AddTariff
{
    public required string Title { get; init; }
    public required TimeSpan StartTime { get; init; }
    public required TimeSpan EndTime { get; init; }
    public required decimal Price { get; init; }
}