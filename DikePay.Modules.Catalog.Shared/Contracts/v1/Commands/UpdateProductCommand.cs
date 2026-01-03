using MediatR;

namespace DikePay.Modules.Catalog.Shared.Contracts.v1.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        string Code,
        string Sku,
        string Name,
        decimal Price,
        decimal Stock,
        string Unit,
        string Currency
        ) : IRequest<Guid>;
}
