using DikePay.Modules.Promociones.Domain.Entities;

namespace DikePay.Modules.Promociones.Application.Abstractions.Persistence
{
    public interface IPromocionRepository
    {
        Task<List<Promocion>> ListarAsync(CancellationToken cancellationToken);
    }
}
