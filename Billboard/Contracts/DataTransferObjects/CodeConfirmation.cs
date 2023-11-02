namespace Contracts.DataTransferObjects;

public record CodeConfirmation
{
    public required string Email { get; init; }
    public required string NewPassword { get; init; }
    public required string ConfirmationCode { get; init; }
}