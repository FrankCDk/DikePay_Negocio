using DikePay.Modules.Configuration.Domain.Interfaces;

namespace DikePay.Modules.Configuration.Infrastructure.Persistence
{
    public class ConfigurationUnitOfWork : IConfigurationUnitOfWork
    {
        private readonly ConfigurationDbContext _context;
        private IConfigurationRepository _versions;

        public ConfigurationUnitOfWork(ConfigurationDbContext context)
        {
            _context = context;
        }

        public IConfigurationRepository Versions =>
                    _versions ??= new ConfigurationRepository(_context);

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
