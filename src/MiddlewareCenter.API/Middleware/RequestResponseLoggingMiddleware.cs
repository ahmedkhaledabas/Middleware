using System.Diagnostics;
using System.Text;
using Microsoft.IO;

namespace MiddlewareCenter.API.Middleware;

/// <summary>
/// Logs all incoming requests and outgoing responses with timing information.
/// </summary>
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private static readonly RecyclableMemoryStreamManager _streamManager = new();

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var transactionId = context.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString();
        var stopwatch = Stopwatch.StartNew();

        var windowsUser = context.User?.Identity?.Name ?? "anonymous";
        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryString = context.Request.QueryString.ToString();

        // Log request
        _logger.LogInformation(
            "[{TransactionId}] → {Method} {Path}{QueryString} | User: {User} | IP: {IP}",
            transactionId, method, path, queryString, windowsUser,
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown");

        // Capture response
        var originalBodyStream = context.Response.Body;
        await using var responseBody = _streamManager.GetStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            responseBody.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation(
                "[{TransactionId}] ← {StatusCode} | {ElapsedMs}ms | User: {User}",
                transactionId, context.Response.StatusCode, stopwatch.ElapsedMilliseconds, windowsUser);

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
