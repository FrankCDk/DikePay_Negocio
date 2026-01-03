using Asp.Versioning;
using DikePay.Modules.Auth.Shared.Contracts.v1.Commands;
using DikePay.Modules.Auth.Shared.Contracts.v1.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(request, cancellationToken);
            if(user == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            return Ok(user);

        }

    }
}
