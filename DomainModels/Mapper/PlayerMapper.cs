using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.DTO;

namespace DomainModels.Mapper
{
    public static class PlayerMapper
    {
        public static Player toCreatePlayer(this CreatePlayerDTO createPlayerDTO)
        {
            return new Player
            {
                UserId = createPlayerDTO.UserId,
                Email = createPlayerDTO.Email,
                UserName = createPlayerDTO.Username,
                score = createPlayerDTO.Score,
                lives = createPlayerDTO.Lives,
                characterColor = createPlayerDTO.CharacterColor,
                wins = createPlayerDTO.Wins,
                sessionId = createPlayerDTO.sessionId
            };
        }

        public static Player toUpdatePlayer(this UpdatePlayerDTO updatePlayerDTO)
        {
            return new Player
            {
                UserName = updatePlayerDTO.Username,
                score = updatePlayerDTO.Score,
                lives = updatePlayerDTO.Lives,
                characterColor = updatePlayerDTO.CharacterColor,
                wins = updatePlayerDTO.Wins,
                inLobby = updatePlayerDTO.InLobby,
                powerUp = updatePlayerDTO.PowerUp,
                sessionId = updatePlayerDTO.sessionId,
            };
        }
    }
}
