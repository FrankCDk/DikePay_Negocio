using DikePay.Modules.Configuracion.Domain;
using DikePay.Modules.Configuracion.Domain.Enums;
using DikePay.Modules.Configuracion.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Configuracion.Infrastructure.Persistence
{
    public class ConfigurationRepository : IConfiguracionRepository
    {
        private readonly ConfigurationDbContext _context;

        public ConfigurationRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        // Obtiene la última versión activa filtrada por plataforma y ordenada por BuildNumber
        public async Task<VersionApp?> ObtenerUltimaVersionAsync(AppPlatform platform)
        {
            return await _context.VersionApp
                .Include(v => v.Notas) // Incluimos las notas por si MAUI las necesita
                .Where(v => v.Plataforma == platform && v.EsActiva)
                .OrderByDescending(v => v.NumeroBuild)
                .FirstOrDefaultAsync();
        }

        // Obtiene todas las versiones registradas
        public async Task<IEnumerable<VersionApp>> ListarVersionesAsync()
        {
            return await _context.VersionApp
                .OrderByDescending(v => v.FechaLanzamiento)
                .ToListAsync();
        }

        // Busca una configuración global por su clave única
        public async Task<ConfiguracionGlobal?> ObtenerConfiguracionPorClaveAsync(string key)
        {
            return await _context.ConfiguracionesGlobales
                .FirstOrDefaultAsync(s => s.Clave == key);
        }

        // Agrega una nueva versión a la base de datos
        public async Task CrearVersionAsync(VersionApp version)
        {
            await _context.VersionApp.AddAsync(version);
        }
    }
}
