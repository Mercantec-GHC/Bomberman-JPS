using Bomberman_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bomberman_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CarrierController : ControllerBase
    {
        private readonly IControllerLogSerivce _controllerLogSerivce;
        public CarrierController(IControllerLogSerivce controllerLogService)
        {
            _controllerLogSerivce = controllerLogService;
        }

        [HttpPost]


        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
