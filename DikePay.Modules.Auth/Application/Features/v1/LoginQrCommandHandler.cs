using DikePay.Modules.Auth.Application.Abstractions.Interfaces;
using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class LoginQrCommandHandler : IRequestHandler<LoginQrCommand, AuthResponse?>
    {
        private readonly ITokenService _tokenService; // Tu servicio que genera JWT
        private readonly IAuthRepository _repository; // Repositorio para acceder a los códigos de autenticación

        public LoginQrCommandHandler(ITokenService token, IAuthRepository authRepository)
        {
            _repository = authRepository;
            _tokenService = token;
        }

        public async Task<AuthResponse?> Handle(LoginQrCommand request, CancellationToken ct)
        {
            // 1. Buscamos el código usando el repositorio
            var authCode = await _repository.GetActiveAuthCode(request.AuthCode, ct);

            if (authCode == null || authCode.User == null)
                return null;

            // 2. Lógica de negocio: invalidar el código
            authCode.IsUsed = true;
            await _repository.UpdateAuthCode(authCode, ct);

            // Persistimos los cambios
            await _repository.SaveChangesAsync(ct);

            // 3. Generar el JWT usando la relación cargada
            var token = _tokenService.GenerateJwtToken(authCode.User);

            return new AuthResponse
            {
                Token = token,
                User = new UserResponse
                {
                    Email = authCode.User.Email,
                    Name = authCode.User.Name
                }
            };
        }
    }
}
