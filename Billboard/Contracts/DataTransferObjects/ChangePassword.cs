namespace Contracts.DataTransferObjects;

public record ChangePassword
{
    public required Guid Id { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}