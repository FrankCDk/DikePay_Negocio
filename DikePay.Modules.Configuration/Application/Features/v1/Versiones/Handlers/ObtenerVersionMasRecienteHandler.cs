using DikePay.Modules.Configuracion.Application.Contracts.v1.DTOs;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Queries;
using DikePay.Modules.Configuracion.Domain.Interfaces;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Features.v1.Versions.Handlers
{
    public class ObtenerVersionMasRecienteHandler : IRequestHandler<ObtenerVersionMasRecienteQuery, RespuestaVerificacionVersion>
    {
        private readonly IConfiguracionRepository _repository;

        public ObtenerVersionMasRecienteHandler(IConfiguracionRepository repository)
        {
            _repository = repository;
        }

        public async Task<RespuestaVerificacionVersion> Handle(ObtenerVersionMasRecienteQuery request, CancellationToken ct)
        {
            // Buscamos la última versión activa para esa plataforma
            var ultimaVersion = await _repository.ObtenerUltimaVersionAsync(request.Plataforma);

            // Si no hay versión o la actual es igual o mayor, no hay actualización
            if (ultimaVersion == null || ultimaVersion.NumeroBuild <= request.NumeroBuildActual)
            {
                return new RespuestaVerificacionVersion(
                    ActualizacionDisponible: false,
                    EsCritica: false,
                    NumeroVersionActual: string.Empty,
                    NumeroBuildActual: 0,
                    UrlDescarga: null,
                    NotasLanzamiento: new()
                );
            }

            // Si hay una versión superior, retornamos la respuesta con los nombres en español
            return new RespuestaVerificacionVersion(
                ActualizacionDisponible: true,
                EsCritica: ultimaVersion.EsActualizacionCritica,
                NumeroVersionActual: ultimaVersion.NumeroVersion,
                NumeroBuildActual: ultimaVersion.NumeroBuild,
                UrlDescarga: ultimaVersion.UrlDescarga,
                NotasLanzamiento: ultimaVersion.Notas.Select(n => n.Notas).ToList()
            );
        }
    }
}