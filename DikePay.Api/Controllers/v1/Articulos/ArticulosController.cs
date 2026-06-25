using Asp.Versioning;
using DikePay.Modules.Articulos.Shared.Contracts.v1.Commands;
using DikePay.Modules.Articulos.Shared.Contracts.v1.DTOs;
using DikePay.Modules.Articulos.Shared.Contracts.v1.Queries;
using DikePay.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Articulos
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ArticulosController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ArticulosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<ArticuloResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CrearArticulo([FromBody] CrearArticuloCommand command, CancellationToken cancellationToken)
        {
            // 1. MediatR ejecuta el Handler y nos devuelve el record ArticuloResponse completo
            var response = await _mediator.Send(command, cancellationToken);
            
            // 2. Retornamos un estado 201 Created. 
            return CreatedAtAction(
                nameof(ObtenerArticuloPorId),
                new { id = command.Id },
                response
            );
        }


        [HttpPut("{codigo}")]
        public async Task<IActionResult> ActualizarArticulo([FromRoute] string codigo, [FromBody] ActualizarArticuloCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ObtenerArticuloPorId(Guid id)
        {
            // Este es un stub para ilustrar el punto.
            // Deberías implementar la lógica real para obtener el Articuloo por ID.
            return Ok($"Articuloo con ID {id}");
        }

        [HttpGet]
        public async Task<IActionResult> ListarArticulos([FromQuery] ListarArticulosQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
