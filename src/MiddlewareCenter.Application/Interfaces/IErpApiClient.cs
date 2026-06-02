using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.ERP;
using MiddlewareCenter.Domain.Interfaces;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// ERP HR services integration client.
/// </summary>
public interface IErpApiClient : IExternalApiClient
{
    // Work From Home
    Task<List<WorkFromHomeDto>> GetWorkFromHomeRequestsAsync(CancellationToken ct = default);
    Task<WorkFromHomeDto> GetWorkFromHomeByIdAsync(int id, CancellationToken ct = default);
    Task<WorkFromHomeDto> CreateWorkFromHomeRequestAsync(CreateWorkFromHomeRequest request, CancellationToken ct = default);
    Task<bool> CancelWorkFromHomeRequestAsync(int id, CancellationToken ct = default);
    Task<WorkFromHomeDto> ApproveWorkFromHomeRequestAsync(int id, ApproveRequest request, CancellationToken ct = default);

    // Leaves
    Task<List<LeaveDto>> GetLeaveRequestsAsync(CancellationToken ct = default);
    Task<LeaveBalanceDto> GetLeaveBalanceAsync(CancellationToken ct = default);
    Task<LeaveDto> CreateLeaveRequestAsync(CreateLeaveRequest request, CancellationToken ct = default);
    Task<bool> CancelLeaveRequestAsync(int id, CancellationToken ct = default);

    // Vacations
    Task<List<VacationDto>> GetVacationRequestsAsync(CancellationToken ct = default);
    Task<VacationDto> CreateVacationRequestAsync(CreateVacationRequest request, CancellationToken ct = default);
    Task<bool> CancelVacationRequestAsync(int id, CancellationToken ct = default);

    // HR Letters
    Task<List<HrLetterDto>> GetHrLetterRequestsAsync(CancellationToken ct = default);
    Task<HrLetterDto> GetHrLetterByIdAsync(int id, CancellationToken ct = default);
    Task<HrLetterDto> CreateHrLetterRequestAsync(CreateHrLetterRequest request, CancellationToken ct = default);
    Task<byte[]> DownloadHrLetterAsync(int id, CancellationToken ct = default);

    // Work Location
    Task<List<WorkLocationDto>> GetWorkLocationRequestsAsync(CancellationToken ct = default);
    Task<WorkLocationDto> CreateWorkLocationRequestAsync(CreateWorkLocationRequest request, CancellationToken ct = default);
    Task<WorkLocationDto> ApproveWorkLocationRequestAsync(int id, ApproveRequest request, CancellationToken ct = default);

    // ID Replacement
    Task<List<IdReplacementDto>> GetIdReplacementRequestsAsync(CancellationToken ct = default);
    Task<IdReplacementDto> CreateIdReplacementRequestAsync(CreateIdReplacementRequest request, CancellationToken ct = default);
    Task<IdReplacementDto> GetIdReplacementByIdAsync(int id, CancellationToken ct = default);

    // Points
    Task<HrPointsDto> GetHrPointsAsync(CancellationToken ct = default);

    // Requests
    Task<List<HrRequestDto>> GetReceivedRequestsAsync(CancellationToken ct = default);
    Task<List<HrRequestDto>> GetSentRequestsAsync(CancellationToken ct = default);
}
