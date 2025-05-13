using DomainModels;
using DomainModels.DTO;

namespace Bomberman_Backend.Repository.Interfaces
{
    public interface IPlayerRepo
    {
        public List<Player> GetPlayers();
        public Player GetPlayer(Guid id);
        public Player CreatePlayer(CreatePlayerDTO player);
        public Player UpdatePlayer(UpdatePlayerDTO player);
        public Player AddPowerUp(string username, PowerUp powerup);
        public Player RemovePowerUp(string username, PowerUp powerup);
        public Player AddBomb(string username, Bomb bomb);
        public Player RemoveBomb(string username, Bomb bomb);
        public TokenResponse Login(LoginDTO login);
        public TokenResponse LoginWithRefreshToken(TokenRequest tokenRequest);

        public bool RevokeRefreshToken(Guid userId);
    }
}
