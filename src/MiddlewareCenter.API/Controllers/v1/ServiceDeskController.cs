using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ServiceDesk;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers.v1;

/// <summary>
/// ManageEngine ServiceDesk integration endpoints.
/// </summary>
[ApiController]
[Route("api/v1/servicedesk")]
public class ServiceDeskController : ApiControllerBase
{
    private readonly IServiceDeskApiClient _client;
    private readonly ILogger<ServiceDeskController> _logger;

    public ServiceDeskController(IServiceDeskApiClient client, ILogger<ServiceDeskController> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>Gets all pending tickets awaiting approval.</summary>
    [HttpGet("tickets/pending")]
    [ProducesResponseType(typeof(ApiResponse<List<ServiceTicketDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<ServiceTicketDto>>>> GetPendingTickets(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetPendingTickets", TransactionId, CurrentUser);
        return OkResponse(await _client.GetPendingTicketsAsync(ct));
    }

    /// <summary>Gets the count of pending tickets.</summary>
    [HttpGet("tickets/pending/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<int>>> GetPendingTicketsCount(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetPendingTicketsCount", TransactionId, CurrentUser);
        return OkResponse(await _client.GetPendingTicketsCountAsync(ct));
    }

    /// <summary>Gets a ticket by ID.</summary>
    [HttpGet("tickets/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<ServiceTicketDto>>> GetTicket(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetTicket {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetTicketByIdAsync(id, ct));
    }

    /// <summary>Approves a service ticket.</summary>
    [HttpPost("tickets/{id:int}/approve")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTicketResultDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<ServiceTicketResultDto>>> ApproveTicket(int id, [FromBody] TicketActionRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → ApproveTicket {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.ApproveTicketAsync(id, request, ct));
    }

    /// <summary>Rejects a service ticket.</summary>
    [HttpPost("tickets/{id:int}/reject")]
    [ProducesResponseType(typeof(ApiResponse<ServiceTicketResultDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<ServiceTicketResultDto>>> RejectTicket(int id, [FromBody] TicketActionRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → RejectTicket {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.RejectTicketAsync(id, request, ct));
    }

    /// <summary>Gets ticket action history.</summary>
    [HttpGet("tickets/{id:int}/history")]
    [ProducesResponseType(typeof(ApiResponse<List<ServiceTicketHistoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<ServiceTicketHistoryDto>>>> GetTicketHistory(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetTicketHistory {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetTicketHistoryAsync(id, ct));
    }

    /// <summary>Gets tickets with optional filtering.</summary>
    [HttpGet("tickets")]
    [ProducesResponseType(typeof(ApiResponse<List<ServiceTicketDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<ServiceTicketDto>>>> GetTickets(
        [FromQuery] string? status,
        [FromQuery] string? priority,
        [FromQuery] string? category,
        [FromQuery] string? assignedTo,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        _logger.LogInformation("[{TxId}] {User} → GetTickets", TransactionId, CurrentUser);
        var filter = new TicketFilter
        {
            status = status, priority = priority, category = category,
            assignedTo = assignedTo, fromDate = fromDate, toDate = toDate,
            pageNumber = pageNumber, pageSize = pageSize
        };
        return OkResponse(await _client.GetTicketsAsync(filter, ct));
    }
}
