namespace DikePay.Modules.Catalog.Application.Abstractions.Persistence
{
    public interface ICatalogUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
