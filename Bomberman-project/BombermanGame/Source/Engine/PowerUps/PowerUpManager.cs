using BombermanGame.Source.Engine.PlayerManager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            if (rng.NextDouble() < 0.3)
            {
                var type = (PowerUpType)rng.Next(Enum.GetValues(typeof(PowerUpType)).Length);
                PowerUps.Add(new PowerUp(position, type, textures[type]));
            }
        }

        public void Update(Player player)
        {
            for (int i = PowerUps.Count - 1; i >= 0; i--)
            {
                if (player.BoundingBox.Intersects(PowerUps[i].Bounds))
                {
                    ApplyPowerUp(player,  PowerUps[i].PowerUpType);
                    PowerUps.RemoveAt(i);
                }
            }
        }

        private void ApplyPowerUp(Player player, PowerUpType type)
        {
            switch (type)
            {
                //case PowerUpType.ExplosionRadiu:
                //    player.ExplosionRadius++;
                //    break;
                //case PowerUpType.IncreaseExplosionRange:
                //    player.ExplosionRange++;
                //    break;
                //case PowerUpType.IncreaseSpeed:
                //    player.MoveSpeed += 0.5f;
                //    break;
            }

        }
    }
    
}
