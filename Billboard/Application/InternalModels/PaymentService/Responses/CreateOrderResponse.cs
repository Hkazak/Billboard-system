using System.Text.Json.Serialization;
using Application.InternalModels.PaymentService.Models;

namespace Application.InternalModels.PaymentService.Responses;

public record CreateOrderResponse
{
    [JsonPropertyName("order")]
    public required PaymentOrder Order { get; init; }

    [JsonPropertyName("order_access_token")]
    public required string OrderAccessToken { get; init; }
}