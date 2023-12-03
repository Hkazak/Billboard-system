namespace Contracts.Configurations;

public record IokaConfiguration
{
    public required string CreateOrderUrl { get; init; }
    public required string GetOrderByIdUrl { get; init; }
    public required string ApiKey { get; init; }
    public required string BackUrl { get; init; }
    public required string SuccessPayUrl { get; init; }
    public required string FailurePayUrl { get; init; }
}