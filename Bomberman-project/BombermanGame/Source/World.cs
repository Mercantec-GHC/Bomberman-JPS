using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using BombermanGame.Source.Engine.Map;
using BombermanGame.Source.Engine.PlayerManager;
using BombermanGame.Source.Engine.PowerUps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombermanGame.Source
{
    public class World
    {
        public List<Player> _players = new List<Player>();
        public List<Player> Players => _players;

        private Tilemap _tilemap;
        public Tilemap Tilemap => _tilemap;

        private Texture2D _tileTexture;
        private Texture2D _wallTexture;
        private Texture2D _breakable;
        private Texture2D _specialTexture;
        public SpriteFont _font;



        public void Load(ContentManager content)
        {
            _wallTexture = content.Load<Texture2D>("2d/Wall");
            _tileTexture = content.Load<Texture2D>("2d/Tile");
            _breakable = content.Load<Texture2D>("2d/breakableBarrel");
            _specialTexture = content.Load<Texture2D>("2d/Wall");

            _tilemap = new Tilemap(10, 10, _tileTexture, _wallTexture, _breakable, _specialTexture);

            _font = Globals.content.Load<SpriteFont>("DefaultFont");
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void UpdatePlayer(int index, PlayerInput input, GameTime gameTime)
        {
            if (index >= 0 && index < _players.Count)
            {
                _players[index].Update(input.MoveDirection, _tilemap, gameTime);

                if (input.PowerUpUsed)
                    _players[index].UseStoredPowerUp();
            }
        }

        public void SetPlayerTexture(int index, Texture2D texture)
        {
            if (index >= 0 && index < _players.Count)
            {
                _players[index].SetTexture(texture);
            }
        }

        public void SetPowerUpTextures(Dictionary<PowerUpType, Texture2D> textures)
        {
            
            foreach (var player in _players)
                player.SetPowerUpIcons(textures);
        }

        public int? CheckWinner()
        {
            int aliveCount = 0;
            int lastAlivePlayerId = -1;

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsAlive)
                {
                    aliveCount++;
                    lastAlivePlayerId = i;
                }
            }

            if (aliveCount == 1)
                return lastAlivePlayerId; // return winning player index (ID)

            return null; // no winner yet
        }
       
        public void Draw()
        {
            _tilemap.Draw(Globals.spriteBatch);
            foreach (var player in _players)
                player.Draw(Globals.spriteBatch, _font);
        }
    }
}
