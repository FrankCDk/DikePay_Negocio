namespace DikePay.Shared.Models
{
    public class ApiError
    {
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string Field { get; init; } = string.Empty;
    }
}
