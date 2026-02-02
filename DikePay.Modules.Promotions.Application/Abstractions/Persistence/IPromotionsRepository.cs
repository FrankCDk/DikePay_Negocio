using DikePay.Modules.Promotions.Domain.Entities;

namespace DikePay.Modules.Promotions.Application.Abstractions.Persistence
{
    public interface IPromotionsRepository
    {
        Task<List<Promotion>> GetAllAsync(CancellationToken cancellationToken);
    }
}
