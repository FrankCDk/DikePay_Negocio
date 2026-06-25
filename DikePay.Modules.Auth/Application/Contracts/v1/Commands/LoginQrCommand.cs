using DikePay.Modules.Auth.Application.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Application.Contracts.v1.Commands
{
    public record LoginQrCommand(string AuthCode, string DeviceName) : IRequest<AuthResponse?>;
}
