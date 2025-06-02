using Bomberman_Backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bomberman_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CarrierController : ControllerBase
    {
        private readonly IControllerLogService _controllerLogSerivce;
        private readonly IControllerService _controllerService;
        public CarrierController(IControllerLogService controllerLogService, IControllerService controllerService)
        {
            _controllerLogSerivce = controllerLogService;
            _controllerService = controllerService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateControllerLogsDTO controllerLog)
        {
            if (controllerLog == null)
            {
                return BadRequest();
            }
            var _controllerLog = _controllerLogSerivce.CreateControllerLog(controllerLog);
            return Ok(_controllerLog);
        }

        [HttpGet]
        public ActionResult<List<ControllerLogs>> GetAllLogs()
        {
            return Ok(_controllerLogSerivce.GetControllerLogs);
        }

        [HttpDelete]
        public ActionResult DeleteControllerLog([FromBody] int id)
        {
            _controllerLogSerivce.DeleteControllerLog(id);
            return Ok();
        }

        [HttpGet("/api/[controller]/controller")]
        public ActionResult GetControllerById([FromQuery] int id)
        {
            var controller = _controllerService.GetControllerByIdAsync(id);
            if (Object.ReferenceEquals(controller, null))
            {
                return NotFound("Controller not found");
            }
            return Ok(controller);
        }

        [HttpGet("/api/[controller]/controller/all")]
        public ActionResult GetAllControllers()
        {
            var controllers = _controllerService.GetAllControllersAsync();
            if (Object.ReferenceEquals(controllers, null))
            {
                return NotFound("No controllers found");
            }
            return Ok(controllers);
        }

        [HttpPost("/api/[controller]/controller")]
        public ActionResult PostController([FromBody] DomainModels.Controller controller)
        {
            if (controller == null)
            {
                return BadRequest();
            }
            var _controller = _controllerService.AddControllerAsync(controller);
            return Ok(_controller);
        }

        [HttpPut("/api/[controller]/controller")]
        public ActionResult PutController([FromBody] UpdateControllerDTO updateControllerDTO)
        {
            if (updateControllerDTO == null)
            {
                return BadRequest();
            }
            var _controller = _controllerService.UpdateControllerAsync(updateControllerDTO);
            if (Object.ReferenceEquals(_controller, null))
            {
                return NotFound("Controller not found");
            }
            return Ok(_controller);
        }

        [HttpPut("/api/[controller]/controller/led")]
        public ActionResult UpdateLedBrightness([FromBody] UpdateControllerLEDBrightnessDTO updateControllerLED)
        {
            if (updateControllerLED == null)
            {
                return BadRequest();
            }
            var _controller = _controllerService.UpdateLEDBrightness(updateControllerLED);
            if (Object.ReferenceEquals(_controller, null))
            {
                return NotFound("Controller not found");
            }
            return Ok(_controller);
        }

        [HttpPut("/api/[controller]/controller/player")]
        public ActionResult UpdatePlayerId([FromBody] UpdateControllerPlayerDTO updateControllerPlayer)
        {
            if (updateControllerPlayer == null)
            {
                return BadRequest();
            }
            var _controller = _controllerService.UpdatePlayerAsync(updateControllerPlayer);
            if (Object.ReferenceEquals(_controller, null))
            {
                return NotFound("Controller not found");
            }
            return Ok(_controller);
        }

        [HttpPut("/api/[controller]/controller/playercolor")]
        public ActionResult UpdatePlayerColor([FromBody] UpdateControllerPlayerColorDTO playerColorDTO)
        {
            if (playerColorDTO == null)
            {
                return BadRequest();
            }
            var _controller = _controllerService.UpdatePlayerColorAsync(playerColorDTO);
            if (Object.ReferenceEquals(_controller, null))
            {
                return NotFound("Controller not found");
            }
            return Ok(_controller);
        }


        [HttpDelete("/api/[controller]/controller")]
        public ActionResult DeleteController([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid controller ID");
            }
            var exists = _controllerService.ControllerExistsAsync(id);
            if (!exists.Result)
            {
                return NotFound("Controller not found");
            }
            _controllerService.DeleteControllerAsync(id);
            return Ok("Controller deleted successfully");
        }

        [HttpGet("/api/[controller]/controller/exists")]
        public ActionResult<bool> ControllerExists([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid controller ID");
            }
            var exists = _controllerService.ControllerExistsAsync(id);
            return Ok(exists.Result);
        }
    }
}
