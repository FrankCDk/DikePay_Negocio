using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Domain;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class GenerateMobileAuthCodeHandler : IRequestHandler<GenerateMobileAuthCodeCommand, ServiceResponse<string>>
    {
        private readonly IAuthRepository _repository;

        public GenerateMobileAuthCodeHandler(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<string>> Handle(GenerateMobileAuthCodeCommand request, CancellationToken ct)
        {
            // 1. Generar un código aleatorio alfanumérico de 6 caracteres
            // Senior Tip: Evita letras similares como 'O' y '0' o 'I' y '1' si fuera manual,
            // pero para QR cualquier alfanumérico vale.
            var newCode = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            var authCode = new MobileAuthCode
            {
                Id = Guid.NewGuid(),
                Code = newCode,
                UserId = request.UserId,
                ExpiryDate = DateTime.UtcNow.AddMinutes(10), // Expira pronto por seguridad
                IsUsed = false
            };

            // 2. Persistir en MySQL
            await _repository.AddAuthCode(authCode, ct);
            await _repository.SaveChangesAsync(ct);

            return ServiceResponse<string>.Ok(newCode);
        }
    }
}
