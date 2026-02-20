using MediatR;

namespace DikePay.Modules.Warehouses.Shared.Contracts.v1.Commands
{
    public record CreateWarehouseCommand() : IRequest<Guid>;
    
}
