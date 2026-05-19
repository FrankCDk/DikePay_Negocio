using DikePay.Modules.Configuration.Domain;
using DikePay.Modules.Configuration.Domain.Enums;
using DikePay.Modules.Configuration.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Configuration.Infrastructure.Persistence
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConfigurationDbContext _context;

        public ConfigurationRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        // Obtiene la última versión activa filtrada por plataforma y ordenada por BuildNumber
        public async Task<AppVersion?> GetLatestVersionAsync(AppPlatform platform)
        {
            return await _context.AppVersions
                .Include(v => v.ReleaseNotes) // Incluimos las notas por si MAUI las necesita
                .Where(v => v.Platform == platform && v.IsActive)
                .OrderByDescending(v => v.BuildNumber)
                .FirstOrDefaultAsync();
        }

        // Obtiene todas las versiones registradas
        public async Task<IEnumerable<AppVersion>> GetAllVersionsAsync()
        {
            return await _context.AppVersions
                .OrderByDescending(v => v.ReleaseDate)
                .ToListAsync();
        }

        // Busca una configuración global por su clave única
        public async Task<GlobalSetting?> GetSettingByKeyAsync(string key)
        {
            return await _context.GlobalSettings
                .FirstOrDefaultAsync(s => s.Key == key);
        }

        // Agrega una nueva versión a la base de datos
        public async Task AddVersionAsync(AppVersion version)
        {
            await _context.AppVersions.AddAsync(version);
        }
    }
}
