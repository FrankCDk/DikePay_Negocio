using DikePay.Modules.Promociones.Infrastructure.Persistence;
using DikePay.Modules.Promociones.Application.Abstractions.Persistence;

namespace DikePay.Modules.Promociones.Infrastructure.Persistence
{
    public class PromocionUnitOfWork : IPromocionUnitOfWork
    {
        private readonly PromocionDbContext _context;

        public PromocionUnitOfWork(PromocionDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}
