using System;



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanGame.Source.Engine.Map;
using Microsoft.AspNetCore.SignalR.Client;
using DomainModels;

namespace BombermanGame.Source.Engine.PlayerManager
{
    public class Player
    {
        private readonly HubConnection _connection;
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
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5293/chatHub")
                .WithAutomaticReconnect()
                .Build();
            _connection.StartAsync();
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
            SignalRSendType user = new SignalRSendType();
            user.Type = "Game";
            user.User = "testUser";


            switch (direction)
            {
                case "Left":
                    newPosition.X -= speed;
                    user.Message = "Left";
                    _connection.SendAsync("SendMessage", user);
                    break;
                case "Right":
                    newPosition.X += speed;
                    user.Message = "Right";
                    _connection.SendAsync("SendMessage", user);
                    break;
                case "Up":
                    newPosition.Y -= speed;
                    user.Message = "Up";
                    _connection.SendAsync("SendMessage", user);
                    break;
                case "Down":
                    newPosition.Y += speed;
                    user.Message = "Down";
                    _connection.SendAsync("SendMessage", user);
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
