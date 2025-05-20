using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanGame.Source.Engine.PowerUps
{
    public class PowerUp
    {
        public Vector2 Position;
        public PowerUpType PowerUpType;
        public Texture2D Texture;
        public Rectangle Bounds => new((int)Position.X,(int)Position.Y, 70, 70);

        public PowerUp(Vector2 position, PowerUpType powerUpType, Texture2D texture) 
        {
            Position = position;
            PowerUpType = powerUpType;
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}
