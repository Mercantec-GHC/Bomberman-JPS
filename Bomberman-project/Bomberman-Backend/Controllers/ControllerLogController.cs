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
        public CarrierController(IControllerLogService controllerLogService)
        {
            _controllerLogSerivce = controllerLogService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateControllerLogsDTO controllerLog, InputType inputType)
        {
            if(controllerLog == null)
            {
                return BadRequest();
            }
            var _controllerLog = _controllerLogSerivce.CreateControllerLog(controllerLog, inputType);
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
    }
}
