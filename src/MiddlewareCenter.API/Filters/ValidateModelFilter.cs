using MiddlewareCenter.Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareCenter.API.Filters;

/// <summary>
/// Action filter that short-circuits the pipeline with a 400 response
/// when model binding validation fails, using the standard <see cref="ApiResponse{T}"/> envelope.
/// Register globally via <c>AddControllers(o => o.Filters.Add&lt;ValidateModelFilter&gt;())</c>.
/// </summary>
public class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var transactionId = context.HttpContext.Items["TransactionId"]?.ToString()
                            ?? Guid.NewGuid().ToString("N");

        var errors = context.ModelState
            .Where(kv => kv.Value?.Errors.Count > 0)
            .SelectMany(kv => kv.Value!.Errors.Select(e =>
                string.IsNullOrWhiteSpace(e.ErrorMessage)
                    ? e.Exception?.Message ?? "Validation error."
                    : e.ErrorMessage))
            .ToList();

        var message = string.Join(" | ", errors);
        var response = ApiResponse<object>.Fail(message, transactionId);

        context.Result = new BadRequestObjectResult(response);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
