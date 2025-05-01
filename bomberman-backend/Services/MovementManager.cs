using DomainModels;
namespace bomberman_backend.Services
{
    public class MovementService
    {
        public void HandleMovement(string direction)
        {
            switch (direction)
            {
                case "Left":
                    MovePlayer(-1, 0);
                    break;
                case "Right":
                    MovePlayer(1, 0);
                    break;
                case "Up":
                    MovePlayer(0, -1);
                    break;
                case "Down":
                    MovePlayer(0, 1);
                    break;
                case "Idle":
                default:
                    Console.WriteLine("Player is idle.");
                    break;
            }
        }

        private void MovePlayer(int dx, int dy)
        {
            Console.WriteLine($"Moving player by dx: {dx}, dy: {dy}");
            
        }
    }
}