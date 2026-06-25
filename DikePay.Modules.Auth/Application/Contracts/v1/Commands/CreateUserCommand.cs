using MediatR;

namespace DikePay.Modules.Auth.Application.Contracts.v1.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "A";
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }    
    }
}
