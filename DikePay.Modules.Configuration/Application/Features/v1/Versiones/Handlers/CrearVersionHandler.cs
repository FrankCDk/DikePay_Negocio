using AutoMapper;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Commands;
using DikePay.Modules.Configuracion.Domain;
using DikePay.Modules.Configuracion.Domain.Interfaces;
using MediatR;

namespace DikePay.Modules.Configuracion.Application.Features.v1.Versions.Handlers
{
    public class CrearVersionHandler : IRequestHandler<CrearVersionCommand, Guid>
    {
        private readonly IConfiguracionUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CrearVersionHandler(IConfiguracionUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CrearVersionCommand request, CancellationToken cancellationToken)
        {

            // 1. Mapeamos la entidad
            var entidad = _mapper.Map<VersionApp>(request);

            // 2. Agregamos la entidad al repositorio
            await _uow.Versiones.CrearVersionAsync(entidad);
            await _uow.SaveChangesAsync(cancellationToken);

            // 3. Retornamos el Id de la entidad creada
            return entidad.Id;
        }
    }
}
