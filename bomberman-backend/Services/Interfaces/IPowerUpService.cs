using DomainModels;

namespace bomberman_backend.Services.Interfaces
{
    public interface IPowerUpService
    {
        public List<PowerUp> GetPowerUps();

        public PowerUp GetPowerUp(int id);
    }
}
