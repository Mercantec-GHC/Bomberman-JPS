using System;
using BombermanGame.Source.Engine.PowerUps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BombermanGame.Source.Engine.Map;
using System.Collections.Generic;

namespace BombermanGame.Source.Engine.PlayerManager
{
    public class Player
    {
        public Vector2 Position;
        private Texture2D _texture; // Player image
        private Rectangle _boundingBox; // for collision and position
        public int Health { get; private set; } = 3;
        public Rectangle BoundingBox => _boundingBox;
        public bool IsAlive { get; private set; } = true;
        private double damageCooldown = 1000; 
        private double timeSinceLastDamage = 0;

        public float BaseSpeed { get; set; } = 4f; // your normal speed
        public float speed = 4f;                   // current speed

        public double speedBoostTimer = 0;
        public bool hasSpeedBoost = false;

        public int ExplosionRadius { get; set; } = 3;
        public int BonusRadius { get; set; } = 0; // Extra radius from powerup
        public bool HasBonusRadius { get; set; } = false;
        public bool CanLifeSteal { get; private set; }
        private bool isInvincible = false;
        private double invincibilityTimer = 0;

        public bool IsGhost { get; private set; }
        private double ghostTimer;

        public PowerUpType StoredPowerUp { get; private set; } = PowerUpType.None;

        private Dictionary<PowerUpType, Texture2D> powerUpIcons;



        public Player(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            UpdateBoundingBox();
        }

        public void TakeDamage(int amount)
        {
            if (isInvincible || timeSinceLastDamage < damageCooldown)
                return;

            Health -= amount;
            timeSinceLastDamage = 0;

            if (Health <= 0)
                Die();
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
            double timePassed = gameTime.ElapsedGameTime.TotalMilliseconds;

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

            if (hasSpeedBoost)
            {
                speedBoostTimer -= timePassed;
                if (speedBoostTimer <= 0)
                {
                    // Speed boost ended
                    hasSpeedBoost = false;
                    speed = BaseSpeed;
                }
            }

            if (isInvincible)
            {
                invincibilityTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (invincibilityTimer <= 0)
                    isInvincible = false;
            }

            if (IsGhost)
            {
                ghostTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (ghostTimer <= 0)
                    IsGhost = false;
            }

            Rectangle newBoundingBox = new Rectangle((int)newPosition.X, (int)newPosition.Y, _texture.Width, _texture.Height);
            Rectangle collisionBounds = new Rectangle(
                newBoundingBox.X + 20,
                newBoundingBox.Y + 40,
                newBoundingBox.Width - 40,
                newBoundingBox.Height - 40
            );


            if (!tilemap.IsTileCollidable(collisionBounds, IsGhost))
            {
                Position = newPosition;
                _boundingBox = newBoundingBox;
            }

            // Ghost timer handling
            if (IsGhost)
            {
                ghostTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (ghostTimer <= 0)
                {
                    // Check if standing on type 1 tile
                    if (tilemap.IsOnGroundTile(_boundingBox))
                    {
                        IsGhost = false; // End ghost mode safely
                    }
                    else
                    {
                        // Prevent ghost mode from ending until on ground tile
                        ghostTimer = 100;
                    }
                }
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

        public void Heal(int amount)
        {
            Health += amount;
        }

        public void SetInvincibility(int milliseconds)
        {
            isInvincible = true;
            invincibilityTimer = milliseconds;
        }

        public void EnableLifeSteal()
        {
            CanLifeSteal = true;
        }

        public void EnableGhostMode(double duration)
        {
            IsGhost = true;
            ghostTimer = duration;
        }


        public void PickUpPowerUp(PowerUpType powerUp)
        {
            if (StoredPowerUp == PowerUpType.None)
            {
                StoredPowerUp = powerUp;
            }
            else
            {
                
            }
        }

        public void UseStoredPowerUp()
        {
            if (StoredPowerUp == PowerUpType.None)
                return;

            switch (StoredPowerUp)
            {
                case PowerUpType.Speed:
                    speed = BaseSpeed + 4.5f;
                    speedBoostTimer = 5000; 
                    hasSpeedBoost = true;
                    break;
                case PowerUpType.HealthUp:
                    Heal(1);
                    break;
                case PowerUpType.ExplosionRadius:
                    HasBonusRadius = true;
                    BonusRadius = 1;
                    break;
                case PowerUpType.Invincible:
                    SetInvincibility(5000);
                    break;
                case PowerUpType.Ghost:
                    EnableGhostMode(5000);
                    break;
                case PowerUpType.LifeSteal:
                    EnableLifeSteal();
                    break;
            }

            // After using it, remove from inventory
            StoredPowerUp = PowerUpType.None;
        }

        public void SetPowerUpIcons(Dictionary<PowerUpType, Texture2D> icons)
        {
            this.powerUpIcons = icons;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!IsAlive) return;
            spriteBatch.Draw(_texture, Position, Color.White);
            Vector2 healthPos = new Vector2(Position.X, Position.Y - 20); // 20 pixels above player
            spriteBatch.DrawString(font, Health.ToString(), healthPos, Color.Red);

            if (StoredPowerUp != PowerUpType.None && powerUpIcons != null && powerUpIcons.ContainsKey(StoredPowerUp))
            {
                Texture2D icon = powerUpIcons[StoredPowerUp];
                Vector2 iconPos = new Vector2(Position.X + _texture.Width - 16, Position.Y - 40); // Top right of player
                spriteBatch.Draw(icon, iconPos, Color.White);
            }
        }
    }
}
