namespace Contracts.Requests;

public record ResetPasswordRequest
{
    public required string NewPassword { get; init; }
    public required string ConfirmationCode { get; init; }
    public required string NewPasswordConfirmation { get; init; }
    public required string Email { get; init; }
}