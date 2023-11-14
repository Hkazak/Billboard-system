using System.Net;
using Contracts.Exceptions;
using Contracts.Responses;

namespace Presentation.Middlewares;

public class CustomExceptionsHandler
{
    private readonly ILogger<CustomExceptionsHandler> _logger;
    private readonly RequestDelegate _next;

    public CustomExceptionsHandler(ILogger<CustomExceptionsHandler> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError("Request to the {Path} endpoint was failed", context.Request.Path);
            await GenerateResponseAsync(context, exception);
        }
    }
    
    private async Task GenerateResponseAsync(HttpContext context, Exception exception)
    {
        _logger.LogError("Request to {Path} throws exception {ExceptionMessage}", context.Request.Path,
            exception.Message);
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            NotFoundException => new ErrorResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = exception.Message
            },
            NotPermissionsException => new ErrorResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                ErrorMessage = exception.Message
            },
            InvalidRequestDataException => new ErrorResponse
            {
                StatusCode  = HttpStatusCode.BadRequest, 
                ErrorMessage = exception.Message
            },
            InvalidCredentialsException => new ErrorResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = exception.Message
            },
            _ => new ErrorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ErrorMessage = "Something wrong"
            }
        };

        context.Response.StatusCode = (int) response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}