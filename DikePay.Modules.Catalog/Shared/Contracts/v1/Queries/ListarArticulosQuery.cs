using DikePay.Modules.Articulos.Shared.Contracts.v1.DTOs;
using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Articulos.Shared.Contracts.v1.Queries
{
    public class ListarArticulosQuery : IRequest<ApiResponse<IEnumerable<ArticuloResponse>>>
    {
    }
}
