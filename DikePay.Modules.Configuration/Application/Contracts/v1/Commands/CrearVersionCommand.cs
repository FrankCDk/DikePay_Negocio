using DikePay.Modules.Configuracion.Domain.Enums;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Contracts.v1.Commands
{
    public record CrearVersionCommand(
        AppPlatform Plataforma,
        string NumeroVersion,
        int NumeroBuild,
        bool EsActualizacionCritica,
        string? UrlDescarga,
        bool EsActiva
        ) : IRequest<Guid>;
}
