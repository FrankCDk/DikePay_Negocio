using DikePay.Modules.Articulos.Application.Abstractions.Persistence;

namespace DikePay.Modules.Articulos.Infrastructure.Persistence
{
    public class ArticuloUnitOfWork : IArticuloUnitOfWork
    {
        private readonly ArticuloDbContext _context;

        public ArticuloUnitOfWork(ArticuloDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}
