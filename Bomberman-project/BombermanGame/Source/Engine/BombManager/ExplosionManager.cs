using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BombermanGame.Source.Engine.PlayerManager;
using BombermanGame.Source.Engine.Map;
using System;

namespace BombermanGame.Source.Engine.BombManager
{
    public class ExplosionManager
    {
        private List<Vector2> activeExplosions = new List<Vector2>();
        private Dictionary<Vector2, int> explosionTimers = new Dictionary<Vector2, int>();
        private Texture2D explosionCenter;
        private Texture2D explosionHorizontal;
        private Texture2D explosionVertical;
        private int explosionDuration = 20;
        private int tileSize = 70;
        private List<Bomb> activeBombs = new List<Bomb>();
        private double bombCooldown = 2000;
        private double timeSinceLastBomb = 0;
        private World world;

        public ExplosionManager(World world, Texture2D center, Texture2D horizontal, Texture2D vertical)
        {
            this.world = world;
            explosionCenter = center;
            explosionHorizontal = horizontal;
            explosionVertical = vertical;
        }

        public void CreateExplosion(Vector2 centerPos)
        {
            void AddExplosionTile(Vector2 pos)
            {
                if (!explosionTimers.ContainsKey(pos))
                {
                    activeExplosions.Add(pos);
                    explosionTimers[pos] = 0;

                    int tileX = (int)(pos.X / tileSize);
                    int tileY = (int)(pos.Y / tileSize);

                    if (world.Tilemap.IsBreakableBlockAtTile(tileX, tileY))
                    {
                        world.Tilemap.BreakBlockAtTile(tileX, tileY);
                        return;
                    }
                }
            }

            AddExplosionTile(centerPos);

            Vector2[] directions = new[]
            {
                new Vector2(tileSize, 0),
                new Vector2(-tileSize, 0),
                new Vector2(0, tileSize),
                new Vector2(0, -tileSize)
            };

            foreach (var dir in directions)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Vector2 next = centerPos + dir * i;
                    if (IsBlocked(next)) break;
                    AddExplosionTile(next);
                }
            }
        }

        public void Update(List<Player> players)
        {
            for (int i = activeExplosions.Count - 1; i >= 0; i--)
            {
                Vector2 pos = activeExplosions[i];

                if (explosionTimers.ContainsKey(pos))
                {
                    explosionTimers[pos]++;

                    if (explosionTimers[pos] == 1)
                    {
                        Rectangle explosionRect = new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize);
                        CheckExplosionDamage(explosionRect, players);
                    }

                    if (explosionTimers[pos] > explosionDuration)
                    {
                        explosionTimers.Remove(pos);
                        activeExplosions.RemoveAt(i);
                    }
                }
                else
                {
                    activeExplosions.RemoveAt(i);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (activeExplosions.Count == 0) return;

            Vector2 center = activeExplosions[0];

            foreach (var pos in activeExplosions)
            {
                Texture2D textureToDraw;

                if (pos == center)
                    textureToDraw = explosionCenter;
                else if (Math.Abs(pos.Y - center.Y) < 1)
                    textureToDraw = explosionHorizontal;
                else
                    textureToDraw = explosionVertical;

                spriteBatch.Draw(textureToDraw, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), Color.White);
            }
        }

        private void CheckExplosionDamage(Rectangle explosionArea, List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.IsAlive && explosionArea.Intersects(player.BoundingBox))
                {
                    player.TakeDamage(1);
                }
            }
        }

        private bool IsBlocked(Vector2 pos)
        {
            int tileX = (int)(pos.X / tileSize);
            int tileY = (int)(pos.Y / tileSize);

            if (tileX < 0 || tileY < 0 || tileX >= world.Tilemap.MapWidth || tileY >= world.Tilemap.MapHeight)
                return true;

            return world.Tilemap.IsWallAtTile(tileX, tileY);
        }
    }
}
