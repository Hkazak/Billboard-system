using System.Net;

namespace Contracts.Responses;

public record ErrorResponse
{
    public required HttpStatusCode StatusCode { get; init; }
    public required string ErrorMessage { get; init; }
}