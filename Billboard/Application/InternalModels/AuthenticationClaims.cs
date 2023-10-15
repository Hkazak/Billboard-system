namespace Application.InternalModels;

public record AuthenticationClaims
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
}