namespace DikePay.Modules.Auth.Shared.Contracts.v1.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty; // Útil para sesiones largas
        public DateTime Expiration { get; set; }
        public UserResponse User { get; set; } = new(); // Para mostrar el nombre en el Dashboard de inmediato
    }
}
