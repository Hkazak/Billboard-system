using System.Net.Http.Json;
using System.Text.Json;
using Application.InternalModels.PaymentService.Models;
using Application.InternalModels.PaymentService.Requests;
using Application.InternalModels.PaymentService.Responses;
using Application.Services;
using Contracts.Configurations;
using Contracts.Exceptions;

namespace Application.ServicesImplementations;

public class IokaPaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly IokaConfiguration _configuration;

    public IokaPaymentService(HttpClient httpClient, IokaConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public string BackUrl => _configuration.BackUrl;
    public string SuccessUrl => _configuration.SuccessPayUrl;
    public string FailureUrl => _configuration.FailurePayUrl;

    public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _configuration.CreateOrderUrl);
        httpRequest.Content = JsonContent.Create(request);
        httpRequest.Headers.Add("API-KEY", _configuration.ApiKey);
        var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new InternalServiceException($"Some problem with Ioka, response body: {jsonResponse}");
        }

        var response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);
        if (response is null)
        {
            throw new JsonException($"Ioka response invalid: {jsonResponse}");
        }

        return response;
    }

    public async Task<PaymentOrder> GetOrderById(string externalOrderId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"{_configuration.GetOrderByIdUrl}/{externalOrderId}";
        var httpResponse = await _httpClient.GetAsync(endpoint, cancellationToken);
        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new InternalServiceException($"Some problem with Ioka, response body: {jsonResponse}");
        }

        var response = JsonSerializer.Deserialize<PaymentOrder>(jsonResponse);
        if (response is null)
        {
            throw new JsonException($"Ioka response invalid: {jsonResponse}");
        }

        return response;
    }
}