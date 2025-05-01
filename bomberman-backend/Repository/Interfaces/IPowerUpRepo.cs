using DomainModels;

namespace bomberman_backend.Repository.Interfaces
{
    public interface IPowerUpRepo
    {
        public List<PowerUp> GetPowerUps();

        public PowerUp GetPowerUp(int id);
    }
}
