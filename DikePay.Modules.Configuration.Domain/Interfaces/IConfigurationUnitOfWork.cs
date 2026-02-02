namespace DikePay.Modules.Configuration.Domain.Interfaces
{
    public interface IConfigurationUnitOfWork
    {
        IConfigurationRepository Versions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
