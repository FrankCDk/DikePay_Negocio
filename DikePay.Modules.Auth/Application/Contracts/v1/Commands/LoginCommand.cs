using DikePay.Modules.Auth.Application.Contracts.v1.DTOs;
using DikePay.Shared.Models;
using MediatR;

namespace DikePay.Modules.Auth.Application.Contracts.v1.Commands
{
    public record LoginCommand(
        string Email, string Password) : IRequest<ApiResponse<UserResponse>>;
}
