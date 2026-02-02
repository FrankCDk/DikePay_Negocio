using DikePay.Modules.Configuration.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Configuration.Shared.Contracts.v1.Queries
{
    public class GetVersionQuery : IRequest<VersionResponseDto>
    {
    }
}
