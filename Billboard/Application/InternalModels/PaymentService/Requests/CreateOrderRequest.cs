using System.Text.Json.Serialization;

namespace Application.InternalModels.PaymentService.Requests;

public record CreateOrderRequest
{
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

    [JsonPropertyName("back_url")]
    public required string BackUrl { get; init; }

    [JsonPropertyName("success_url")]
    public required string SuccessUrl { get; init; }

    [JsonPropertyName("failure_url")]
    public required string FailureUrl { get; init; }
}