using DomainModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Mapper
{
    public static class ControllerMapper
    {
        public static Controller toUpdateController(this UpdateControllerLEDBrightnessDTO controllerLEDBrightnessDTO)
        {
            return new Controller
            {
                Id = controllerLEDBrightnessDTO.Id,
                ledBrightness = controllerLEDBrightnessDTO.LEDBrightness,
            };
        }
        public static Controller toUpdateController(this UpdateControllerPlayerColorDTO controllerPlayerColorDTO)
        {
            return new Controller
            {
                Id = controllerPlayerColorDTO.Id,
                playerColor = controllerPlayerColorDTO.PlayerColor,

            };
        }

        public static Controller toUpdateController(this UpdateControllerPlayerDTO updateControllerPlayer)
        {
            return new Controller
            {
                Id = updateControllerPlayer.Id,
                playerId = updateControllerPlayer.playerId,

            };
        }

        public static Controller toUpdateController(this UpdateControllerDTO updateControllerDTO)
        {
            return new Controller
            {
                Id = updateControllerDTO.Id,
                playerColor = updateControllerDTO.playerColor,
                ledBrightness = updateControllerDTO.ledBrightness,
                playerId = updateControllerDTO.playerId
            };
        }
    }
}
