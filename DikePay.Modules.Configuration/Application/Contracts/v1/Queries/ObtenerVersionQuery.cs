using DikePay.Modules.Configuracion.Application.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Contracts.v1.Queries
{
    public class ObtenerVersionQuery : IRequest<VersionRespuestaDto>
    {
    }
}
