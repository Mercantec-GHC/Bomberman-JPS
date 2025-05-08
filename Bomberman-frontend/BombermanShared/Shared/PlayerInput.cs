using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame.Source.Engine.Input
{
    public class PlayerInput
    {
        public string MoveDirection { get; set; } = "Idle";
        public bool BombPlaced { get; set; }
        public bool PowerUpUsed { get; set; }

        public void ResetActions()
        {
            BombPlaced = false;
            PowerUpUsed = false;
        }
    }
}
