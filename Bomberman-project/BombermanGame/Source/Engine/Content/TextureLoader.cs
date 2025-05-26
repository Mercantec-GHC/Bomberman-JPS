using BombermanGame.Source.Engine.PowerUps;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombermanGame.Source.Engine.Content
{
    public static class TextureLoader
    {
        public static Texture2D[] LoadPlayerTextures(ContentManager content)
        {
            return new Texture2D[]
            {
                content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human"),
                content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human2nd")
            };
        }

        public static Texture2D[] LoadBombTextures(ContentManager content)
        {
            Texture2D[] bombTextures = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                bombTextures[i] = content.Load<Texture2D>($"2d/Animation/Bomb/Bomb{i + 1}");
            }
            return bombTextures;
        }

        public static Texture2D LoadExplosionCenter(ContentManager content)
        {
            return content.Load<Texture2D>("2d/Animation/Bomb/BombCenter");
        }

        public static Texture2D LoadExplosionHorizontal(ContentManager content)
        {
            return content.Load<Texture2D>("2d/Animation/Bomb/Bomb9");
        }

        public static Texture2D LoadExplosionVertical(ContentManager content)
        {
            return content.Load<Texture2D>("2d/Animation/Bomb/Bomb10");
        }

        public static Dictionary<PowerUpType, Texture2D> LoadPowerUpTextures(ContentManager content)
        {
            return new Dictionary<PowerUpType, Texture2D>
            {
                [PowerUpType.Speed] = content.Load<Texture2D>("2d/PowerUps/RiceStar"),
                [PowerUpType.ExplosionRadius] = content.Load<Texture2D>("2d/PowerUps/SmokeStar"),
                [PowerUpType.Ghost] = content.Load<Texture2D>("2d/PowerUps/GhostStar"),
                [PowerUpType.Invincible] = content.Load<Texture2D>("2d/PowerUps/InvincibleStar"),
                [PowerUpType.HealthUp] = content.Load<Texture2D>("2d/PowerUps/HealthUp"),
            };
        }
    }
}
