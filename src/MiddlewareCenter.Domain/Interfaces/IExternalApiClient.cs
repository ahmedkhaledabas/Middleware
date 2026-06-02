namespace MiddlewareCenter.Domain.Interfaces;

/// <summary>
/// Generic contract for all external API clients.
/// </summary>
public interface IExternalApiClient
{
    Task<TResponse> GetAsync<TResponse>(string endpoint, CancellationToken ct = default);
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload, CancellationToken ct = default);
    Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest payload, CancellationToken ct = default);
    Task<TResponse> DeleteAsync<TResponse>(string endpoint, CancellationToken ct = default);
}
