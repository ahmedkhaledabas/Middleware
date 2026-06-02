namespace MiddlewareCenter.Application.DTOs.SharePoint;

public class SharePointSiteDto
{
    public string siteId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    public string? description { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? lastModifiedAt { get; set; }
}

public class SharePointListDto
{
    public string listId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string? description { get; set; }
    public int itemCount { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? lastModifiedAt { get; set; }
}

public class SharePointItemDto
{
    public int itemId { get; set; }
    public string title { get; set; } = string.Empty;
    public Dictionary<string, object?> fields { get; set; } = new();
    public DateTime createdAt { get; set; }
    public DateTime? lastModifiedAt { get; set; }
    public string? createdBy { get; set; }
    public string? modifiedBy { get; set; }
}

public class SharePointFileDto
{
    public string fileId { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string serverRelativeUrl { get; set; } = string.Empty;
    public long sizeBytes { get; set; }
    public string? contentType { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime? lastModifiedAt { get; set; }
    public string? createdBy { get; set; }
    public string? modifiedBy { get; set; }
}
