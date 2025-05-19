using System;



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanGame.Source.Engine.Map;

namespace BombermanGame.Source.Engine.PlayerManager
{
    public class Player
    {
        public Vector2 Position;
        private Texture2D _texture;
        private float speed = 4;
        private Rectangle _boundingBox;
        public int Health { get; private set; } = 3;
        public Rectangle BoundingBox => _boundingBox;
        public bool IsAlive { get; private set; } = true;
        private double damageCooldown = 1000; 
        private double timeSinceLastDamage = 0;

        public Player(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            UpdateBoundingBox();
        }

        public void TakeDamage(int amount)
        {
            if (timeSinceLastDamage < damageCooldown)
                return;

            Health -= amount;
            timeSinceLastDamage = 0;
            if (Health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Console.WriteLine("Player died");
            IsAlive = false;
        }

        public void Update(string direction, Tilemap tilemap, GameTime gameTime)
        {
            timeSinceLastDamage += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!IsAlive) return;
            Vector2 newPosition = Position;

            // Store the current position before any movement
            Rectangle previousBoundingBox = _boundingBox;

            switch (direction)
            {
                case "Left":
                    newPosition.X -= speed;
                    break;
                case "Right":
                    newPosition.X += speed;
                    break;
                case "Up":
                    newPosition.Y -= speed;
                    break;
                case "Down":
                    newPosition.Y += speed;
                    break;
            }

            Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _texture.Width, _texture.Height);

            Rectangle collisionBounds = new Rectangle(
        newBoundingBox.X + 20, //left
        newBoundingBox.Y + 40, //top 
        newBoundingBox.Width - 40, //right
        newBoundingBox.Height - 40 //bottom
    );


            if (!tilemap.IsTileCollidable(collisionBounds))
            {

                Position = newPosition;
                _boundingBox = newBoundingBox;
            }
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
            UpdateBoundingBox();
        }

        private void UpdateBoundingBox()
        {
            if (_texture != null)
            {
                _boundingBox = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) return;
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
