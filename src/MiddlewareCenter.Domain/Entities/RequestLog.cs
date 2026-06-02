using MiddlewareCenter.Domain.Common;

namespace MiddlewareCenter.Domain.Entities;

/// <summary>
/// Represents a logged HTTP request/response pair.
/// </summary>
public class RequestLog : BaseEntity
{
    public string TransactionId { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string? QueryString { get; set; }
    public string? RequestBody { get; set; }
    public int StatusCode { get; set; }
    public string? ResponseBody { get; set; }
    public long ElapsedMilliseconds { get; set; }
    public string? WindowsUser { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? ErrorMessage { get; set; }
}
