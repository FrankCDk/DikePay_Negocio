using DikePay.Modules.Articulos.Application.Abstractions.Persistence;
using DikePay.Modules.Articulos.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Articulos.Infrastructure.Persistence
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ArticuloDbContext _context;
        public ArticuloRepository(ArticuloDbContext context) => _context = context;


        public async Task CrearAsync(Articulo articulo, CancellationToken cancellationToken)
        {
            await _context.Articulos.AddAsync(articulo, cancellationToken);
        }

        public async Task<List<Articulo>> ListarAsync(CancellationToken cancellationToken)
        {
            return await _context.Articulos.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task ActualizarAsync(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistePorCodigoAsync(string code, CancellationToken cancellationToken)
        {
            return await _context.Articulos.AnyAsync(p => p.Codigo == code, cancellationToken);
        }

        public async Task<bool> ExistePorSkuAsync(string sku, Guid excludeId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(sku)) return false;

            return await _context.Articulos.AnyAsync(
                p => p.CodigoSku == sku && p.Id != excludeId,
                cancellationToken
            );
        }

        public async Task<Articulo?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Articulos.FindAsync(new object[] { id }, cancellationToken);
        }
    }
}
