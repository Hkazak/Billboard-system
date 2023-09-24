namespace Contracts.Requests;

public record SigninRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}