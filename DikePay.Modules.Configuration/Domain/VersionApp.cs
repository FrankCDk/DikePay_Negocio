using DikePay.Modules.Configuracion.Domain.Enums;

namespace DikePay.Modules.Configuracion.Domain
{
    public class VersionApp
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Nota: Usé Guid.NewGuid() para mejor práctica
        public AppPlatform Plataforma { get; set; }
        public string NumeroVersion { get; set; } = string.Empty;
        public int NumeroBuild { get; set; }
        public bool EsActualizacionCritica { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string? UrlDescarga { get; set; }
        public bool EsActiva { get; set; }

        // Propiedad de navegación renombrada a español
        public ICollection<NotasLanzamiento> Notas { get; set; } = new List<NotasLanzamiento>();
    }
}
