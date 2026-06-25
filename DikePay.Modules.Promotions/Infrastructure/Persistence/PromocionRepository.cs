using DikePay.Modules.Promociones.Application.Abstractions.Persistence;
using DikePay.Modules.Promociones.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Promociones.Infrastructure.Persistence
{
    public class PromocionRepository : IPromocionRepository
    {
        private readonly PromocionDbContext _dbContext;
        public PromocionRepository(PromocionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Promocion>> ListarAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Promociones.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
