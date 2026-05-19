using DikePay.Modules.Promotions.Application.Abstractions.Persistence;
using DikePay.Modules.Promotions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DikePay.Modules.Promotions.Infrastructure.Persistence
{
    public class PromotionsRepository : IPromotionsRepository
    {
        private readonly PromotionsDbContext _dbContext;
        public PromotionsRepository(PromotionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Promotion>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Promotions.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
