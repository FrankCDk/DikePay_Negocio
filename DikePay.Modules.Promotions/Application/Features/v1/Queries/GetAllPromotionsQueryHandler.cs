using DikePay.Modules.Promociones.Application.Abstractions.Persistence;
using DikePay.Modules.Promociones.Application.Contracts.v1;
using DikePay.Modules.Promociones.Domain.Entities;
using MediatR;

namespace DikePay.Modules.Promociones.Application.Features.v1.Queries
{
    public class GetAllPromotionsQueryHandler : IRequestHandler<ListarPromocionesQuery, IEnumerable<Promocion>>
    {
        private IPromocionRepository _promotions;
        public GetAllPromotionsQueryHandler(IPromocionRepository promotions)
        {
            _promotions = promotions;
        }

        public async Task<IEnumerable<Promocion>> Handle(ListarPromocionesQuery request, CancellationToken cancellationToken)
        {
            // Podrías agregar lógica aquí para filtrar solo las vigentes si quisieras,
            // pero para una demo "GetAll" cumple su propósito.
            var results = await _promotions.ListarAsync(cancellationToken);

            return results ?? Enumerable.Empty<Promocion>();
        }
    }
}
