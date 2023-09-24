using System.Text.Json.Serialization;

namespace Contracts.Configurations;

public record JwtConfiguration
{
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }

    [JsonIgnore]
    public static readonly JwtConfiguration Empty = new()
    {
        Key = string.Empty,
        Issuer = string.Empty,
        Audience = string.Empty
    };
}
