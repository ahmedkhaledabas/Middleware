namespace MiddlewareCenter.API.Middleware;

/// <summary>
/// Assigns a unique transaction ID to every request and exposes it in the response header.
/// </summary>
public class TransactionIdMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionIdMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var transactionId = Guid.NewGuid().ToString("N");
        context.Items["TransactionId"] = transactionId;
        context.Response.Headers["X-Transaction-Id"] = transactionId;
        await _next(context);
    }
}
