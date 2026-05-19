using MediatR;

namespace DikePay.Modules.Auth.Shared.Contracts.v1.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "A";
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = "V";
    }
}
