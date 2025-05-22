using Microsoft.Xna.Framework.Input;

namespace BombermanGame.Source.Engine.Input
{
    public class PlayerInput
    {
        public string MoveDirection { get; set; } = "Idle";
        public bool BombPlaced { get; set; }
        public bool PowerUpUsed { get; set; }

        private bool fromMqtt = false;

        public void SetFromMQTT(string type, string value)
        {
            fromMqtt = true;

            switch (type)
            {
                case "tilt_move":
                    MoveDirection = value;
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

            if (string.IsNullOrEmpty(MoveDirection))  // Only if MQTT hasn’t set anything
            {
                if (keyboardState.IsKeyDown(Keys.W))
                    MoveDirection = "up";
                else if (keyboardState.IsKeyDown(Keys.S))
                    MoveDirection = "down";
                else if (keyboardState.IsKeyDown(Keys.A))
                    MoveDirection = "left";
                else if (keyboardState.IsKeyDown(Keys.D))
                    MoveDirection = "right";
            }

            if (keyboardState.IsKeyDown(Keys.Space))
                BombPlaced = true;
        }

        public void ResetActions()
        {
            BombPlaced = false;
            PowerUpUsed = false;
            fromMqtt = false;
        }
    }

}
