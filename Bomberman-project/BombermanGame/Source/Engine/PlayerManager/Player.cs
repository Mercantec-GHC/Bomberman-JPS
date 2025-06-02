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

        public float BaseSpeed { get; set; } = 3f; // your normal speed
        public float Speed { get; private set; } = 3f; // current speed (property with getter for consistency)

        private double speedBoostTimer = 0;
        private bool hasSpeedBoost = false;

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
            Speed = BaseSpeed;
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

            switch (direction)
            {
                case "Left":
                    newPosition.X -= Speed;
                    break;
                case "Right":
                    newPosition.X += Speed;
                    break;
                case "Up":
                    newPosition.Y -= Speed;
                    break;
                case "Down":
                    newPosition.Y += Speed;
                    break;
            }

            if (hasSpeedBoost)
            {
                speedBoostTimer -= timePassed;
                if (speedBoostTimer <= 0)
                {
                    hasSpeedBoost = false;
                    Speed = BaseSpeed;
                }
            }

            if (isInvincible)
            {
                invincibilityTimer -= timePassed;
                if (invincibilityTimer <= 0)
                    isInvincible = false;
            }

            if (IsGhost)
            {
                ghostTimer -= timePassed;
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
                ghostTimer -= timePassed;

                if (ghostTimer <= 0)
                {
                    if (tilemap.IsFullyOnGroundTile(_boundingBox))

                    {
                        IsGhost = false;
                    }
                    else
                    {
                        ghostTimer = 100; // Wait a bit and recheck
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
                    Speed = BaseSpeed + 4.5f;
                    speedBoostTimer = 5000;
                    hasSpeedBoost = true;
                    break;
                case PowerUpType.HealthUp:
                    Heal(1);
                    break;
                case PowerUpType.ExplosionRadius:
                    HasBonusRadius = true;
                    BonusRadius = 3;
                    break;
                    break;
                case PowerUpType.Invincible:
                    SetInvincibility(5000);
                    break;
                case PowerUpType.Ghost:
                    EnableGhostMode(5000);
                    break;
            }

            StoredPowerUp = PowerUpType.None;
        }

        public void SetPowerUpIcons(Dictionary<PowerUpType, Texture2D> icons)
        {
            powerUpIcons = icons;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (!IsAlive) return;
            spriteBatch.Draw(_texture, Position, Color.White);

            Vector2 healthPos = new Vector2(Position.X, Position.Y - 20);
            spriteBatch.DrawString(font, Health.ToString(), healthPos, Color.Red);

            if (StoredPowerUp != PowerUpType.None && powerUpIcons != null && powerUpIcons.ContainsKey(StoredPowerUp))
            {
                Texture2D icon = powerUpIcons[StoredPowerUp];
                Vector2 iconPos = new Vector2(Position.X + _texture.Width - 16, Position.Y - 40);
                spriteBatch.Draw(icon, iconPos, Color.White);
            }
        }
    }
}
