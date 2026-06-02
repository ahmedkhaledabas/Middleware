using MiddlewareCenter.Domain.Entities;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// Repository contract for persisting and querying HTTP request logs.
/// </summary>
public interface IRequestLogRepository
{
    /// <summary>Persists a new request log entry.</summary>
    Task AddAsync(RequestLog log, CancellationToken ct = default);

    /// <summary>Returns a single log by its transaction ID, or null if not found.</summary>
    Task<RequestLog?> GetByTransactionIdAsync(string transactionId, CancellationToken ct = default);

    /// <summary>Returns a paged list of logs, newest first.</summary>
    Task<List<RequestLog>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default);

    /// <summary>Returns all logs for a given Windows user, newest first.</summary>
    Task<List<RequestLog>> GetByUserAsync(string windowsUser, int pageSize = 50, CancellationToken ct = default);

    /// <summary>Returns all logs within a UTC date range, newest first.</summary>
    Task<List<RequestLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default);

    /// <summary>Deletes logs older than the specified UTC cutoff. Returns the number of rows deleted.</summary>
    Task<int> PurgeOlderThanAsync(DateTime cutoffUtc, CancellationToken ct = default);
}
