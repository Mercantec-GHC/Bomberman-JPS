using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanGame.Source.Engine.Map
{
    public class Tilemap
    {
        private Tile[,] _tiles;
        private int _tileWidth;
        private int _tileHeight;
        private Texture2D _tileTexture;
        private Texture2D _wallTexture;

        private int[,] _map = new int[,]
        {
            { 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 1, 1, 1, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2},
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
        };

        public Tilemap(int width, int height, Texture2D tileTexture, Texture2D wallTexture)
        {
            _tileTexture = tileTexture;
            _wallTexture = wallTexture;

            _tileWidth = tileTexture.Width;
            _tileHeight = tileTexture.Height;

            int mapWidth = _map.GetLength(1);  
            int mapHeight = _map.GetLength(0); 

            _tiles = new Tile[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int tileType = _map[y, x];
                    Texture2D texture = null;

                    if (tileType == 0)
                        continue;
                    else if (tileType == 1)
                        texture = _tileTexture;
                    else if (tileType == 2)
                        texture = _wallTexture;

                    _tiles[x, y] = new Tile(
                        texture,
                        new Rectangle(x * _tileWidth, y * _tileHeight, _tileWidth, _tileHeight),
                        tileType == 2
                    );
                }
            }
        } 
        

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in _tiles)
            {
                if (tile != null)
                {
                    spriteBatch.Draw(tile.Texture, tile.Position, Color.White);
                }
            }
        }

        public bool IsTileCollidable(Rectangle playerBounds)
        {
            // Expand the player bounds slightly for collision detection
            Rectangle collisionBounds = new Rectangle(
             playerBounds.X + 4,  
             playerBounds.Y + 4,  
             playerBounds.Width - 8, 
             playerBounds.Height - 8  
        );

            foreach (var tile in _tiles)
            {
                if (tile != null && tile.IsWall && tile.Position.Intersects(collisionBounds))
                {
                    return true; // Prevent movement if there's a collision
                }
            }
            return false;
        }

    }
}
