using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloOpenTelemetry.Controllers
{
    [ApiController]
    public class HelloController : ControllerBase
    {

        [HttpGet]
        [Route("greeting/{name:alpha}")]
        public IActionResult GetGreeting(string name)
        {
            return Ok($"Hello, {name}!");
        }
    }
}
