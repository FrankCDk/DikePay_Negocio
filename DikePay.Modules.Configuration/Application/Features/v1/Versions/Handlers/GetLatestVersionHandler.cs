using DikePay.Modules.Configuration.Domain.Interfaces;
using DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Queries;
using MediatR;

namespace DikePay.Modules.Configuration.Application.Features.v1.Versions.Handlers
{
    public class GetLatestVersionHandler : IRequestHandler<GetLatestVersionQuery, VersionCheckResponse>
    {
        private readonly IConfigurationRepository _repository;

        public GetLatestVersionHandler(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<VersionCheckResponse> Handle(GetLatestVersionQuery request, CancellationToken ct)
        {
            // Buscamos la última versión activa para esa plataforma
            var latest = await _repository.GetLatestVersionAsync(request.Platform);

            if (latest == null || latest.BuildNumber <= request.CurrentBuildNumber)
            {
                return new VersionCheckResponse(false, false, string.Empty, 0, null, new());
            }

            return new VersionCheckResponse(
                UpdateAvailable: true,
                IsCritical: latest.IsCriticalUpdate,
                LatestVersionNumber: latest.VersionNumber,
                LatestBuildNumber: latest.BuildNumber,
                DownloadUrl: latest.DownloadUrl,
                ReleaseNotes: latest.ReleaseNotes.Select(n => n.Notes).ToList()
            );
        }
    }
}
