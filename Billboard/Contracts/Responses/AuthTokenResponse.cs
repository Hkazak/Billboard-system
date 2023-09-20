namespace Contracts.Responses;

public record AuthTokenResponse
{
    public required string AccessToken { get; init; }
}