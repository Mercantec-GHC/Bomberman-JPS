using BombermanGame.Source.Engine.PlayerManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BombermanGame.Source.Engine.PowerUps;


namespace BombermanGame.Source.Engine.PowerUps
{
    public class PowerUpManager
    {
        private List<PowerUp> PowerUps = new();
        private Dictionary<PowerUpType, Texture2D> textures;
        private Random rng = new();

        public PowerUpManager(Dictionary<PowerUpType, Texture2D> textures)
        {
            this.textures = textures;
        }

        public void SpawnPowerUp(Vector2 position)
        {
            if (rng.NextDouble() < 1)
            {
                int powerUpCount = Enum.GetValues(typeof(PowerUpType)).Length - 1; 
                var type = (PowerUpType)rng.Next(1, powerUpCount + 1); // doesnt spawn "None"

                PowerUps.Add(new PowerUp(position, type, textures[type]));
            }
        }

        public void Update(Player player)
        {
            for (int i = PowerUps.Count - 1; i >= 0; i--)
            {
                if (player.BoundingBox.Intersects(PowerUps[i].Bounds))
                {
                    // Do not store if player already has a powerUp
                    if (player.StoredPowerUp == PowerUpType.None)
                    {
                        player.PickUpPowerUp(PowerUps[i].PowerUpType);
                        PowerUps.RemoveAt(i);
                    }
                    
                }
            }
        }

        private void ApplyPowerUp(Player player, PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.Speed:
                    player.speed = player.BaseSpeed + 4.5f;
                    player.speedBoostTimer = 5000; // 5 seconds
                    player.hasSpeedBoost = true;
                    break;
                case PowerUpType.HealthUp:
                    player.Heal(1);
                    break;
                case PowerUpType.Invincible:
                    player.SetInvincibility(3000); // 3 seconds
                    break;
                case PowerUpType.ExplosionRadius:
                    player.BonusRadius = 3;
                    player.HasBonusRadius = true;
                    break;
                case PowerUpType.LifeSteal:
                    player.EnableLifeSteal();
                    break;
                case PowerUpType.Ghost:
                    player.EnableGhostMode(5000); // 3 seconds
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var powerUp in PowerUps)
                powerUp.Draw(spriteBatch);
        }
    }
    
}
