namespace DikePay.Modules.Configuracion.Domain.Interfaces
{
    public interface IConfiguracionUnitOfWork
    {
        IConfiguracionRepository Versiones { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
