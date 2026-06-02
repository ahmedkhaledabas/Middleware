using MiddlewareCenter.Application.Interfaces;
using MiddlewareCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Persistence;

/// <summary>
/// EF Core implementation of <see cref="IRequestLogRepository"/>.
/// </summary>
public class RequestLogRepository : IRequestLogRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<RequestLogRepository> _logger;

    public RequestLogRepository(AppDbContext db, ILogger<RequestLogRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task AddAsync(RequestLog log, CancellationToken ct = default)
    {
        try
        {
            _db.RequestLogs.Add(log);
            await _db.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            // Log but never let persistence failures bubble up to the caller
            _logger.LogError(ex, "Failed to persist RequestLog for transaction {TransactionId}", log.TransactionId);
        }
    }

    public Task<RequestLog?> GetByTransactionIdAsync(string transactionId, CancellationToken ct = default) =>
        _db.RequestLogs
           .AsNoTracking()
           .FirstOrDefaultAsync(r => r.TransactionId == transactionId, ct);

    public Task<List<RequestLog>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken ct = default) =>
        _db.RequestLogs
           .AsNoTracking()
           .OrderByDescending(r => r.CreatedAt)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync(ct);

    public Task<List<RequestLog>> GetByUserAsync(string windowsUser, int pageSize = 50, CancellationToken ct = default) =>
        _db.RequestLogs
           .AsNoTracking()
           .Where(r => r.WindowsUser == windowsUser)
           .OrderByDescending(r => r.CreatedAt)
           .Take(pageSize)
           .ToListAsync(ct);

    public Task<List<RequestLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct = default) =>
        _db.RequestLogs
           .AsNoTracking()
           .Where(r => r.CreatedAt >= from && r.CreatedAt <= to)
           .OrderByDescending(r => r.CreatedAt)
           .ToListAsync(ct);

    public async Task<int> PurgeOlderThanAsync(DateTime cutoffUtc, CancellationToken ct = default)
    {
        var deleted = await _db.RequestLogs
            .Where(r => r.CreatedAt < cutoffUtc)
            .ExecuteDeleteAsync(ct);

        _logger.LogInformation("Purged {Count} request logs older than {Cutoff:O}", deleted, cutoffUtc);
        return deleted;
    }
}
