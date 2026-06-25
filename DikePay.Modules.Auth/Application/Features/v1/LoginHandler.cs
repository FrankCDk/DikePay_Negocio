using DikePay.Modules.Auth.Application.Abstractions.Interfaces;
using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Application.Contracts.v1.Commands;
using DikePay.Modules.Auth.Application.Contracts.v1.DTOs;
using DikePay.Shared.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class LoginHandler : IRequestHandler<LoginCommand, ApiResponse<UserResponse>>
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public LoginHandler(IAuthRepository authRepository, IConfiguration configuration, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<UserResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar usuario
            var user = await _authRepository.GetUserByEmail(request.Email, cancellationToken);

            if (user == null)
            {
                throw new Exception("AUTH_001: El usuario no existe.");
            }

            if (!user.IsActive)
            {
                throw new Exception("AUTH_02: El usuario se encuentra inactivo");
            }

            // 2. Verificar password
            bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("AUTH_003: Contraseña incorrecta.");
            }

            var token = _tokenService.GenerateJwtToken(user);

            var responseData = new UserResponse
            {
                Code = user.Code,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                State = user.IsActive,
                Token = token
            };

            return ApiResponse<UserResponse>.Ok(responseData, "Login exitoso.");
        }

    }
}
