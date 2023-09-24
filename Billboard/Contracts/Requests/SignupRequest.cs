namespace Contracts.Requests;

public record SignupRequest
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string Password { get; init; }
}