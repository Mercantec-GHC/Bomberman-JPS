using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using BombermanGame.Source.Engine.Map;
using BombermanGame.Source.Engine.PlayerManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BombermanGame.Source
{
    public class World
    {
        public Player _player;
        private Tilemap _tilemap;
        public Tilemap Tilemap => _tilemap;

        private Texture2D _tileTexture;
        private Texture2D _wallTexture;
        private Texture2D _breakable;

        public void Load(ContentManager content)
        {
            _wallTexture = content.Load<Texture2D>("2d/Wall");
            _tileTexture = content.Load<Texture2D>("2d/Tile");
            _breakable = content.Load<Texture2D>("2d/breakableBarrel");
            _player = new Player(null, new Vector2(50, 50));

            _tilemap = new Tilemap(10, 10, _tileTexture, _wallTexture, _breakable);
        }
        public void Update(PlayerInput input, GameTime gameTime)
        {
            _player.Update(input.MoveDirection, _tilemap, gameTime);
        }

        public void SetPlayerTextures(Texture2D[] textures)
        {
            if (_player != null)
                _player.SetTexture(textures[0]); // Start with first frame
        }

        

        public void Draw()
        {
            _tilemap.Draw(Globals.spriteBatch);
            _player.Draw(Globals.spriteBatch);

        }
    }
}
