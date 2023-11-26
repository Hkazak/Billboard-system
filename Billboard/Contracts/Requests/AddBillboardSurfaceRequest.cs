namespace Contracts.Requests;

public record AddBillboardSurfaceRequest
{
    public required string Surface { get; init; }
}