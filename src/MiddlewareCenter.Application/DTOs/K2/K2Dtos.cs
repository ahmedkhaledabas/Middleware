namespace MiddlewareCenter.Application.DTOs.K2;

public class MemoDto
{
    public int memoId { get; set; }
    public string subject { get; set; } = string.Empty;
    public string fromUser { get; set; } = string.Empty;
    public string? toUser { get; set; }
    public string? body { get; set; }
    public string status { get; set; } = string.Empty;
    public string priority { get; set; } = string.Empty;
    public DateTime createdAt { get; set; }
    public DateTime? dueDate { get; set; }
    public List<MemoAttachment> attachments { get; set; } = new();
}

public class MemoAttachment
{
    public int attachmentId { get; set; }
    public string fileName { get; set; } = string.Empty;
    public string fileType { get; set; } = string.Empty;
    public long fileSizeBytes { get; set; }
    public string downloadUrl { get; set; } = string.Empty;
}

public class MemoHistoryDto
{
    public int historyId { get; set; }
    public int memoId { get; set; }
    public string subject { get; set; } = string.Empty;
    public string action { get; set; } = string.Empty;
    public string actionBy { get; set; } = string.Empty;
    public string? comment { get; set; }
    public DateTime actionDate { get; set; }
}

public class MemoResultDto
{
    public int memoId { get; set; }
    public bool success { get; set; }
    public string action { get; set; } = string.Empty;
    public string? message { get; set; }
    public DateTime processedAt { get; set; }
}
