using System.Reflection;
using System.Text;
using Application.Services;
using Application.ServicesImplementations;
using Contracts.Configurations;
using FluentValidation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<BillboardContext>(builder =>
        {
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }

    public static IServiceCollection ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        if (emailConfig is null)
        {
            throw new InvalidOperationException("Email service configuration is undefined");
        }

        services.AddSingleton<IEmailService, EmailService>(_ => new EmailService(emailConfig, new SmtpClient()));
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
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        var jwtConfig = configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
        if (jwtConfig is null)
        {
            throw new InvalidOperationException("JWT Configuration is undefined");
        }

        services.AddTransient<JwtConfiguration>(_ => jwtConfig);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
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
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            const string version = "v1";
            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = "Billboard System API",
                Version = version,
                Description = "Billboard System API Services"
            });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }

    public static IServiceCollection ConfigureCache(this IServiceCollection services)
    {
        return services.AddDistributedMemoryCache();
    }

    public static IServiceCollection ConfigureFileProvider(this IServiceCollection services, string folderPath)
    {
        return services.AddScoped<IMediaFileProvider, UploadedFileProvider>(_ => new UploadedFileProvider(folderPath));
    }
}