using System.Text.Json.Serialization;

namespace Application.InternalModels.PaymentService.Models;

public record PaymentOrder
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("shop_id")]
    public required string ShopId { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("created_at")]
    public required string CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public required long Amount { get; init; }

    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("capture_method")]
    public required string CaptureMethod { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("attempts")]
    public required int Attempts { get; init; }

    [JsonPropertyName("due_date")]
    public required string DueDate { get; init; }

    [JsonPropertyName("customer_id")]
    public required string CustomerId { get; init; }

    [JsonPropertyName("card_id")]
    public required string CardId { get; init; }

    [JsonPropertyName("back_url")]
    public required string BackUrl { get; init; }

    [JsonPropertyName("success_url")]
    public required string SuccessUrl { get; init; }

    [JsonPropertyName("failure_url")]
    public required string FailureUrl { get; init; }

    [JsonPropertyName("template")]
    public required string Template { get; init; }

    [JsonPropertyName("checkout_url")]
    public required string CheckoutUrl { get; init; }

    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("mcc")]
    public required string Mcc { get; init; }
}