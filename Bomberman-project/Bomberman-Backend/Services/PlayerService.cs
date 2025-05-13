using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels.DTO;
using DomainModels;

namespace Bomberman_Backend.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepo _playerRepo;
        public PlayerService(IPlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }
        public Player AddBomb(string username, Bomb bomb)
        {
            return _playerRepo.AddBomb(username, bomb);
        }

        public Player AddPowerUp(string username, PowerUp powerup)
        {
            return _playerRepo.AddPowerUp(username, powerup);
        }

        public Player CreatePlayer(CreatePlayerDTO player)
        {
            return _playerRepo.CreatePlayer(player);
        }

        public Player GetPlayer(Guid id)
        {
            return _playerRepo.GetPlayer(id);
        }

        public List<Player> GetPlayers()
        {
            return _playerRepo.GetPlayers();
        }

        public Player RemoveBomb(string username, Bomb bomb)
        {
            return _playerRepo.RemoveBomb(username, bomb);
        }

        public Player RemovePowerUp(string username, PowerUp powerup)
        {
            return _playerRepo.RemovePowerUp(username, powerup);
        }

        public Player UpdatePlayer(UpdatePlayerDTO player)
        {
            return _playerRepo.UpdatePlayer(player);
        }

        public TokenResponse Login(LoginDTO login)
        {
            return _playerRepo.Login(login);
        }

        public TokenResponse LoginWithRefreshToken(TokenRequest tokenRequest)
        {
            return _playerRepo.LoginWithRefreshToken(tokenRequest);
        }

        public bool RevokeRefreshToken(Guid userId)
        {
            return _playerRepo.RevokeRefreshToken(userId);
        }
    }
}
