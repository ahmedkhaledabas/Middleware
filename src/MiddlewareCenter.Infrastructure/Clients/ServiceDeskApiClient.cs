using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ServiceDesk;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// ManageEngine ServiceDesk API client implementation.
/// </summary>
public class ServiceDeskApiClient : ExternalApiClientBase, IServiceDeskApiClient
{
    public ServiceDeskApiClient(HttpClient httpClient, string baseUrl, ILogger<ServiceDeskApiClient> logger)
        : base(httpClient, baseUrl, logger) { }

    public Task<List<ServiceTicketDto>> GetPendingTicketsAsync(CancellationToken ct = default)
        => GetAsync<List<ServiceTicketDto>>("tickets/pending", ct);

    public async Task<int> GetPendingTicketsCountAsync(CancellationToken ct = default)
    {
        var result = await GetAsync<CountResult>("tickets/pending/count", ct);
        return result.count;
    }

    public Task<ServiceTicketDto> GetTicketByIdAsync(int ticketId, CancellationToken ct = default)
        => GetAsync<ServiceTicketDto>($"tickets/{ticketId}", ct);

    public Task<ServiceTicketResultDto> ApproveTicketAsync(int ticketId, TicketActionRequest request, CancellationToken ct = default)
        => PostAsync<TicketActionRequest, ServiceTicketResultDto>($"tickets/{ticketId}/approve", request, ct);

    public Task<ServiceTicketResultDto> RejectTicketAsync(int ticketId, TicketActionRequest request, CancellationToken ct = default)
        => PostAsync<TicketActionRequest, ServiceTicketResultDto>($"tickets/{ticketId}/reject", request, ct);

    public Task<List<ServiceTicketHistoryDto>> GetTicketHistoryAsync(int ticketId, CancellationToken ct = default)
        => GetAsync<List<ServiceTicketHistoryDto>>($"tickets/{ticketId}/history", ct);

    public Task<List<ServiceTicketDto>> GetTicketsAsync(TicketFilter? filter = null, CancellationToken ct = default)
    {
        var query = BuildFilterQuery(filter);
        return GetAsync<List<ServiceTicketDto>>($"tickets{query}", ct);
    }

    private static string BuildFilterQuery(TicketFilter? filter)
    {
        if (filter is null) return string.Empty;

        var parts = new List<string>();
        if (!string.IsNullOrEmpty(filter.status)) parts.Add($"status={Uri.EscapeDataString(filter.status)}");
        if (!string.IsNullOrEmpty(filter.priority)) parts.Add($"priority={Uri.EscapeDataString(filter.priority)}");
        if (!string.IsNullOrEmpty(filter.category)) parts.Add($"category={Uri.EscapeDataString(filter.category)}");
        if (!string.IsNullOrEmpty(filter.assignedTo)) parts.Add($"assignedTo={Uri.EscapeDataString(filter.assignedTo)}");
        if (filter.fromDate.HasValue) parts.Add($"fromDate={filter.fromDate.Value:yyyy-MM-dd}");
        if (filter.toDate.HasValue) parts.Add($"toDate={filter.toDate.Value:yyyy-MM-dd}");
        parts.Add($"pageNumber={filter.pageNumber}");
        parts.Add($"pageSize={filter.pageSize}");

        return parts.Count > 0 ? "?" + string.Join("&", parts) : string.Empty;
    }

    private record CountResult(int count);
}
