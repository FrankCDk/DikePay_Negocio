using DikePay.Modules.Catalog.Application.Abstractions.Persistence;

namespace DikePay.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogUnitOfWork : ICatalogUnitOfWork
    {
        private readonly CatalogDbContext _context;

        public CatalogUnitOfWork(CatalogDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}
