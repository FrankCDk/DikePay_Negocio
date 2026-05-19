using DikePay.Modules.Promotions.Domain.Entities;
using MediatR;

namespace DikePay.Modules.Promotions.Shared.Contracts.v1
{
    public record GetAllPromotionsQuery() : IRequest<IEnumerable<Promotion>>;
    
}
