using bomberman_backend.Repository.Interfaces;
using bomberman_backend.Services.Interfaces;
using DomainModels;

namespace bomberman_backend.Services
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
