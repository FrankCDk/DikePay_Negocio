using AutoMapper;
using DikePay.Modules.Configuration.Domain;
using DikePay.Modules.Configuration.Domain.Interfaces;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Commands;
using MediatR;

namespace DikePay.Modules.Configuration.Application.Features.v1.Versions.Handlers
{
    public class CreateVersionHandler : IRequestHandler<CreateVersionCommand, Guid>
    {
        private readonly IConfigurationUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateVersionHandler(IConfigurationUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateVersionCommand request, CancellationToken cancellationToken)
        {

            // 1. Mapeamos la entidad
            var entidad = _mapper.Map<AppVersion>(request);

            // 2. Agregamos la entidad al repositorio
            await _uow.Versions.AddVersionAsync(entidad);
            await _uow.SaveChangesAsync(cancellationToken);

            // 3. Retornamos el Id de la entidad creada
            return entidad.Id;
        }
    }
}
