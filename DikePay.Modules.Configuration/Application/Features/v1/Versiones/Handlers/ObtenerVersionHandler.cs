using DikePay.Modules.Configuracion.Application.Contracts.v1.DTOs;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Queries;
using DikePay.Modules.Configuracion.Domain.Interfaces;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Features.v1.Versions.Handlers
{
    public class ObtenerVersionHandler : IRequestHandler<ObtenerVersionQuery, VersionRespuestaDto>
    {

        private readonly IConfiguracionRepository _version;

        public ObtenerVersionHandler(IConfiguracionRepository configuration)
        {
            _version = configuration;
        }

        public async Task<VersionRespuestaDto> Handle(ObtenerVersionQuery request, CancellationToken cancellationToken)
        {
            var response = new VersionRespuestaDto();



            return response;
        }
    }
}
