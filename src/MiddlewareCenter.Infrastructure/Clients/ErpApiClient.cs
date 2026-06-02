using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ERP;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// ERP HR services API client implementation.
/// </summary>
public class ErpApiClient : ExternalApiClientBase, IErpApiClient
{
    public ErpApiClient(HttpClient httpClient, string baseUrl, ILogger<ErpApiClient> logger)
        : base(httpClient, baseUrl, logger) { }

    // Work From Home
    public Task<List<WorkFromHomeDto>> GetWorkFromHomeRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<WorkFromHomeDto>>("wfh", ct);

    public Task<WorkFromHomeDto> GetWorkFromHomeByIdAsync(int id, CancellationToken ct = default)
        => GetAsync<WorkFromHomeDto>($"wfh/{id}", ct);

    public Task<WorkFromHomeDto> CreateWorkFromHomeRequestAsync(CreateWorkFromHomeRequest request, CancellationToken ct = default)
        => PostAsync<CreateWorkFromHomeRequest, WorkFromHomeDto>("wfh", request, ct);

    public Task<bool> CancelWorkFromHomeRequestAsync(int id, CancellationToken ct = default)
        => PutAsync<object, bool>($"wfh/{id}/cancel", new { }, ct);

    public Task<WorkFromHomeDto> ApproveWorkFromHomeRequestAsync(int id, ApproveRequest request, CancellationToken ct = default)
        => PutAsync<ApproveRequest, WorkFromHomeDto>($"wfh/{id}/approve", request, ct);

    // Leaves
    public Task<List<LeaveDto>> GetLeaveRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<LeaveDto>>("leaves", ct);

    public Task<LeaveBalanceDto> GetLeaveBalanceAsync(CancellationToken ct = default)
        => GetAsync<LeaveBalanceDto>("leaves/balance", ct);

    public Task<LeaveDto> CreateLeaveRequestAsync(CreateLeaveRequest request, CancellationToken ct = default)
        => PostAsync<CreateLeaveRequest, LeaveDto>("leaves", request, ct);

    public Task<bool> CancelLeaveRequestAsync(int id, CancellationToken ct = default)
        => PutAsync<object, bool>($"leaves/{id}/cancel", new { }, ct);

    // Vacations
    public Task<List<VacationDto>> GetVacationRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<VacationDto>>("vacations", ct);

    public Task<VacationDto> CreateVacationRequestAsync(CreateVacationRequest request, CancellationToken ct = default)
        => PostAsync<CreateVacationRequest, VacationDto>("vacations", request, ct);

    public Task<bool> CancelVacationRequestAsync(int id, CancellationToken ct = default)
        => PutAsync<object, bool>($"vacations/{id}/cancel", new { }, ct);

    // HR Letters
    public Task<List<HrLetterDto>> GetHrLetterRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<HrLetterDto>>("letters", ct);

    public Task<HrLetterDto> GetHrLetterByIdAsync(int id, CancellationToken ct = default)
        => GetAsync<HrLetterDto>($"letters/{id}", ct);

    public Task<HrLetterDto> CreateHrLetterRequestAsync(CreateHrLetterRequest request, CancellationToken ct = default)
        => PostAsync<CreateHrLetterRequest, HrLetterDto>("letters", request, ct);

    public Task<byte[]> DownloadHrLetterAsync(int id, CancellationToken ct = default)
        => GetAsync<byte[]>($"letters/{id}/download", ct);

    // Work Location
    public Task<List<WorkLocationDto>> GetWorkLocationRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<WorkLocationDto>>("work-location", ct);

    public Task<WorkLocationDto> CreateWorkLocationRequestAsync(CreateWorkLocationRequest request, CancellationToken ct = default)
        => PostAsync<CreateWorkLocationRequest, WorkLocationDto>("work-location", request, ct);

    public Task<WorkLocationDto> ApproveWorkLocationRequestAsync(int id, ApproveRequest request, CancellationToken ct = default)
        => PutAsync<ApproveRequest, WorkLocationDto>($"work-location/{id}/approve", request, ct);

    // ID Replacement
    public Task<List<IdReplacementDto>> GetIdReplacementRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<IdReplacementDto>>("id-replacement", ct);

    public Task<IdReplacementDto> CreateIdReplacementRequestAsync(CreateIdReplacementRequest request, CancellationToken ct = default)
        => PostAsync<CreateIdReplacementRequest, IdReplacementDto>("id-replacement", request, ct);

    public Task<IdReplacementDto> GetIdReplacementByIdAsync(int id, CancellationToken ct = default)
        => GetAsync<IdReplacementDto>($"id-replacement/{id}", ct);

    // Points
    public Task<HrPointsDto> GetHrPointsAsync(CancellationToken ct = default)
        => GetAsync<HrPointsDto>("points", ct);

    // Requests
    public Task<List<HrRequestDto>> GetReceivedRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<HrRequestDto>>("requests/received", ct);

    public Task<List<HrRequestDto>> GetSentRequestsAsync(CancellationToken ct = default)
        => GetAsync<List<HrRequestDto>>("requests/sent", ct);
}
