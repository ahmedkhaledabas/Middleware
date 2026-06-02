using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ServiceDesk;
using MiddlewareCenter.Domain.Interfaces;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// ManageEngine ServiceDesk integration client.
/// </summary>
public interface IServiceDeskApiClient : IExternalApiClient
{
    Task<List<ServiceTicketDto>> GetPendingTicketsAsync(CancellationToken ct = default);
    Task<int> GetPendingTicketsCountAsync(CancellationToken ct = default);
    Task<ServiceTicketDto> GetTicketByIdAsync(int ticketId, CancellationToken ct = default);
    Task<ServiceTicketResultDto> ApproveTicketAsync(int ticketId, TicketActionRequest request, CancellationToken ct = default);
    Task<ServiceTicketResultDto> RejectTicketAsync(int ticketId, TicketActionRequest request, CancellationToken ct = default);
    Task<List<ServiceTicketHistoryDto>> GetTicketHistoryAsync(int ticketId, CancellationToken ct = default);
    Task<List<ServiceTicketDto>> GetTicketsAsync(TicketFilter? filter = null, CancellationToken ct = default);
}
