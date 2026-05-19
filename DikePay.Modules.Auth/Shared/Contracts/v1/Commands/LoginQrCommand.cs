using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Shared.Contracts.v1.Commands
{
    public record LoginQrCommand(string AuthCode, string DeviceName) : IRequest<AuthResponse?>;
}
