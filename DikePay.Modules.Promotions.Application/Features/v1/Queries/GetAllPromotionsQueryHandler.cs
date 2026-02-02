using DikePay.Modules.Promotions.Application.Abstractions.Persistence;
using DikePay.Modules.Promotions.Domain.Entities;
using DikePay.Modules.Promotions.Shared.Contracts.v1;
using MediatR;

namespace DikePay.Modules.Promotions.Application.Features.v1.Queries
{
    public class GetAllPromotionsQueryHandler : IRequestHandler<GetAllPromotionsQuery, IEnumerable<Promotion>>
    {
        private IPromotionsRepository _promotions;
        public GetAllPromotionsQueryHandler(IPromotionsRepository promotions)
        {
            _promotions = promotions;
        }

        public async Task<IEnumerable<Promotion>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
        {
            // Podrías agregar lógica aquí para filtrar solo las vigentes si quisieras,
            // pero para una demo "GetAll" cumple su propósito.
            var results = await _promotions.GetAllAsync(cancellationToken);

            return results ?? Enumerable.Empty<Promotion>();
        }
    }
}
