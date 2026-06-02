using MiddlewareCenter.Application.DTOs.Common;
using MiddlewareCenter.Application.DTOs.SharePoint;
using MiddlewareCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers.v1;

/// <summary>
/// SharePoint On-Premises integration endpoints.
/// </summary>
[ApiController]
[Route("api/v1/sharepoint")]
public class SharePointController : ApiControllerBase
{
    private readonly ISharePointApiClient _client;
    private readonly ILogger<SharePointController> _logger;

    public SharePointController(ISharePointApiClient client, ILogger<SharePointController> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>Gets all SharePoint sites.</summary>
    [HttpGet("sites")]
    [ProducesResponseType(typeof(ApiResponse<List<SharePointSiteDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<SharePointSiteDto>>>> GetSites(CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetSites", TransactionId, CurrentUser);
        return OkResponse(await _client.GetSitesAsync(ct));
    }

    /// <summary>Gets lists for a site.</summary>
    [HttpGet("sites/{siteId}/lists")]
    [ProducesResponseType(typeof(ApiResponse<List<SharePointListDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<SharePointListDto>>>> GetLists(string siteId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetLists {SiteId}", TransactionId, CurrentUser, siteId);
        return OkResponse(await _client.GetListsAsync(siteId, ct));
    }

    /// <summary>Gets items from a list.</summary>
    [HttpGet("sites/{siteId}/lists/{listId}/items")]
    [ProducesResponseType(typeof(ApiResponse<List<SharePointItemDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<SharePointItemDto>>>> GetListItems(string siteId, string listId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetListItems {SiteId}/{ListId}", TransactionId, CurrentUser, siteId, listId);
        return OkResponse(await _client.GetListItemsAsync(siteId, listId, ct));
    }

    /// <summary>Creates a new list item.</summary>
    [HttpPost("sites/{siteId}/lists/{listId}/items")]
    [ProducesResponseType(typeof(ApiResponse<SharePointItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<SharePointItemDto>>> CreateListItem(
        string siteId, string listId, [FromBody] Dictionary<string, object> itemData, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → CreateListItem {SiteId}/{ListId}", TransactionId, CurrentUser, siteId, listId);
        return OkResponse(await _client.CreateListItemAsync(siteId, listId, itemData, ct));
    }

    /// <summary>Updates an existing list item.</summary>
    [HttpPut("sites/{siteId}/lists/{listId}/items/{itemId:int}")]
    [ProducesResponseType(typeof(ApiResponse<SharePointItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<SharePointItemDto>>> UpdateListItem(
        string siteId, string listId, int itemId, [FromBody] Dictionary<string, object> itemData, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → UpdateListItem {SiteId}/{ListId}/{ItemId}", TransactionId, CurrentUser, siteId, listId, itemId);
        return OkResponse(await _client.UpdateListItemAsync(siteId, listId, itemId, itemData, ct));
    }

    /// <summary>Deletes a list item.</summary>
    [HttpDelete("sites/{siteId}/lists/{listId}/items/{itemId:int}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteListItem(
        string siteId, string listId, int itemId, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → DeleteListItem {SiteId}/{ListId}/{ItemId}", TransactionId, CurrentUser, siteId, listId, itemId);
        return OkResponse(await _client.DeleteListItemAsync(siteId, listId, itemId, ct));
    }

    /// <summary>Gets files in a folder.</summary>
    [HttpGet("sites/{siteId}/files")]
    [ProducesResponseType(typeof(ApiResponse<List<SharePointFileDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<SharePointFileDto>>>> GetFiles(
        string siteId, [FromQuery] string folderUrl, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → GetFiles {SiteId}", TransactionId, CurrentUser, siteId);
        return OkResponse(await _client.GetFilesAsync(siteId, folderUrl, ct));
    }

    /// <summary>Downloads a file.</summary>
    [HttpGet("sites/{siteId}/files/download")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> DownloadFile(string siteId, [FromQuery] string fileUrl, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → DownloadFile {SiteId}", TransactionId, CurrentUser, siteId);
        var bytes = await _client.DownloadFileAsync(siteId, fileUrl, ct);
        var fileName = Path.GetFileName(fileUrl);
        return File(bytes, "application/octet-stream", fileName);
    }

    /// <summary>Uploads a file.</summary>
    [HttpPost("sites/{siteId}/files/upload")]
    [ProducesResponseType(typeof(ApiResponse<SharePointFileDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<SharePointFileDto>>> UploadFile(
        string siteId, [FromQuery] string folderUrl, IFormFile file, CancellationToken ct)
    {
        _logger.LogInformation("[{TxId}] {User} → UploadFile {SiteId} {FileName}", TransactionId, CurrentUser, siteId, file.FileName);
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, ct);
        var result = await _client.UploadFileAsync(siteId, folderUrl, file.FileName, ms.ToArray(), ct);
        return OkResponse(result);
    }
}
