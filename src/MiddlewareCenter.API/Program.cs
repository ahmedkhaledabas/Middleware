using System.Threading.RateLimiting;
using MiddlewareCenter.API.Extensions;
using MiddlewareCenter.API.Middleware;
using MiddlewareCenter.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ── Controllers + Validation Filter ──────────────────────────────────────────
builder.Services.AddApiControllers();

// ── Windows Authentication ───────────────────────────────────────────────────
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// ── CORS ─────────────────────────────────────────────────────────────────────
builder.Services.AddApiCors(configuration);

// ── Rate Limiting ─────────────────────────────────────────────────────────────
var permitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit", 100);
var queueLimit  = configuration.GetValue<int>("RateLimiting:QueueLimit", 20);

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var user = context.User?.Identity?.Name
                   ?? context.Connection.RemoteIpAddress?.ToString()
                   ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(user, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = permitLimit,
            QueueLimit  = queueLimit,
            Window      = TimeSpan.FromMinutes(1)
        });
    });

    options.OnRejected = async (ctx, ct) =>
    {
        ctx.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await ctx.HttpContext.Response.WriteAsJsonAsync(new
        {
            success        = false,
            transaction_id = ctx.HttpContext.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString("N"),
            error_message  = "Too many requests. Please slow down."
        }, ct);
    };
});

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddApiSwagger();

// ── Health Checks ─────────────────────────────────────────────────────────────
builder.Services.AddHealthChecks();

// ── Infrastructure (DbContext, Repositories, External API Clients) ────────────
builder.Services.AddInfrastructure(configuration);

// ── Logging ───────────────────────────────────────────────────────────────────
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ─────────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Middleware Pipeline ───────────────────────────────────────────────────────
app.UseMiddleware<TransactionIdMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareCenter API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseCors("MiddlewareCenterPolicy");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
