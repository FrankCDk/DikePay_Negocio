namespace DikePay.Modules.Articulos.Application.Abstractions.Persistence
{
    public interface IArticuloUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
