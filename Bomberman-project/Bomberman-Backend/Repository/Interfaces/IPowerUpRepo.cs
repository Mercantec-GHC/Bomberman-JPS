using DomainModels;

namespace Bomberman_Backend.Repository.Interfaces
{
    public interface IPowerUpRepo
    {
        public List<PowerUp> GetPowerUps();

        public PowerUp GetPowerUp(int id);
    }
}
