using DikePay.Modules.Promociones.Domain.Entities;
using MediatR;

namespace DikePay.Modules.Promociones.Application.Contracts.v1
{
    public record ListarPromocionesQuery() : IRequest<IEnumerable<Promocion>>;
    
}
