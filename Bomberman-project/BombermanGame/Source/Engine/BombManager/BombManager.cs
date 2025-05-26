using BombermanGame.Source.Engine.BombManager;
using BombermanGame.Source;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

public class BombManager
{
    private List<Bomb> activeBombs = new List<Bomb>();
    private Texture2D[] bombTextures;
    private ExplosionManager explosionManager;
    private World world;

    private double bombCooldown = 2000;
    private double timeSinceLastBomb = 0;
    private const int tileSize = 70;

    public BombManager(World world, Texture2D[] bombTextures, ExplosionManager explosionManager)
    {
        this.world = world;
        this.bombTextures = bombTextures;
        this.explosionManager = explosionManager;
    }

    public void PlaceBomb(Vector2 playerPosition, int playerIndex)
    {
        int tileX = (int)((playerPosition.X + tileSize / 2) / tileSize);
        int tileY = (int)((playerPosition.Y + tileSize / 2) / tileSize);
        Vector2 bombPos = new Vector2(tileX * tileSize, tileY * tileSize);

        if (playerIndex < 0 || playerIndex >= world.Players.Count)
            return;

        var player = world.Players[playerIndex];

        if (timeSinceLastBomb >= bombCooldown && !world.Tilemap.IsWallAtTile(tileX, tileY) && player.IsAlive)
        {
            Console.WriteLine($"Player {playerIndex + 1} placed a bomb at {bombPos}");
            activeBombs.Add(new Bomb(bombPos, playerIndex));

            timeSinceLastBomb = 0;
        }
    }

    public void UpdateBombs(GameTime gameTime)
    {
        timeSinceLastBomb += gameTime.ElapsedGameTime.TotalMilliseconds;

        for (int i = activeBombs.Count - 1; i >= 0; i--)
        {
            activeBombs[i].Update(bombTextures);

            if (activeBombs[i].IsFinsihed)
            {
                var bomb = activeBombs[i];
                var player = world.Players[bomb.PlayerIndex];

                int radius = player.ExplosionRadius;
                if (player.HasBonusRadius && player.BonusRadius > 0)
                {
                    radius += player.BonusRadius;
                    player.BonusRadius = 0;
                    player.HasBonusRadius = false;
                }

                explosionManager.CreateExplosion(bomb.Position, radius);
                activeBombs.RemoveAt(i);
            }

        }
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var bomb in activeBombs)
        {
            bomb.Draw(spriteBatch, bombTextures);
        }
    }
}
