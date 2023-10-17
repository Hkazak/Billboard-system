namespace Contracts.Configurations;

public record EmailConfiguration
{
    public required string EmailAddress { get; init; }
    public required string Password { get; init; }
    public required string Nickname { get; init; }
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required bool UseSsl { get; init; }
}