using CustomApiNetCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CustomApiNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [Route("{statusCode}")]
        [HttpGet]
        public IActionResult Index(int statusCode)
        {
            //var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ResponseModel response = new ResponseModel
            {
                code = statusCode,
                message = "The requested resource is not found..."
            };
            return StatusCode(statusCode, response);
            //return StatusCode(new { statusCode, message = "The requested resource is not found..." });
        }
    }
}
