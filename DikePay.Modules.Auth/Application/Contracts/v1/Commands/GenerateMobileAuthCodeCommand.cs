using DikePay.Modules.Auth.Application.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Application.Contracts.v1.Commands
{
    public record GenerateMobileAuthCodeCommand(string UserId) : IRequest<ServiceResponse<string>>;
}
