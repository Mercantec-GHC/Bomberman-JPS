using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame.Source.Engine.PowerUps
{
    public enum PowerUpType
    {
        None,
        Speed,              //Increase players speed for some seconds
        ExplosionRadius,    //Increases the explosion radius for the next bomb placed
        Ghost,              // Make so the player can walk through blocks for some seconds
        Invincible,         // Makes the player imune to damage for some seconds
        LifeSteal,          // The next player who is struck loses a life, which is then added to their own total.
        HealthUp,           //Gives the player 1 extra health
        
    }
}
