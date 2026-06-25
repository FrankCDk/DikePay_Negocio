using Asp.Versioning;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Commands;
using DikePay.Modules.Configuracion.Application.Contracts.v1.Queries;
using DikePay.Modules.Configuracion.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Configurations
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VersionesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public VersionesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CrearVersion([FromBody] CrearVersionCommand request, CancellationToken cancellationToken)
        {
            var versionId = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = versionId }, versionId);
        }

        [HttpGet("ultima")]
        public async Task<IActionResult> ObtenerUltimaVersionPlataforma(
            [FromQuery] AppPlatform platform,
            [FromQuery] int currentBuild,
            CancellationToken ct)
        {
            var query = new ObtenerVersionMasRecienteQuery(platform, currentBuild);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId([FromRoute] string id, CancellationToken cancellationToken)
        {
            //var query = new GetVersionByIdQuery { Id = id };
            //var result = await _mediator.Send(query, cancellationToken);
            //return Ok(result);

            return Ok();
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAllVersions([FromQuery] GetAllVersionsQuery request, CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(request, cancellationToken);
        //    return Ok(result);
        //}


    }
}
