namespace Contracts.Responses;

public record TariffResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string StartTime { get; init; }
    public required string EndTime { get; init; }
    public required decimal Price { get; init; }
}