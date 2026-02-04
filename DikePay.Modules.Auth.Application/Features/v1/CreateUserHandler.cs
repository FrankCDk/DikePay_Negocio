using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Domain;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using MediatR;

namespace DikePay.Modules.Auth.Application.Features.v1
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserAccount user = new UserAccount
            {
                Code = request.Codigo,
                Name = request.Nombre,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = request.Rol,
                IsActive = request.Estado.ToLower() == "A"
            };

            await _userRepository.Create(user);
            return true;
        }

        private string HashPassword(string password)
        {
            // El "WorkFactor" 12 es el equilibrio perfecto entre seguridad y velocidad actual
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);
        }
    }
}
