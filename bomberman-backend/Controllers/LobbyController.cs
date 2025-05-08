using bomberman_backend.Services.Interfaces;
using DomainModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bomberman_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyService _lobbyService;
        public LobbyController(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }


        [HttpPost]
        public ActionResult CreateLobby([FromBody] CreateLobbyDTO lobby)
        {
            if (lobby == null)
            {
                return BadRequest();
            }
            var _lobby = _lobbyService.CreateLobby(lobby);
            return Ok(_lobby);
        }

        [HttpPut]
        public ActionResult UpdateLobby([FromQuery] int lobbyId, [FromBody] CreateLobbyDTO createLobbyDTO)
        {
            var _lobby = _lobbyService.UpdateLobby(lobbyId, createLobbyDTO);
            if (Object.ReferenceEquals(_lobby, null))
            {
                return NotFound("Lobby not found");
            }
            return Ok(_lobby);
        }

        [HttpDelete]
        public ActionResult DeleteLobby([FromQuery] int id)
        {
            _lobbyService.DeleteLobby(id);
            return Ok();
        }

        [HttpPost("/api/[controller]/addplayer")]
        public ActionResult AddPlayerToLobby([FromQuery] int lobbyId, [FromQuery] Guid userId)
        {
            _lobbyService.AddUserToLobby(lobbyId, userId);
            return Ok();
        }

        [HttpDelete("/api/[controller]/removeplayer")]
        public ActionResult RemovePlayerFromLobby([FromQuery] int lobbyId, [FromQuery] Guid userId)
        {
            _lobbyService.RemoveUserFromLobby(lobbyId, userId);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetLobbies()
        {
            var lobbies = _lobbyService.GetLobbies();
            if (Object.ReferenceEquals(lobbies, null))
            {
                return NotFound("No lobbies found");
            }
            return Ok(lobbies);
        }

        [HttpGet("{id}")]
        public ActionResult GetLobby(int id)
        {
            var lobby = _lobbyService.GetLobby(id);
            if (Object.ReferenceEquals(lobby, null))
            {
                return NotFound("Lobby not found");
            }
            return Ok(lobby);
        }

        [HttpGet("/api/[controller]/players")]
        public ActionResult GetPlayers([FromQuery] int lobbyId)
        {
            var players = _lobbyService.GetPlayers(lobbyId);
            if (Object.ReferenceEquals(players, null))
            {
                return NotFound("No players found");
            }
            return Ok(players);
        }

    }
}
