using Asp.Versioning;
using DikePay.Modules.Catalog.Shared.Contracts.v1.Commands;
using DikePay.Modules.Catalog.Shared.Contracts.v1.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Products
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            var productId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }


        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string code, [FromBody] UpdateProductCommand command)
        {
            if (code != command.Code)
            {
                return BadRequest("El código del producto en la ruta no coincide con el del cuerpo de la solicitud.");
            }

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            // Este es un stub para ilustrar el punto.
            // Deberías implementar la lógica real para obtener el producto por ID.
            return Ok($"Producto con ID {id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetProductQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }


    }
}
