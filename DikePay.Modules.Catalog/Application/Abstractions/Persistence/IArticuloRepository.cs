using DikePay.Modules.Articulos.Domain;

namespace DikePay.Modules.Articulos.Application.Abstractions.Persistence
{
    public interface IArticuloRepository
    {
        Task CrearAsync(Articulo articulo, CancellationToken cancellationToken);
        Task ActualizarAsync(Articulo articulo);
        Task<bool> ExistePorCodigoAsync(string code, CancellationToken cancellationToken); // Buscamos el articulo por su codigo
        Task<bool> ExistePorSkuAsync(string sku, Guid excludeId, CancellationToken cancellationToken); 
        Task<List<Articulo>> ListarAsync(CancellationToken cancellationToken);
        Task<Articulo?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
