using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanGame.Source;
using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using BombermanGame.Source.Engine.Map;
using BombermanGame.Source.Engine.PlayerManager;
using BombermanGame.Source.Engine.BombManager;
using System.Collections.Generic;
using System;
using BombermanGame.Source.Engine.Content;
using BombermanGame.Source.Engine.PowerUps;

namespace BombermanGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private PlayerInput _input;

        private Texture2D[] bombTextures;
        private Texture2D[] runningTextures;

        private World world;
        private readonly List<PlayerInput> _inputs;
        private readonly List<Player> _players = new();

        private ExplosionManager explosionManager;
        private BombManager bombManager;

        private FrameAnimator playerAnimator;

        private PowerUpManager powerUpManager;

        enum GameState
        {
            Playing,
            Winner
        }

        GameState gameState = GameState.Playing;
        bool isGameOver = false;
        int winningPlayerId = -1;
        double gameOverTimer = 0;



        public Main(List<PlayerInput> inputs)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            _inputs = inputs;

        }

        protected override void Initialize()
        {
            var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Window.IsBorderless = true;

            var textures = TextureLoader.LoadPowerUpTextures(Content);

            powerUpManager = new PowerUpManager(textures);

            
            base.Initialize();
        }

        protected override void LoadContent()
        {

            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            runningTextures = TextureLoader.LoadPlayerTextures(Content);
            bombTextures = TextureLoader.LoadBombTextures(Content);

            Texture2D explosionCenter = TextureLoader.LoadExplosionCenter(Content);
            Texture2D explosionHorizontal = TextureLoader.LoadExplosionHorizontal(Content);
            Texture2D explosionVertical = TextureLoader.LoadExplosionVertical(Content);

            var textures = TextureLoader.LoadPowerUpTextures(Content);
            powerUpManager = new PowerUpManager(textures);

            world = new World();
            world.Load(Content);

            int tileWidth = world.Tilemap.TileSize;
            int tileHeight = world.Tilemap.TileSize;
            int mapWidth = world.Tilemap.MapWidth;
            int mapHeight = world.Tilemap.MapHeight;

            Vector2[] spawnPositions = new Vector2[]
            {
        new Vector2(tileWidth, tileHeight),                                      // Top-left
        new Vector2((mapWidth - 2) * tileWidth, tileHeight),                    // Top-right
        new Vector2(tileWidth, (mapHeight - 2) * tileHeight),                   // Bottom-left
        new Vector2((mapWidth - 2) * tileWidth, (mapHeight - 2) * tileHeight),  // Bottom-right
            };

            // Spawn exactly one player per spawn position, max 4 players
            for (int i = 0; i < _inputs.Count && i < 4; i++)
            {
                var player = new Player(runningTextures[0], spawnPositions[i]);
                world.AddPlayer(player);
                world.SetPlayerTexture(i, runningTextures[0]);
            }

            explosionManager = new ExplosionManager(world, explosionCenter, explosionHorizontal, explosionVertical, powerUpManager);
            bombManager = new BombManager(world, bombTextures, explosionManager);

            playerAnimator = new FrameAnimator(runningTextures.Length);

            // Removed extra manual players here (player1, player2)

            world.SetPowerUpTextures(textures);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int playerCount = world.Players.Count;
            int inputCount = _inputs.Count;

            for (int i = 0; i < playerCount; i++)
            {
                if (i >= inputCount)
                    continue;

                var input = _inputs[i];
                input.HandleKeyboardInput();

                string dir = input.MoveDirection?.ToLowerInvariant();
                string playerDir = dir switch
                {
                    "left" => "Left",
                    "right" => "Right",
                    "up" => "Up",
                    "down" => "Down",
                    _ => "Idle"
                };

                world.Players[i].Update(playerDir, world.Tilemap, gameTime);

                if (input.BombPlaced)
                    bombManager.PlaceBomb(world.Players[i].Position, i);
                bombManager.UpdateBombs(gameTime);

                if (input.PowerUpUsed)
                    world.Players[i].UseStoredPowerUp();

                input.ResetActions();
            }

            // Run bomb animations & explosions regardless of user input
            bombManager.UpdateBombs(gameTime);
            explosionManager.Update(world.Players);

            int currentFrame = playerAnimator.UpdateAndGetFrame();
            for (int i = 0; i < playerCount; i++)
            {
                world.SetPlayerTexture(i, runningTextures[currentFrame]);
            }

            foreach (var player in world.Players)
            {
                powerUpManager.Update(player);
            }

            if (!isGameOver)
            {
                int? winnerId = world.CheckWinner();
                if (winnerId.HasValue)
                {
                    isGameOver = true;
                    winningPlayerId = winnerId.Value;
                }
            }
            else
            {
                gameOverTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (gameOverTimer <= 0)
                {
                    gameState = GameState.Winner; 
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Globals.spriteBatch.Begin();

            if (gameState == GameState.Winner && winningPlayerId != null)
            {
                string winnerText = $"Player {winningPlayerId} Wins!";
                Vector2 textSize = world._font.MeasureString(winnerText);
                Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);

                Globals.spriteBatch.DrawString(world._font, winnerText, screenCenter - textSize / 2f, Color.Gold);
            }
            else
            {
                world.Draw();
                bombManager.Draw(Globals.spriteBatch);
                explosionManager.Draw(Globals.spriteBatch);
                powerUpManager.Draw(Globals.spriteBatch);
            }

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
