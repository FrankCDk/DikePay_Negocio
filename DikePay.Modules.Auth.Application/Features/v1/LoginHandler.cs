using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Domain;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserResponse?>
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public LoginHandler(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<UserResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar usuario
            var user = await _authRepository.GetUserByEmail(request.Email, cancellationToken);

            if (user == null) return null;

            // 2. Verificar password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid) return null; // Contraseña incorrecta

            var token = GenerateJwtToken(user);

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

        private string GenerateJwtToken(UserAccount user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Los "Claims" son la información que viaja dentro del token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Code),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Nombre", user.Name)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(8), // El token dura 8 horas
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
