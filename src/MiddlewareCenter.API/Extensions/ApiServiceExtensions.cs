using MiddlewareCenter.API.Filters;
using Microsoft.OpenApi.Models;

namespace MiddlewareCenter.API.Extensions;

/// <summary>
/// Extension methods for registering API-layer services (controllers, Swagger, health checks, etc.).
/// Keeps <c>Program.cs</c> lean by grouping related registrations.
/// </summary>
public static class ApiServiceExtensions
{
    /// <summary>
    /// Registers controllers with the global <see cref="ValidateModelFilter"/>,
    /// camelCase JSON serialisation, and null-value suppression.
    /// </summary>
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilter>();
        })
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.PropertyNamingPolicy =
                System.Text.Json.JsonNamingPolicy.CamelCase;
            opts.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        return services;
    }

    /// <summary>
    /// Registers Swagger / OpenAPI with Windows Authentication security definition.
    /// </summary>
    public static IServiceCollection AddApiSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title       = "MiddlewareCenter API",
                Version     = "v1",
                Description = "Central integration hub for Coursera, ERP, K2, ServiceDesk, and SharePoint.",
                Contact     = new OpenApiContact { Name = "MiddlewareCenter Team" }
            });

            c.AddSecurityDefinition("Windows", new OpenApiSecurityScheme
            {
                Type        = SecuritySchemeType.Http,
                Scheme      = "negotiate",
                Description = "Windows Authentication (Negotiate/Kerberos/NTLM)"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id   = "Windows"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
                c.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Registers CORS with origins read from <c>Cors:AllowedOrigins</c> in configuration.
    /// </summary>
    public static IServiceCollection AddApiCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>()
            ?? ["http://localhost:3000", "http://localhost:4200"];

        services.AddCors(options =>
        {
            options.AddPolicy("MiddlewareCenterPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
