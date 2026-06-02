namespace MiddlewareCenter.Application.DTOs.Common;

/// <summary>
/// Generic approval action request.
/// </summary>
public class ApproveRequest
{
    public string? Comment { get; set; }
    public string? ApprovedBy { get; set; }
}

/// <summary>
/// Action request for memo/ticket operations.
/// </summary>
public class MemoActionRequest
{
    public string? Comment { get; set; }
    public string? ActionBy { get; set; }
}

/// <summary>
/// Action request for ticket operations.
/// </summary>
public class TicketActionRequest
{
    public string? Comment { get; set; }
    public string? ActionBy { get; set; }
}
