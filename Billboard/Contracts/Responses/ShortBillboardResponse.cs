namespace Contracts.Responses;

public record ShortBillboardResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}