using System.Reflection;
using System.Text;
using Application.Services;
using Application.ServicesImplementations;
using Contracts.Configurations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    
    public static IServiceCollection ConfigureCqrs(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.Load(nameof(Application)));
        });
        return services;
    }

    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(Assembly.Load(nameof(Presentation)));
    }

    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
        if (jwtConfig is null)
        {
            throw new InvalidOperationException("JWT Configuration is undefined");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
        services.AddTransient<JwtConfiguration>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = securityKey
                };
            });
        services.AddAuthorization();
        return services;
    }
}