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

namespace BombermanGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private PlayerInput _input;

        private Texture2D[] bombTextures;
        private Texture2D[] runningTextures;

        private int counter;
        private int activateFrame;

        private World world;
        private ExplosionManager explosionManager;
        private BombManager bombManager;

        private FrameAnimator playerAnimator;

        public Main(PlayerInput input)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            _input = input;
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

            world = new World();
            world.Load(Content);
            world.SetPlayerTextures(new Texture2D[] { runningTextures[0] });

            explosionManager = new ExplosionManager(world, explosionCenter, explosionHorizontal, explosionVertical);
            bombManager = new BombManager(world, bombTextures, explosionManager);

            playerAnimator = new FrameAnimator(runningTextures.Length);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _input.HandleKeyboardInput();
            world.Update(_input, gameTime);

            int currentFrame = playerAnimator.UpdateAndGetFrame();
            world.SetPlayerTextures(new Texture2D[] { runningTextures[currentFrame] });

            bombManager.Update(gameTime, world._player.Position, _input.BombPlaced);
            explosionManager.Update(new List<Player> { world._player });

            _input.ResetActions();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin();
            world.Draw();

            bombManager.Draw(Globals.spriteBatch);
            explosionManager.Draw(Globals.spriteBatch);

            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
