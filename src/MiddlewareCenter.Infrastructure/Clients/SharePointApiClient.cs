using MiddlewareCenter.Application.DTOs.SharePoint;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MiddlewareCenter.Infrastructure.Clients;

/// <summary>
/// SharePoint On-Premises REST API client implementation.
/// </summary>
public class SharePointApiClient : ExternalApiClientBase, ISharePointApiClient
{
    public SharePointApiClient(HttpClient httpClient, string baseUrl, ILogger<SharePointApiClient> logger)
        : base(httpClient, baseUrl, logger) { }

    public Task<List<SharePointSiteDto>> GetSitesAsync(CancellationToken ct = default)
        => GetAsync<List<SharePointSiteDto>>("sites", ct);

    public Task<List<SharePointListDto>> GetListsAsync(string siteId, CancellationToken ct = default)
        => GetAsync<List<SharePointListDto>>($"sites/{siteId}/lists", ct);

    public Task<List<SharePointItemDto>> GetListItemsAsync(string siteId, string listId, CancellationToken ct = default)
        => GetAsync<List<SharePointItemDto>>($"sites/{siteId}/lists/{listId}/items", ct);

    public Task<SharePointItemDto> CreateListItemAsync(string siteId, string listId, Dictionary<string, object> itemData, CancellationToken ct = default)
        => PostAsync<Dictionary<string, object>, SharePointItemDto>($"sites/{siteId}/lists/{listId}/items", itemData, ct);

    public Task<SharePointItemDto> UpdateListItemAsync(string siteId, string listId, int itemId, Dictionary<string, object> itemData, CancellationToken ct = default)
        => PutAsync<Dictionary<string, object>, SharePointItemDto>($"sites/{siteId}/lists/{listId}/items/{itemId}", itemData, ct);

    public Task<bool> DeleteListItemAsync(string siteId, string listId, int itemId, CancellationToken ct = default)
        => DeleteAsync<bool>($"sites/{siteId}/lists/{listId}/items/{itemId}", ct);

    public Task<List<SharePointFileDto>> GetFilesAsync(string siteId, string folderUrl, CancellationToken ct = default)
        => GetAsync<List<SharePointFileDto>>($"sites/{siteId}/files?folderUrl={Uri.EscapeDataString(folderUrl)}", ct);

    public Task<byte[]> DownloadFileAsync(string siteId, string fileUrl, CancellationToken ct = default)
        => GetAsync<byte[]>($"sites/{siteId}/files/download?fileUrl={Uri.EscapeDataString(fileUrl)}", ct);

    public Task<SharePointFileDto> UploadFileAsync(string siteId, string folderUrl, string fileName, byte[] fileContent, CancellationToken ct = default)
        => PostAsync<UploadPayload, SharePointFileDto>(
            $"sites/{siteId}/files/upload?folderUrl={Uri.EscapeDataString(folderUrl)}",
            new UploadPayload(fileName, Convert.ToBase64String(fileContent)),
            ct);

    private record UploadPayload(string fileName, string fileContentBase64);
}
