using Asp.Versioning;
using DikePay.Modules.Promociones.Application.Contracts.v1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Products
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PromocionesController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PromocionesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPromociones(CancellationToken cancellationToken)
        {
            var query = new ListarPromocionesQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

    }
}
