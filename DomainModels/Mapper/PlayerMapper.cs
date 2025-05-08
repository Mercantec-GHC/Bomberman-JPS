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
                UserName = createPlayerDTO.userName,
                Password = createPlayerDTO.password,
                Email = createPlayerDTO.email,
                sessionId = createPlayerDTO.sessionId
            };
        }

        public static Player toUpdatePlayer(this UpdatePlayerDTO updatePlayerDTO)
        {
            return new Player
            {
                characterColor = updatePlayerDTO.CharacterColor,
                wins = updatePlayerDTO.Wins,
                inLobby = updatePlayerDTO.InLobby,
                powerUp = updatePlayerDTO.PowerUp,
                sessionId = updatePlayerDTO.sessionId,
            };
        }
    }
}
