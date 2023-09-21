using Application.Services;
using Application.ServicesImplementations;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BillboardContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }

    public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
    
    public static IServiceCollection ConfigureCqrs()
    {
        // TODO configure CQRS by MediatR
        throw new NotImplementedException();
    }
}