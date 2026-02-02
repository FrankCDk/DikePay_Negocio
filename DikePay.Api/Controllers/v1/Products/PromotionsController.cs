using Asp.Versioning;
using DikePay.Modules.Promotions.Shared.Contracts.v1;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Products
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PromotionsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PromotionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPromotions(CancellationToken cancellationToken)
        {
            var query = new GetAllPromotionsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

    }
}
