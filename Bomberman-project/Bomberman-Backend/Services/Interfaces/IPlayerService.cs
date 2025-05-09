using DomainModels.DTO;
using DomainModels;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetPlayers();
        public Player GetPlayer(Guid id);
        public Player CreatePlayer(CreatePlayerDTO player);
        public Player UpdatePlayer(UpdatePlayerDTO player);
        public Player AddPowerUp(string username, PowerUp powerup);
        public Player RemovePowerUp(string username, PowerUp powerup);
        public Player AddBomb(string username, Bomb bomb);
        public Player RemoveBomb(string username, Bomb bomb);
    }
}
