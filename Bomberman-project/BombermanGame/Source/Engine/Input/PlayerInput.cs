using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace BombermanGame.Source.Engine.Input
{
    public class PlayerInput
    {
        public string MoveDirection { get; set; } = "Idle";
        public bool BombPlaced { get; set; }
        public bool PowerUpUsed { get; set; }

        public void HandleKeyboardInput()
        {
            var state = Keyboard.GetState();
            MoveDirection = "Idle";

            if (state.IsKeyDown(Keys.A))
                MoveDirection = "Left";
            else if (state.IsKeyDown(Keys.D))
                MoveDirection = "Right";
            else if (state.IsKeyDown(Keys.W))
                MoveDirection = "Up";
            else if (state.IsKeyDown(Keys.S))
                MoveDirection = "Down";           

            BombPlaced = state.IsKeyDown(Keys.Space);
            PowerUpUsed = state.IsKeyDown(Keys.F);


        }

        public void ResetActions()
        {
            BombPlaced = false;
            PowerUpUsed = false;
        }
    }
}
