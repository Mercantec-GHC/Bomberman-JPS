using BombermanGame.Source.Engine.PlayerManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
            if (rng.NextDouble() < 1) //The chance for a powerUp to spawn
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
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var powerUp in PowerUps)
                powerUp.Draw(spriteBatch);
        }
    }
    
}
