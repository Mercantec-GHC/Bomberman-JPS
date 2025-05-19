using BombermanGame.Source.Engine.BombManager;
using BombermanGame.Source;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

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

    public void Update(GameTime gameTime, Vector2 playerPosition, bool bombPlaced)
    {
        timeSinceLastBomb += gameTime.ElapsedGameTime.TotalMilliseconds;

        int tileX = (int)((playerPosition.X + tileSize / 2) / tileSize);
        int tileY = (int)((playerPosition.Y + tileSize / 2) / tileSize);
        Vector2 bombPos = new Vector2(tileX * tileSize, tileY * tileSize);

        if (bombPlaced && timeSinceLastBomb >= bombCooldown && !world.Tilemap.IsWallAtTile(tileX, tileY) && world._player.IsAlive)
        {
            activeBombs.Add(new Bomb(bombPos));
            timeSinceLastBomb = 0;
        }

        for (int i = activeBombs.Count - 1; i >= 0; i--)
        {
            activeBombs[i].Update(bombTextures);
            if (activeBombs[i].IsFinsihed)
            {
                explosionManager.CreateExplosion(activeBombs[i].Position);
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