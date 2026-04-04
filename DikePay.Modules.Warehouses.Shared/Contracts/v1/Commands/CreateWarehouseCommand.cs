using MediatR;

namespace DikePay.Modules.Warehouses.Shared.Contracts.v1.Commands
{
    public record CreateWarehouseCommand(
        string Code,
        string Name,
        string Description,
        string Address,

        ) : IRequest<Guid>;
    
}
