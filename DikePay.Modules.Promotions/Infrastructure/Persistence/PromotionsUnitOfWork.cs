using DikePay.Modules.Promotions.Application.Abstractions.Persistence;

namespace DikePay.Modules.Promotions.Infrastructure.Persistence
{
    public class PromotionsUnitOfWork : IPromotionsUnitOfWork
    {
        private readonly PromotionsDbContext _context;

        public PromotionsUnitOfWork(PromotionsDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _context.SaveChangesAsync(ct);
    }
}
