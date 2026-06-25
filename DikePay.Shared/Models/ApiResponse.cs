using System.Diagnostics;

namespace DikePay.Shared.Models
{
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; init; }

        public static ApiResponse<T> Ok(T data, string message = "Operación realizada con éxito")
            => new()
            {
                Success = true,
                Message = message,
                Data = data,
                Code = "SUCCESS"
            };
    }

    // Esta clase base permite devolver respuestas sin necesidad de especificar un tipo T
    public class ApiResponse
    {
        public bool Success { get; init; }
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string TraceId { get; init; } = Activity.Current?.Id ?? string.Empty;
        public List<ApiError> Errors { get; init; } = [];

        // --- ESTE ES EL MÉTODO QUE BUSCAS ---
        // Ahora puedes hacer: ApiResponse.Ok("Producto creado")
        public static ApiResponse Ok(string message = "Operación realizada con éxito")
            => new()
            {
                Success = true,
                Message = message,
                Code = "SUCCESS"
            };

        public static ApiResponse Fail(string code, string message, List<ApiError>? errors = null)
            => new()
            {
                Success = false,
                Code = code,
                Message = message,
                Errors = errors ?? []
            };
    }

}
