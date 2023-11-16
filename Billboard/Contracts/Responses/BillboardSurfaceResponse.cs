namespace Contracts.Responses;

public record BillboardSurfaceResponse 
{
    public required Guid Id { get; init; }
    public required string Surface { get; init; }
}