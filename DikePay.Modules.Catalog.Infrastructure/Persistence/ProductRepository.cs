using DikePay.Modules.Catalog.Application.Abstractions.Persistence;
using DikePay.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Catalog.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;
        public ProductRepository(CatalogDbContext context) => _context = context;


        public async Task AddAsync(Product articulo, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(articulo, cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product articulo)
        {
            _context.Products.Update(articulo);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(p => p.Code == code, cancellationToken);
        }
    }
}
