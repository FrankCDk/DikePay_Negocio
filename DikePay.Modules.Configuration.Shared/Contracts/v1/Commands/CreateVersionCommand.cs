using DikePay.Modules.Configuration.Domain.Enums;
using MediatR;

namespace DikePay.Modules.Configuration.Shared.Contracts.v1.Commands
{
    public record CreateVersionCommand(
        AppPlatform Platform,
        string VersionNumber,
        int BuildNumber,
        int IsCritical,
        string DownloadUrl,
        int IsActive
        ) : IRequest<Guid>;
}
