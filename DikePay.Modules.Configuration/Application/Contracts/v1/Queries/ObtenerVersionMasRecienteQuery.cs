using DikePay.Modules.Configuracion.Application.Contracts.v1.DTOs;
using DikePay.Modules.Configuracion.Domain.Enums;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Contracts.v1.Queries
{
    public record ObtenerVersionMasRecienteQuery(
        AppPlatform Plataforma,
        int NumeroBuildActual
    ) : IRequest<RespuestaVerificacionVersion>;
}
