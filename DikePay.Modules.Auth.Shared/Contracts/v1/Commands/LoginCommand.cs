using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Shared.Contracts.v1.Commands
{
    public record LoginCommand(
        string Email, string Password) : IRequest<UserResponse?>;
}
