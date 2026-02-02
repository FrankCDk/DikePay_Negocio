using DikePay.Modules.Configuration.Domain.Enums;
using DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Configuration.Shared.Contracts.v1.Queries
{
    public record GetLatestVersionQuery(
        AppPlatform Platform,
        int CurrentBuildNumber
    ) : IRequest<VersionCheckResponse>;
}
