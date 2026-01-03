namespace DikePay.Modules.Auth.Domain
{
    public class UserAccount
    {
        public string Code { get; set; } = string.Empty;// Lo mapearemos a codigo_usuario
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Vendedor";
        public bool IsActive { get; set; }
    }
}
