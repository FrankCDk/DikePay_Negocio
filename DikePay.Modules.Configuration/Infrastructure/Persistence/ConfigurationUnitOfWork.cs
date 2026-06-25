using DikePay.Modules.Configuracion.Domain.Interfaces;

namespace DikePay.Modules.Configuracion.Infrastructure.Persistence
{
    public class ConfigurationUnitOfWork : IConfiguracionUnitOfWork
    {
        private readonly ConfigurationDbContext _context;
        private IConfiguracionRepository _versiones;

        public ConfigurationUnitOfWork(ConfigurationDbContext context)
        {
            _context = context;
        }

        public IConfiguracionRepository Versiones =>
                    _versiones ??= new ConfigurationRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
