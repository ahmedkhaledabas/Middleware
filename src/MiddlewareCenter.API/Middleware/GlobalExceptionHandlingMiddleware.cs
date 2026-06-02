using System.Net;
using System.Text.Json;
using MiddlewareCenter.Application.DTOs.Common;

namespace MiddlewareCenter.API.Middleware;

/// <summary>
/// Catches all unhandled exceptions and returns a generic error response.
/// Detailed errors are logged internally only.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var transactionId = context.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString();

            _logger.LogError(ex,
                "[{TransactionId}] Unhandled exception on {Method} {Path}",
                transactionId, context.Request.Method, context.Request.Path);

            await WriteErrorResponseAsync(context, ex, transactionId);
        }
    }

    private static async Task WriteErrorResponseAsync(HttpContext context, Exception ex, string transactionId)
    {
        context.Response.ContentType = "application/json";

        (int statusCode, string message) = ex switch
        {
            HttpRequestException httpEx => ((int)(httpEx.StatusCode ?? HttpStatusCode.BadGateway),
                "An error occurred while communicating with an external service."),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access."),
            ArgumentException or ArgumentNullException => (StatusCodes.Status400BadRequest, "Invalid request parameters."),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "The requested resource was not found."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.")
        };

        context.Response.StatusCode = statusCode;

        var response = ApiResponse<object>.Fail(message, transactionId);
        var json = JsonSerializer.Serialize(response, _jsonOptions);
        await context.Response.WriteAsync(json);
    }
}
