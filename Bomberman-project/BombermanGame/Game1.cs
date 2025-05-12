using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BombermanGame.Source;
using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using SharpDX.Direct2D1;
using System.Collections.Generic;



namespace BombermanGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private PlayerInput _input;
        List<Bomb> activeBombs = new List<Bomb>();
        Texture2D[] bombTextures;

        World world;

        Texture2D[] runningTextures;


        int counter;
        int activateFrame;

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
            // TODO: Add your initialization logic here

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
            Globals.spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            runningTextures = new Texture2D[2];

            runningTextures[0] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human");
            runningTextures[1] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human2nd");

            bombTextures = new Texture2D[10];

            for (int i = 0; i < 10; i++) 
            {
                bombTextures[i] = Content.Load<Texture2D>($"2d/Animation/Bomb/Bomb{i + 1}");
            }            

            world = new World();
            world.Load(Content);
            world.SetPlayerTextures(runningTextures);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _input.HandleKeyboardInput();
            world.Update(_input);
          

            counter++;
            if (counter > 25)
            {
                counter = 0;
                activateFrame++;

                if (activateFrame > runningTextures.Length - 1)
                {
                    activateFrame = 0;
                }


                world.SetPlayerTextures(new Texture2D[] { runningTextures[activateFrame] });
            }

            if (_input.BombPlaced)
            {
                Vector2 playerPos = world._player.Position;
                activeBombs.Add(new Bomb(playerPos));
            }

            for(int i = activeBombs.Count - 1; i >= 0; i--)
            {
                activeBombs[i].Update(bombTextures);
                if(activeBombs[i].IsFinsihed)
                {
                    activeBombs.RemoveAt(i);
                }
            }
            _input.ResetActions();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Globals.spriteBatch.Begin();
            world.Draw();

            foreach(var bomb in activeBombs)
            {
                bomb.Draw(Globals.spriteBatch, bombTextures);
            }
            
            Globals.spriteBatch.End();
            
            

            base.Draw(gameTime);
        }
    }
}