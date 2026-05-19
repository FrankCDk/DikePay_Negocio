using DikePay.Modules.Catalog.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Catalog.Shared.Contracts.v1.Queries
{
    public class GetProductQuery : IRequest<IEnumerable<ProductResponse>>
    {
    }
}
