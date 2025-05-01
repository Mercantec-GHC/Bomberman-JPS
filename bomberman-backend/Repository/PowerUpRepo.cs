using bomberman_backend.Data;
using bomberman_backend.Repository.Interfaces;
using DomainModels;

namespace bomberman_backend.Repository
{
    public class PowerUpRepo : IPowerUpRepo
    {
        private readonly DatabaseContextcs _databaseContext;

        public PowerUpRepo(DatabaseContextcs databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public PowerUp GetPowerUp(int id)
        {
            var powerUp = _databaseContext.powerup.Find(id);
            if (powerUp == null)
            {
                return null;
            }
            return powerUp;
        }

        public List<PowerUp> GetPowerUps()
        {
            var powerUps = _databaseContext.powerup.ToList();
            List<PowerUp> powerUpList = new List<PowerUp>();
            foreach (PowerUp powerUp in powerUps)
            {
                var powerUpEntity = new PowerUp
                {
                    Id = powerUp.Id,
                    Name = powerUp.Name,
                    duration = powerUp.duration,
                    Effect = powerUp.Effect
                };
                powerUpList.Add(powerUpEntity);
            }
            return powerUpList;
        }
    }
}
