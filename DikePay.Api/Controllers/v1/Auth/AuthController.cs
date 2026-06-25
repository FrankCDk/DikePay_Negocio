using System.Security.Claims;
using Asp.Versioning;
using DikePay.Modules.Auth.Application.Contracts.v1.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Auth
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(request, cancellationToken);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.Success)
            {
                // Puedes mapear códigos específicos a StatusCodes específicos
                return response.Code == "AUTH_003"
                    ? Unauthorized(response)
                    : BadRequest(response);
            }

            return Ok(response);

        }

        [Authorize]
        [HttpPost("generar-qr-code")]
        public async Task<IActionResult> GenerateQrCode(CancellationToken ct)
        {
            // Obtenemos el ID del usuario del Token JWT actual
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var command = new GenerateMobileAuthCodeCommand(userId);
            var result = await _mediator.Send(command, ct);

            return Ok(result); // Devuelve el código temporal
        }

        [HttpPost("login-qr")]
        public async Task<IActionResult> LoginQr([FromBody] LoginQrCommand request, CancellationToken ct)
        {
            var authResponse = await _mediator.Send(request, ct);

            if (authResponse == null)
                return Unauthorized(new { message = "Código inválido o expirado" });

            return Ok(authResponse);
        }

    }
}
