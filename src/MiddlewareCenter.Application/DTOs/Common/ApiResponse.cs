namespace MiddlewareCenter.Application.DTOs.Common;

/// <summary>
/// Standard API response wrapper for all endpoints.
/// </summary>
public class ApiResponse<T>
{
    public bool success { get; set; }
    public string transaction_id { get; set; } = string.Empty;
    public string? error_message { get; set; }
    public T? data { get; set; }

    public static ApiResponse<T> Ok(T data, string transactionId) => new()
    {
        success = true,
        transaction_id = transactionId,
        data = data
    };

    public static ApiResponse<T> Fail(string errorMessage, string transactionId) => new()
    {
        success = false,
        transaction_id = transactionId,
        error_message = errorMessage
    };
}
