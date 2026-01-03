namespace DikePay.Modules.Auth.Application.Abstractions.Persistence
{
    public interface IAuthUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
