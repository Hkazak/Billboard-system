namespace Contracts.Requests;

public record ForgotPasswordRequest
{
    public required string Email { get; init; }
}