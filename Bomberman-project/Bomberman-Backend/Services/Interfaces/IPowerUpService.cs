using DomainModels;

namespace Bomberman_Backend.Services.Interfaces
{
    public interface IPowerUpService
    {
        public List<PowerUp> GetPowerUps();

        public PowerUp GetPowerUp(int id);
    }
}
