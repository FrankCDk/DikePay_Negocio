using DikePay.Modules.Catalog.Application.Abstractions.Persistence;
using DikePay.Modules.Catalog.Domain;
using DikePay.Modules.Catalog.Shared.Contracts.v1.DTOs;
using DikePay.Modules.Catalog.Shared.Contracts.v1.Queries;
using MediatR;

namespace DikePay.Modules.Catalog.Application.Features.v1.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductResponse>>
    {

        private readonly IProductRepository _productRepository;
        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetAllAsync(cancellationToken);

            // Mapeamos de Entidad -> DTO
            return products.Select(p => new ProductResponse(
                p.Id,
                p.Code,
                p.Name,
                p.Price,
                p.Stock,
                p.Unit
            ));
        }
    }
}
