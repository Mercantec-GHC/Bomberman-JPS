﻿using Bomberman_Backend.Services.Interfaces;
using DomainModels.DTO;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bomberman_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult GetPlayer([FromQuery] Guid id)
        {
            var player = _playerService.GetPlayer(id);
            if (Object.ReferenceEquals(player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(player);
        }

        [HttpGet("/api/[controller]/all")]
        public ActionResult GetAllPlayers()
        {
            var players = _playerService.GetPlayers();
            if (Object.ReferenceEquals(players, null))
            {
                return NotFound("No players found");
            }
            return Ok(players);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreatePlayerDTO player)
        {
            var _player = _playerService.CreatePlayer(player);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPut]
        public ActionResult Put([FromBody] UpdatePlayerDTO player)
        {
            var _player = _playerService.UpdatePlayer(player);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPut("/api/[controller]/addPowerUp")]
        public ActionResult AddPowerUp([FromQuery] string username, [FromBody] PowerUp powerup)
        {
            var _player = _playerService.AddPowerUp(username, powerup);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPut("/api/[controller]/removePowerUp")]
        public ActionResult RemovePowerUp([FromQuery] string username, [FromBody] PowerUp powerup)
        {
            var _player = _playerService.RemovePowerUp(username, powerup);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPut("/api/[controller]/addBomb")]
        public ActionResult AddBomb([FromQuery] string username, [FromBody] Bomb bomb)
        {
            var _player = _playerService.AddBomb(username, bomb);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPut("/api/[controller]/removeBomb")]
        public ActionResult RemoveBomb([FromQuery] string username, [FromBody] Bomb bomb)
        {
            var _player = _playerService.RemoveBomb(username, bomb);
            if (Object.ReferenceEquals(_player, null))
            {
                return NotFound("Player not found");
            }
            return Ok(_player);
        }

        [HttpPost("/api/[controller]/login")]
        public ActionResult Login(LoginDTO login)
        {
            var _player = _playerService.Login(login);
            return Ok(_player);
        }

        [HttpPost("/api/[controller]/refreshToken")]
        public ActionResult RefreshToken(TokenRequest tokenRequest)
        {
            var _player = _playerService.LoginWithRefreshToken(tokenRequest);
            return Ok(_player);
        }
        
        [Authorize]
        [HttpDelete("/api/[controller]/{id:guid}/refreshToken")]
        public ActionResult RefreshToken([FromRoute] Guid id)
        {
            bool success = _playerService.RevokeRefreshToken(id);
            return success ? Ok() : NotFound("Player not found");
        }
    }
}
