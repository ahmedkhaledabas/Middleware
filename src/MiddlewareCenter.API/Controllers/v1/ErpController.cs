using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ERP;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers.v1;

/// <summary>
/// ERP HR services integration endpoints.
/// </summary>
[ApiController]
[Route("api/v1/erp")]
public class ErpController : ApiControllerBase
{
    private readonly IErpApiClient _client;
    private readonly ILogger<ErpController> _logger;

    public ErpController(IErpApiClient client, ILogger<ErpController> logger)
    {
        _client = client;
        _logger = logger;
    }

    // ── Work From Home ──────────────────────────────────────────────────────

    [HttpGet("wfh")]
    [ProducesResponseType(typeof(ApiResponse<List<WorkFromHomeDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<WorkFromHomeDto>>>> GetWfhRequests(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetWfhRequests", TransactionId, CurrentUser);
        return OkResponse(await _client.GetWorkFromHomeRequestsAsync(ct));
    }

    [HttpGet("wfh/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<WorkFromHomeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<WorkFromHomeDto>>> GetWfhById(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetWfhById {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetWorkFromHomeByIdAsync(id, ct));
    }

    [HttpPost("wfh")]
    [ProducesResponseType(typeof(ApiResponse<WorkFromHomeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<WorkFromHomeDto>>> CreateWfh([FromBody] CreateWorkFromHomeRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateWfh", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateWorkFromHomeRequestAsync(request, ct));
    }

    [HttpPut("wfh/{id:int}/cancel")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<bool>>> CancelWfh(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CancelWfh {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.CancelWorkFromHomeRequestAsync(id, ct));
    }

    [HttpPut("wfh/{id:int}/approve")]
    [ProducesResponseType(typeof(ApiResponse<WorkFromHomeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<WorkFromHomeDto>>> ApproveWfh(int id, [FromBody] ApproveRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → ApproveWfh {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.ApproveWorkFromHomeRequestAsync(id, request, ct));
    }

    // ── Leaves ──────────────────────────────────────────────────────────────

    [HttpGet("leaves")]
    [ProducesResponseType(typeof(ApiResponse<List<LeaveDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<LeaveDto>>>> GetLeaves(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetLeaves", TransactionId, CurrentUser);
        return OkResponse(await _client.GetLeaveRequestsAsync(ct));
    }

    [HttpGet("leaves/balance")]
    [ProducesResponseType(typeof(ApiResponse<LeaveBalanceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<LeaveBalanceDto>>> GetLeaveBalance(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetLeaveBalance", TransactionId, CurrentUser);
        return OkResponse(await _client.GetLeaveBalanceAsync(ct));
    }

    [HttpPost("leaves")]
    [ProducesResponseType(typeof(ApiResponse<LeaveDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<LeaveDto>>> CreateLeave([FromBody] CreateLeaveRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateLeave", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateLeaveRequestAsync(request, ct));
    }

    [HttpPut("leaves/{id:int}/cancel")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<bool>>> CancelLeave(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CancelLeave {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.CancelLeaveRequestAsync(id, ct));
    }

    // ── Vacations ────────────────────────────────────────────────────────────

    [HttpGet("vacations")]
    [ProducesResponseType(typeof(ApiResponse<List<VacationDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<VacationDto>>>> GetVacations(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetVacations", TransactionId, CurrentUser);
        return OkResponse(await _client.GetVacationRequestsAsync(ct));
    }

    [HttpPost("vacations")]
    [ProducesResponseType(typeof(ApiResponse<VacationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<VacationDto>>> CreateVacation([FromBody] CreateVacationRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateVacation", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateVacationRequestAsync(request, ct));
    }

    [HttpPut("vacations/{id:int}/cancel")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<bool>>> CancelVacation(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CancelVacation {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.CancelVacationRequestAsync(id, ct));
    }

    // ── HR Letters ───────────────────────────────────────────────────────────

    [HttpGet("letters")]
    [ProducesResponseType(typeof(ApiResponse<List<HrLetterDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<HrLetterDto>>>> GetLetters(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetLetters", TransactionId, CurrentUser);
        return OkResponse(await _client.GetHrLetterRequestsAsync(ct));
    }

    [HttpGet("letters/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HrLetterDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<HrLetterDto>>> GetLetterById(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetLetterById {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetHrLetterByIdAsync(id, ct));
    }

    [HttpPost("letters")]
    [ProducesResponseType(typeof(ApiResponse<HrLetterDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<HrLetterDto>>> CreateLetter([FromBody] CreateHrLetterRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateLetter", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateHrLetterRequestAsync(request, ct));
    }

    [HttpGet("letters/{id:int}/download")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadLetter(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → DownloadLetter {Id}", TransactionId, CurrentUser, id);
        var bytes = await _client.DownloadHrLetterAsync(id, ct);
        return File(bytes, "application/pdf", $"hr-letter-{id}.pdf");
    }

    // ── Work Location ────────────────────────────────────────────────────────

    [HttpGet("work-location")]
    [ProducesResponseType(typeof(ApiResponse<List<WorkLocationDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<WorkLocationDto>>>> GetWorkLocations(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetWorkLocations", TransactionId, CurrentUser);
        return OkResponse(await _client.GetWorkLocationRequestsAsync(ct));
    }

    [HttpPost("work-location")]
    [ProducesResponseType(typeof(ApiResponse<WorkLocationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<WorkLocationDto>>> CreateWorkLocation([FromBody] CreateWorkLocationRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateWorkLocation", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateWorkLocationRequestAsync(request, ct));
    }

    [HttpPut("work-location/{id:int}/approve")]
    [ProducesResponseType(typeof(ApiResponse<WorkLocationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<WorkLocationDto>>> ApproveWorkLocation(int id, [FromBody] ApproveRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → ApproveWorkLocation {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.ApproveWorkLocationRequestAsync(id, request, ct));
    }

    // ── ID Replacement ───────────────────────────────────────────────────────

    [HttpGet("id-replacement")]
    [ProducesResponseType(typeof(ApiResponse<List<IdReplacementDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<IdReplacementDto>>>> GetIdReplacements(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetIdReplacements", TransactionId, CurrentUser);
        return OkResponse(await _client.GetIdReplacementRequestsAsync(ct));
    }

    [HttpGet("id-replacement/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<IdReplacementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IdReplacementDto>>> GetIdReplacementById(int id, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetIdReplacementById {Id}", TransactionId, CurrentUser, id);
        return OkResponse(await _client.GetIdReplacementByIdAsync(id, ct));
    }

    [HttpPost("id-replacement")]
    [ProducesResponseType(typeof(ApiResponse<IdReplacementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IdReplacementDto>>> CreateIdReplacement([FromBody] CreateIdReplacementRequest request, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateIdReplacement", TransactionId, CurrentUser);
        return OkResponse(await _client.CreateIdReplacementRequestAsync(request, ct));
    }

    // ── Points ───────────────────────────────────────────────────────────────

    [HttpGet("points")]
    [ProducesResponseType(typeof(ApiResponse<HrPointsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<HrPointsDto>>> GetPoints(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetPoints", TransactionId, CurrentUser);
        return OkResponse(await _client.GetHrPointsAsync(ct));
    }

    // ── Requests ─────────────────────────────────────────────────────────────

    [HttpGet("requests/received")]
    [ProducesResponseType(typeof(ApiResponse<List<HrRequestDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<HrRequestDto>>>> GetReceivedRequests(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetReceivedRequests", TransactionId, CurrentUser);
        return OkResponse(await _client.GetReceivedRequestsAsync(ct));
    }

    [HttpGet("requests/sent")]
    [ProducesResponseType(typeof(ApiResponse<List<HrRequestDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<HrRequestDto>>>> GetSentRequests(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetSentRequests", TransactionId, CurrentUser);
        return OkResponse(await _client.GetSentRequestsAsync(ct));
    }
}
