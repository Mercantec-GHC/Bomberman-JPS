using DomainModels;
namespace bomberman_backend.Services
{
    public class PowerUpManager
    {
        public void ActivatePowerUp(PowerUp powerUp) 
        {
            switch(powerUp.Effect)
            {
                case Effect.Speed:
                    Console.WriteLine("Activating Speed for" + powerUp.duration);
                    break;

                case Effect.GhostWalk:
                    Console.WriteLine("Activating GhostWalk for" + powerUp.duration);
                    break;

                case Effect.Invinsible:
                    Console.WriteLine("Activating Invinsible for" + powerUp.duration);
                    break;

                case Effect.HealthUp:
                    Console.WriteLine("Activating HealthUp for" + powerUp.duration);
                    break;
            }
}