using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanGame.Source.Engine.BombManager
{
    public class Bomb
    {
        public Vector2 Position;
        public int Frame;
        public int Counter;
        public bool IsFinsihed;
        private int FrameSpeed = 60;

        public Bomb(Vector2 position)
        {
            Position = position;
            Frame = 0;
            Counter = 0;
            IsFinsihed = false;
        }

        public void Update(Texture2D[] textures)
        {
            if (IsFinsihed) return;

            Counter++;
            if (Counter > FrameSpeed)
            {
                Counter = 0;
                Frame++;

                if (Frame >= textures.Length)
                {
                    IsFinsihed = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D[] textures)
        {
            if (!IsFinsihed && Frame < textures.Length)
            {
                spriteBatch.Draw(textures[Frame], new Rectangle((int)Position.X, (int)Position.Y, 70, 70), Color.White);
            }
        }
    }

    
}
