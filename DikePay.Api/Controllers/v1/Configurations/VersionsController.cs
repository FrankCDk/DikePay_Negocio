using Asp.Versioning;
using DikePay.Modules.Configuration.Domain.Enums;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Commands;
using DikePay.Modules.Configuration.Shared.Contracts.v1.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Configurations
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VersionsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public VersionsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateVersion([FromBody] CreateVersionCommand request, CancellationToken cancellationToken)
        {
            var versionId = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetVersionById), new { id = versionId }, versionId);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestVersionForPlatform(
            [FromQuery] AppPlatform platform,
            [FromQuery] int currentBuild,
            CancellationToken ct)
        {
            var query = new GetLatestVersionQuery(platform, currentBuild);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVersionById([FromRoute] string id, CancellationToken cancellationToken)
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
