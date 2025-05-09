using Bomberman_Backend.Repository.Interfaces;
using Bomberman_Backend.Services.Interfaces;
using DomainModels;

namespace Bomberman_Backend.Services
{
    public class PowerUpService : IPowerUpService
    {
        private readonly IPowerUpRepo _powerUpRepo;
        public PowerUpService(IPowerUpRepo powerUpRepo)
        {
            _powerUpRepo = powerUpRepo;
        }
        public List<PowerUp> GetPowerUps()
        {
            return _powerUpRepo.GetPowerUps();
        }
        public PowerUp GetPowerUp(int id)
        {
            return _powerUpRepo.GetPowerUp(id);
        }
    }
}
