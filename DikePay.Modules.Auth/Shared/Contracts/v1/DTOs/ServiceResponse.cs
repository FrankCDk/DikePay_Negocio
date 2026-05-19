namespace DikePay.Modules.Auth.Shared.Contracts.v1.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }

        // Métodos estáticos para facilitar el retorno (Senior Style)
        public static ServiceResponse<T> Fail(string message)
            => new() { Success = false, Message = message };

        public static ServiceResponse<T> Ok(T data)
            => new() { Data = data, Success = true };
    }
}
