using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.K2;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers.v1;

/// <summary>
/// K2 Nintex (myMemo) workflow integration endpoints.
/// </summary>
[ApiController]
[Route("api/v1/k2")]
public class K2Controller : ApiControllerBase
{
    private readonly IK2ApiClient _client;
    private readonly ILogger<K2Controller> _logger;

    public K2Controller(IK2ApiClient client, ILogger<K2Controller> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>Gets all pending memos awaiting action.</summary>
    [HttpGet("memos/pending")]
    [ProducesResponseType(typeof(ApiResponse<List<MemoDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<MemoDto>>>> GetPendingMemos(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetPendingMemos", TransactionId, CurrentUser);
        return OkResponse(await _client.GetPendingMemosAsync(ct));
    }

    /// <summary>Gets the count of pending memos.</summary>
    [HttpGet("memos/pending/count")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<int>>> GetPendingMemosCount(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetPendingMemosCount", TransactionId, CurrentUser);
        return OkResponse(await _client.GetPendingMemosCountAsync(ct));
    }

    /// <summary>Gets a memo by ID.</summary>
    [HttpGet("memos/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<MemoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<MemoDto>>> GetMemo(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetMemo {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetMemoByIdAsync(id, ct));
    }

    /// <summary>Approves a memo.</summary>
    [HttpPost("memos/{id:int}/approve")]
    [ProducesResponseType(typeof(ApiResponse<MemoResultDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<MemoResultDto>>> ApproveMemo(int id, [FromBody] MemoActionRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → ApproveMemo {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.ApproveMemoAsync(id, request, ct));
    }

    /// <summary>Rejects a memo.</summary>
    [HttpPost("memos/{id:int}/reject")]
    [ProducesResponseType(typeof(ApiResponse<MemoResultDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<MemoResultDto>>> RejectMemo(int id, [FromBody] MemoActionRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → RejectMemo {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.RejectMemoAsync(id, request, ct));
    }

    /// <summary>Gets memo action history.</summary>
    [HttpGet("memos/history")]
    [ProducesResponseType(typeof(ApiResponse<List<MemoHistoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<MemoHistoryDto>>>> GetMemoHistory(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetMemoHistory", TransactionId, CurrentUser);
        return OkResponse(await _client.GetMemoHistoryAsync(ct));
    }
}
