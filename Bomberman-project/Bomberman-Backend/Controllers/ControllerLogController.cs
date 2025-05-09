using Microsoft.AspNetCore.Mvc;

namespace Bomberman_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CarrierController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
