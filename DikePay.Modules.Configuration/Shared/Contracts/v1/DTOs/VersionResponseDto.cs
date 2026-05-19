namespace DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs
{
    public class VersionResponseDto
    {
        public string VersionServidor { get; set; } = string.Empty; // Ej: "1.2.5"
        public bool Obligatoria { get; set; } // ¿Bloqueamos la app hasta que actualice?
        public string UrlDescarga { get; set; } = string.Empty; // Link a la PlayStore o APK
        public string Novedades { get; set; } = string.Empty; // Qué hay de nuevo
        public DateTime FechaPublicacion { get; set; }
    }
}
