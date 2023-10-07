namespace Contracts.Requests;

public record AddManagerRequest
{
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string MiddleName { get; init; }
    public required string LastName { get; init; }
    public required string Phone { get; init; }
}