using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DikePay.Api.Controllers.v1.Facturacion
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BillingController : ControllerBase
    {

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] CreateInvoiceCommand request)
        //{

        //}

    }
}
