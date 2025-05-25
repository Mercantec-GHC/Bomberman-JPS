using Microsoft.Xna.Framework.Input;

namespace BombermanGame.Source.Engine.Input
{
    public class PlayerInput
    {
        public string MoveDirection { get; private set; } = "Idle";
        public bool BombPlaced { get; private set; }
        public bool PowerUpUsed { get; private set; }

        private bool fromMqtt = false;

        public void SetFromMQTT(string type, string value)
        {
            fromMqtt = true;

            switch (type)
            {
                case "tilt_move":
                    MoveDirection = value;  // e.g. "Up", "Down", "Left", "Right", or "Idle"
                    break;
                case "bomb_press":
                    BombPlaced = true;
                    break;
                case "powerup_used":
                    PowerUpUsed = true;
                    break;
            }
        }

        public void HandleKeyboardInput()
        {
            var keyboardState = Keyboard.GetState();

            if (!fromMqtt) // only handle keyboard if no recent MQTT input
            {
                // Only update MoveDirection if a movement key is pressed
                if (keyboardState.IsKeyDown(Keys.W))
                    MoveDirection = "Up";
                else if (keyboardState.IsKeyDown(Keys.S))
                    MoveDirection = "Down";
                else if (keyboardState.IsKeyDown(Keys.A))
                    MoveDirection = "Left";
                else if (keyboardState.IsKeyDown(Keys.D))
                    MoveDirection = "Right";
                // IMPORTANT: do NOT reset to "Idle" here, keep last direction
            }

            // Bomb placement key (Space)
            if (keyboardState.IsKeyDown(Keys.Space))
                BombPlaced = true;

            // Example: Use power-up on pressing E key
            if (keyboardState.IsKeyDown(Keys.E))
                PowerUpUsed = true;
        }

        // Call after processing inputs each frame to reset one-time flags and MQTT flag
        public void ResetActions()
        {
            BombPlaced = false;
            PowerUpUsed = false;
            fromMqtt = false; // allow keyboard input next frame if no MQTT input
        }
    }
}
