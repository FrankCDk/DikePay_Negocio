using MediatR;

namespace DikePay.Modules.Catalog.Shared.Contracts.v1.Commands
{
    public record CreateProductCommand(
        string Code,
        string Sku,
        string Name,
        decimal Price,
        decimal Stock,
        string Unit,
        string Currency
        ) : IRequest<Guid>;
}
