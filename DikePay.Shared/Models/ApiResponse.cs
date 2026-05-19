using System.Diagnostics;

namespace DikePay.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public List<ApiError> Errors { get; init; } = [];
        public string TraceId { get; init; } = Activity.Current?.Id ?? string.Empty;
        public static ApiResponse<T> Ok(T data, string message = "OK")
            => new()
            {
                Success = true,
                Data = data,
                Message = message,
                Code = "SUCCESS"
            };

        public static ApiResponse<T> Fail(string code, string message, List<ApiError>? errors = null)
            => new()
            {
                Success = false,
                Code = code,
                Message = message,
                Errors = errors ?? []
            };
    }
    
}
