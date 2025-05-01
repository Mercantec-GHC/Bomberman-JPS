using bomberman_backend.Services.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace bomberman_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PowerUpController : ControllerBase
    {
        private readonly IPowerUpService _powerUpService;
        public PowerUpController(IPowerUpService powerUpService)
        {
            _powerUpService = powerUpService;
        }

        [HttpGet]
        public ActionResult<PowerUp> Get([FromQuery] int id)
        {
            var powerUp = _powerUpService.GetPowerUp(id);
            if (Object.ReferenceEquals(powerUp, null))
            {
                return NotFound("PowerUp not found");
            }
            return powerUp;
        }

        [HttpGet("/api/[controller]/all")]
        public ActionResult<List<PowerUp>> GetAll()
        {
            var powerUps = _powerUpService.GetPowerUps();
            if (Object.ReferenceEquals(powerUps, null))
            {
                return NotFound("No PowerUps found");
            }
            return powerUps;
        }

    }
}
