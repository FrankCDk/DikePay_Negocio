namespace DikePay.Modules.Auth.Domain
{
    public class MobileAuthCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;

        // Debe coincidir con el tipo de 'codigo_usuario' en tu tabla usuarios
        public string UserId { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }

        // Propiedad de navegación (VITAL para el Include)
        public virtual UserAccount User { get; set; } = null!;
    }
}
