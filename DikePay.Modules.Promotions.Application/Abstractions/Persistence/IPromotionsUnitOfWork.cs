namespace DikePay.Modules.Promotions.Application.Abstractions.Persistence
{
    public interface IPromotionsUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
