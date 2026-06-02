using MiddlewareCenter.Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareCenter.API.Controllers;

/// <summary>
/// Base controller providing shared helpers for all API controllers.
/// </summary>
[ApiController]
[Authorize]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
    protected string TransactionId =>
        HttpContext.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString("N");

    protected string CurrentUser =>
        User?.Identity?.Name ?? "anonymous";

    protected ActionResult<ApiResponse<T>> OkResponse<T>(T data) =>
        Ok(ApiResponse<T>.Ok(data, TransactionId));

    protected ActionResult<ApiResponse<T>> ErrorResponse<T>(string message, int statusCode = 500)
    {
        var response = ApiResponse<T>.Fail(message, TransactionId);
        return StatusCode(statusCode, response);
    }
}
