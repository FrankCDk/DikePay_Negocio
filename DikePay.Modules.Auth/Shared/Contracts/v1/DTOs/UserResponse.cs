namespace DikePay.Modules.Auth.Shared.Contracts.v1.DTOs
{
    public class UserResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool State { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
