namespace Contracts.Requests;

public record AddManagerRequest
{
    public required string Email { get; init; }
    public required string Name { get; init; }
}