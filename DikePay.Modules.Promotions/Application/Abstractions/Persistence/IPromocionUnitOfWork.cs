namespace DikePay.Modules.Promociones.Application.Abstractions.Persistence
{
    public interface IPromocionUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
