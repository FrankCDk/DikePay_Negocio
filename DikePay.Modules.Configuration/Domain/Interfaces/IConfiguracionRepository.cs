using DikePay.Modules.Configuracion.Domain.Enums;

namespace DikePay.Modules.Configuracion.Domain.Interfaces
{
    public interface IConfiguracionRepository
    {
        // Obtener la última versión activa para una plataforma específica
        Task<VersionApp?> ObtenerUltimaVersionAsync(AppPlatform platform);

        // Obtener todas las versiones (útil para un panel administrativo)
        Task<IEnumerable<VersionApp>> ListarVersionesAsync();

        // Obtener una configuración global por su clave
        Task<ConfiguracionGlobal?> ObtenerConfiguracionPorClaveAsync(string key);

        // Guardar cambios
        Task CrearVersionAsync(VersionApp version);
    }
}
