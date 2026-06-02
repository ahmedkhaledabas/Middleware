namespace MiddlewareCenter.Application.DTOs.ServiceDesk;

public class ServiceTicketDto
{
    public int ticketId { get; set; }
    public string subject { get; set; } = string.Empty;
    public string requester { get; set; } = string.Empty;
    public string? assignedTo { get; set; }
    public string category { get; set; } = string.Empty;
    public string priority { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public string? description { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? dueDate { get; set; }
    public DateTime? resolvedAt { get; set; }
    public List<ServiceTicketAttachment> attachments { get; set; } = new();
}

public class ServiceTicketAttachment
{
    public int attachmentId { get; set; }
    public string fileName { get; set; } = string.Empty;
    public string fileType { get; set; } = string.Empty;
    public long fileSizeBytes { get; set; }
    public string downloadUrl { get; set; } = string.Empty;
}

public class ServiceTicketHistoryDto
{
    public int historyId { get; set; }
    public int ticketId { get; set; }
    public string action { get; set; } = string.Empty;
    public string actionBy { get; set; } = string.Empty;
    public string? comment { get; set; }
    public DateTime actionDate { get; set; }
}

public class ServiceTicketResultDto
{
    public int ticketId { get; set; }
    public bool success { get; set; }
    public string action { get; set; } = string.Empty;
    public string? message { get; set; }
    public DateTime processedAt { get; set; }
}

public class TicketFilter
{
    public string? status { get; set; }
    public string? priority { get; set; }
    public string? category { get; set; }
    public string? assignedTo { get; set; }
    public DateTime? fromDate { get; set; }
    public DateTime? toDate { get; set; }
    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 20;
}
