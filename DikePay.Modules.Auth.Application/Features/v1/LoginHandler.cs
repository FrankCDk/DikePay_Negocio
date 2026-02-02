using DikePay.Modules.Auth.Application.Abstractions.Interfaces;
using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserResponse?>
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

        public async Task<UserResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar usuario
            var user = await _authRepository.GetUserByEmail(request.Email, cancellationToken);

            if (user == null) return null;

            // 2. Verificar password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid) return null; // Contraseña incorrecta

            var token = _tokenService.GenerateJwtToken(user);

            return new UserResponse
            {
                Code = user.Code,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                State = user.IsActive,
                Token = token
            };

        }

        public async Task<bool> VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        
    }
}
