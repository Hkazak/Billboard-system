using Presentation.Middlewares;

namespace Presentation.Extensions;

public static class MiddlewareExtensions
{
    public static void UseCustomExceptionHandling(this WebApplication application)
    {
        application.UseMiddleware<CustomExceptionsHandler>();
    }
}