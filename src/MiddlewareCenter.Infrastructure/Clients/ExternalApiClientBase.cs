using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MiddlewareCenter.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// Base implementation for all external HTTP API clients.
/// Provides standardised GET/POST/PUT/DELETE with error handling and logging.
/// No authentication is configured here — add per-client if required.
/// </summary>
public abstract class ExternalApiClientBase : IExternalApiClient
{
    protected readonly HttpClient _httpClient;
    protected readonly string _baseUrl;
    protected readonly ILogger _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected ExternalApiClientBase(HttpClient httpClient, string baseUrl, ILogger logger)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/');
        _logger = logger;
    }

    public async Task<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken ct = default)
    {
        var url = BuildUrl(endpoint);
        _logger.LogDebug("GET {Url}", url);

        var response = await _httpClient.GetAsync(url, ct);
        return await ReadResponseAsync<TResponse>(response, ct);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload, CancellationToken ct = default)
    {
        var url = BuildUrl(endpoint);
        _logger.LogDebug("POST {Url}", url);

        var content = new StringContent(JsonSerializer.Serialize(payload, _jsonOptions), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content, ct);
        return await ReadResponseAsync<TResponse>(response, ct);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest payload, CancellationToken ct = default)
    {
        var url = BuildUrl(endpoint);
        _logger.LogDebug("PUT {Url}", url);

        var content = new StringContent(JsonSerializer.Serialize(payload, _jsonOptions), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(url, content, ct);
        return await ReadResponseAsync<TResponse>(response, ct);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string endpoint, CancellationToken ct = default)
    {
        var url = BuildUrl(endpoint);
        _logger.LogDebug("DELETE {Url}", url);

        var response = await _httpClient.DeleteAsync(url, ct);
        return await ReadResponseAsync<TResponse>(response, ct);
    }

    private string BuildUrl(string endpoint) =>
        endpoint.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? endpoint
            : $"{_baseUrl}/{endpoint.TrimStart('/')}";

    private async Task<TResponse> ReadResponseAsync<TResponse>(HttpResponseMessage response, CancellationToken ct)
    {
        var body = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("External API error {StatusCode}: {Body}", (int)response.StatusCode, body);
            throw new HttpRequestException(
                $"External API returned {(int)response.StatusCode}: {response.ReasonPhrase}",
                null,
                response.StatusCode);
        }

        if (typeof(TResponse) == typeof(string))
            return (TResponse)(object)body;

        if (typeof(TResponse) == typeof(byte[]))
            return (TResponse)(object)await response.Content.ReadAsByteArrayAsync(ct);

        var result = JsonSerializer.Deserialize<TResponse>(body, _jsonOptions);
        return result ?? throw new InvalidOperationException("Deserialization returned null.");
    }
}
