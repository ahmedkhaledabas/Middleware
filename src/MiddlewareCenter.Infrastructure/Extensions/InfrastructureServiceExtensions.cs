using MiddlewareCenter.Application.Interfaces;
using MiddlewareCenter.Infrastructure.Clients;
using MiddlewareCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Extensions;

/// <summary>
/// Registers all Infrastructure services into the DI container.
/// </summary>
public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ── EF Core DbContext ─────────────────────────────────────────────────
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (!string.IsNullOrWhiteSpace(connectionString) &&
            !connectionString.Contains("YOUR_SERVER", StringComparison.OrdinalIgnoreCase))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    sqlOptions.CommandTimeout(30);
                }));
        }
        else
        {
            // Fallback to in-memory for local dev / CI without a real SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("MiddlewareCenterDev"));
        }

        // ── Repository ────────────────────────────────────────────────────────
        services.AddScoped<IRequestLogRepository, RequestLogRepository>();

        // ── External API Clients ──────────────────────────────────────────────
        // Register named HttpClients and their typed API clients
        RegisterApiClient<ICourseraApiClient, CourseraApiClient>(
            services, configuration, "ExternalApis:Coursera:BaseUrl", "Coursera");

        RegisterApiClient<IErpApiClient, ErpApiClient>(
            services, configuration, "ExternalApis:ERP:BaseUrl", "ERP");

        RegisterApiClient<IK2ApiClient, K2ApiClient>(
            services, configuration, "ExternalApis:K2:BaseUrl", "K2");

        RegisterApiClient<IServiceDeskApiClient, ServiceDeskApiClient>(
            services, configuration, "ExternalApis:ServiceDesk:BaseUrl", "ServiceDesk");

        RegisterApiClient<ISharePointApiClient, SharePointApiClient>(
            services, configuration, "ExternalApis:SharePoint:BaseUrl", "SharePoint");

        return services;
    }

    private static void RegisterApiClient<TInterface, TImplementation>(
        IServiceCollection services,
        IConfiguration configuration,
        string configKey,
        string clientName)
        where TInterface : class
        where TImplementation : ExternalApiClientBase, TInterface
    {
        services.AddHttpClient(clientName, client =>
        {
            var baseUrl = configuration[configKey] ?? $"https://placeholder-{clientName.ToLower()}.example.com/api";
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<TInterface>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient(clientName);
            var baseUrl = configuration[configKey] ?? $"https://placeholder-{clientName.ToLower()}.example.com/api";
            var logger = sp.GetRequiredService<ILogger<TImplementation>>();
            return (TInterface)Activator.CreateInstance(typeof(TImplementation), httpClient, baseUrl, logger)!;
        });
    }
}
