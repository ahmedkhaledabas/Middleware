using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.K2;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// K2 Nintex (myMemo) workflow API client implementation.
/// </summary>
public class K2ApiClient : ExternalApiClientBase, IK2ApiClient
{
    public K2ApiClient(HttpClient httpClient, string baseUrl, ILogger<K2ApiClient> logger)
        : base(httpClient, baseUrl, logger) { }

    public Task<List<MemoDto>> GetPendingMemosAsync(CancellationToken ct = default)
        => GetAsync<List<MemoDto>>("memos/pending", ct);

    public async Task<int> GetPendingMemosCountAsync(CancellationToken ct = default)
    {
        var result = await GetAsync<CountResult>("memos/pending/count", ct);
        return result.count;
    }

    public Task<MemoDto> GetMemoByIdAsync(int memoId, CancellationToken ct = default)
        => GetAsync<MemoDto>($"memos/{memoId}", ct);

    public Task<MemoResultDto> ApproveMemoAsync(int memoId, MemoActionRequest request, CancellationToken ct = default)
        => PostAsync<MemoActionRequest, MemoResultDto>($"memos/{memoId}/approve", request, ct);

    public Task<MemoResultDto> RejectMemoAsync(int memoId, MemoActionRequest request, CancellationToken ct = default)
        => PostAsync<MemoActionRequest, MemoResultDto>($"memos/{memoId}/reject", request, ct);

    public Task<List<MemoHistoryDto>> GetMemoHistoryAsync(CancellationToken ct = default)
        => GetAsync<List<MemoHistoryDto>>("memos/history", ct);

    private record CountResult(int count);
}
