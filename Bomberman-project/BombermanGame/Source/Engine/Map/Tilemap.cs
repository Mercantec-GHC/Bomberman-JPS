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
        private int _tileSize;
        private Texture2D _tileTexture;
        private Texture2D _wallTexture;
        private Texture2D _breakable;

        private int[,] _map = new int[,]
{
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
    { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 1, 1, 1, 2 },
    { 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2 },
    { 2, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
    { 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2 },
    { 2, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 2 },
    { 2, 1, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 3, 2, 3, 2 },
    { 2, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 2 },
    { 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2 },
    { 2, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 1, 2 },
    { 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2 },
    { 2, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 2 },
    { 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2 },
    { 2, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 2 },
    { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
};


        public int MapWidth => _map.GetLength(1);
        public int MapHeight => _map.GetLength(0);
        public int TileSize => _tileSize;

        public Tilemap(int width, int height, Texture2D tileTexture, Texture2D wallTexture, Texture2D breakable)
        {
            _tileTexture = tileTexture;
            _wallTexture = wallTexture;
            _breakable = breakable;
            _tileSize = 70;

            _tiles = new Tile[MapWidth, MapHeight];

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int tileType = _map[y, x];
                    Texture2D texture = null;

                    if (tileType == 2) // Wall
                        texture = _wallTexture;
                    else if (tileType == 1) // Ground tile
                        texture = _tileTexture;
                    else if (tileType == 3) // Breakable block
                        texture = _breakable;

                    if (texture != null)
                    {
                        _tiles[x, y] = new Tile(
                            texture,
                            new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize),
                            tileType == 2
                        );
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in _tiles)
            {
                if (tile != null)
                    spriteBatch.Draw(tile.Texture, tile.Position, Color.White);
            }
        }

        public bool IsWallAtTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MapWidth || y >= MapHeight)
                return true;
            return _map[y, x] == 2;
        }

        public bool IsBreakableBlockAtTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MapWidth || y >= MapHeight)
                return false;
            return _map[y, x] == 3;
        }

        public void BreakBlockAtTile(int x, int y)
        {
            if (IsBreakableBlockAtTile(x, y))
            {
                // Replace the breakable block with a ground tile (1)
                _map[y, x] = 1; // Ground tile
                _tiles[x, y] = new Tile(_tileTexture, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), false);
            }
        }

        public bool IsTileCollidable(Rectangle playerBounds)
        {
            Rectangle collisionBounds = new Rectangle(
                playerBounds.X + 4,
                playerBounds.Y + 4,
                playerBounds.Width - 8,
                playerBounds.Height - 8
            );

            foreach (var tile in _tiles)
            {
                if (tile != null && (tile.IsWall || IsBreakableBlockAtTile(tile.Position.X / _tileSize, tile.Position.Y / _tileSize)))
                {
                    if (tile.Position.Intersects(collisionBounds))
                        return true;
                }
            }

            return false;
        }
    }

}
