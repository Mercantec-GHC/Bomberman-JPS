using System;



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanGame.Source.Engine.PlayerManager
{
    public class Player
    {
        public Vector2 Position;
        private Texture2D _texture;

        public Player (Vector2 position, Texture2D texture)
        {
            Position = position;
            _texture = texture;
        }

        public void Update(string direction)
        {
            float speed = 2f;

            switch (direction)
            {
                case "Left":
                    Position.X -= speed;
                    break;
                case "Right":
                    Position.X += speed;
                    break;
                case "Up":
                    Position.Y -= speed;
                    break;
                case "Down":
                    Position.Y += speed;
                    break;
            }
        }

        public void draw (SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
