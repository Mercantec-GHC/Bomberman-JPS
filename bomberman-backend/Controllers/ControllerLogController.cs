using Microsoft.AspNetCore.Mvc;

namespace bomberman_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CarrierController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
