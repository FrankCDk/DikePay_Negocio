using DikePay.Modules.Catalog.Application.Abstractions.Persistence;
using DikePay.Modules.Catalog.Domain;
using DikePay.Modules.Catalog.Shared.Contracts.v1.Commands;
using MediatR;

namespace DikePay.Modules.Catalog.Application.Features.v1.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly ICatalogUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public CreateProductHandler(ICatalogUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Sku = request.Sku,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Unit = request.Unit,
                Currency = request.Currency,
                CreatedAt = DateTime.UtcNow,
                Status = "V"
            };

            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Id;

        }
    }
}
