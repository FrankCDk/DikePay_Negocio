using DikePay.Modules.Configuration.Domain.Interfaces;
using DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Queries;
using MediatR;

namespace DikePay.Modules.Configuration.Application.Features.v1.Versions.Handlers
{
    public class GetVersionHandler : IRequestHandler<GetVersionQuery, VersionResponseDto>
    {

        private readonly IConfigurationRepository _version;

        public GetVersionHandler(IConfigurationRepository configuration)
        {
            _version = configuration;
        }

        public async Task<VersionResponseDto> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            var response = new VersionResponseDto();



            return response;
        }
    }
}
