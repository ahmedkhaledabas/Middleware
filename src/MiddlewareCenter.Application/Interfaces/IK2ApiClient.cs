using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.K2;
using MiddlewareCenter.Domain.Interfaces;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// K2 Nintex (myMemo) workflow integration client.
/// </summary>
public interface IK2ApiClient : IExternalApiClient
{
    Task<List<MemoDto>> GetPendingMemosAsync(CancellationToken ct = default);
    Task<int> GetPendingMemosCountAsync(CancellationToken ct = default);
    Task<MemoDto> GetMemoByIdAsync(int memoId, CancellationToken ct = default);
    Task<MemoResultDto> ApproveMemoAsync(int memoId, MemoActionRequest request, CancellationToken ct = default);
    Task<MemoResultDto> RejectMemoAsync(int memoId, MemoActionRequest request, CancellationToken ct = default);
    Task<List<MemoHistoryDto>> GetMemoHistoryAsync(CancellationToken ct = default);
}
