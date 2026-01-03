using DikePay.Modules.Catalog.Domain;

namespace DikePay.Modules.Catalog.Application.Abstractions.Persistence
{
    public interface IProductRepository
    {
        Task AddAsync(Product articulo, CancellationToken cancellationToken);
        Task UpdateAsync(Product articulo);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
    }
}
