using Microsoft.AspNetCore.Mvc;
namespace Chemical_Ne_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult GetAPIstatus()
        {
            return Ok();
        }
    }
}
