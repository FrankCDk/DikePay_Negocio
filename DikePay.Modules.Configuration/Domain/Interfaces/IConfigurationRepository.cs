using DikePay.Modules.Configuration.Domain.Enums;

namespace DikePay.Modules.Configuration.Domain.Interfaces
{
    public interface IConfigurationRepository
    {
        // Obtener la última versión activa para una plataforma específica
        Task<AppVersion?> GetLatestVersionAsync(AppPlatform platform);

        // Obtener todas las versiones (útil para un panel administrativo)
        Task<IEnumerable<AppVersion>> GetAllVersionsAsync();

        // Obtener una configuración global por su clave
        Task<GlobalSetting?> GetSettingByKeyAsync(string key);

        // Guardar cambios
        Task AddVersionAsync(AppVersion version);
    }
}
