using MiddlewareCenter.Application.DTOs.SharePoint;
using MiddlewareCenter.Domain.Interfaces;

namespace MiddlewareCenter.Application.Interfaces;

/// <summary>
/// SharePoint On-Premises integration client.
/// </summary>
public interface ISharePointApiClient : IExternalApiClient
{
    Task<List<SharePointSiteDto>> GetSitesAsync(CancellationToken ct = default);
    Task<List<SharePointListDto>> GetListsAsync(string siteId, CancellationToken ct = default);
    Task<List<SharePointItemDto>> GetListItemsAsync(string siteId, string listId, CancellationToken ct = default);
    Task<SharePointItemDto> CreateListItemAsync(string siteId, string listId, Dictionary<string, object> itemData, CancellationToken ct = default);
    Task<SharePointItemDto> UpdateListItemAsync(string siteId, string listId, int itemId, Dictionary<string, object> itemData, CancellationToken ct = default);
    Task<bool> DeleteListItemAsync(string siteId, string listId, int itemId, CancellationToken ct = default);
    Task<List<SharePointFileDto>> GetFilesAsync(string siteId, string folderUrl, CancellationToken ct = default);
    Task<byte[]> DownloadFileAsync(string siteId, string fileUrl, CancellationToken ct = default);
    Task<SharePointFileDto> UploadFileAsync(string siteId, string folderUrl, string fileName, byte[] fileContent, CancellationToken ct = default);
}
