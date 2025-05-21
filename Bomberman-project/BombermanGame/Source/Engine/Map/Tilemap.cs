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
        private Texture2D _specialTexture;

        private int[,] _map = new int[,]
{
    { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
    { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 1, 1, 1, 4 },
    { 4, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 4 },
    { 4, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 3, 3, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 4 },
    { 4, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 4 },
    { 4, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 3, 4 },
    { 4, 1, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 3, 2, 3, 4 },
    { 4, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 4 },
    { 4, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 4 },
    { 4, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 1, 4 },
    { 4, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 2, 3, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 4 },
    { 4, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 4 },
    { 4, 1, 2, 1, 2, 1, 2, 3, 2, 1, 2, 3, 2, 1, 2, 1, 2, 3, 2, 1, 2, 1, 2, 1, 4 },
    { 4, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 3, 1, 1, 1, 4 },
    { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
};


        public int MapWidth => _map.GetLength(1);
        public int MapHeight => _map.GetLength(0);
        public int TileSize => _tileSize;

        public Tilemap(int width, int height, Texture2D tileTexture, Texture2D wallTexture, Texture2D breakable, Texture2D specialTexture)
        {
            _tileTexture = tileTexture;
            _wallTexture = wallTexture;
            _breakable = breakable;
            _specialTexture = specialTexture;
            _tileSize = 70;

            _tiles = new Tile[MapWidth, MapHeight];

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int tileType = _map[y, x];
                    Texture2D texture = null;
                    bool isCollidable = false;

                    if (tileType == 1) // Ground
                    {
                        texture = _tileTexture;
                        isCollidable = false;
                    }
                    else if (tileType == 2) // Wall
                    {
                        texture = _wallTexture;
                        isCollidable = true;
                    }
                    else if (tileType == 3) // Breakable block
                    {
                        texture = _breakable;
                        isCollidable = true;
                    }
                    else if (tileType == 4) // Special solid tile
                    {
                        texture = _specialTexture;
                        isCollidable = true;
                    }

                    if (texture != null)
                    {
                        _tiles[x, y] = new Tile(texture, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), isCollidable);
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
            return _map[y, x] == 2 || _map[y, x] == 4;
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
        public bool IsOnGroundTile(Rectangle bounds)
        {
            int tileX = bounds.Center.X / _tileSize;
            int tileY = bounds.Center.Y / _tileSize;

            if (tileX < 0 || tileY < 0 || tileX >= MapWidth || tileY >= MapHeight)
                return false;

            return _map[tileY, tileX] == 1;
        }


        public bool IsTileCollidable(Rectangle playerBounds, bool isGhost = false)
        {
            foreach (var tile in _tiles)
            {
                if (tile == null) continue;

                int tileX = tile.Position.X / _tileSize;
                int tileY = tile.Position.Y / _tileSize;

                int tileType = _map[tileY, tileX];

                if (isGhost)
                {
                    // Ghost mode: collide only with type 4
                    if (tileType == 4 && tile.Position.Intersects(playerBounds))
                        return true;
                }
                else
                {
                    // Normal mode: collide with type 2 (wall), 3 (breakable), and 4 (special)
                    if ((tileType == 2 || tileType == 3 || tileType == 4) && tile.Position.Intersects(playerBounds))
                        return true;
                }
            }
            return false;
        }

    }

}
