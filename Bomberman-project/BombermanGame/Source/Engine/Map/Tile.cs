using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanGame.Source.Engine.Map
{
    public class Tile
    {
        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }
        public bool IsWall { get; set; }

        public Tile(Texture2D texture, Rectangle position, bool isWall)
        {
            Texture = texture;
            Position = position;
            IsWall = isWall;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
