using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BombermanGame.Source;
using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;



namespace BombermanGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private PlayerInput _input;

        World world;

        Texture2D[] runningTextures;

        Texture2D[] Bomb;

        int counter;
        int activateFrame;

        int bombFrame = 0;
        int bombCounter = 0;
        int bombFrameSpeed = 10;
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
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            runningTextures = new Texture2D[2];

            runningTextures[0] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human");
            runningTextures[1] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human2nd");

            Bomb = new Texture2D[8];

            Bomb[0] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb1");
            Bomb[1] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb2");
            Bomb[2] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb3");
            Bomb[3] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb4");
            Bomb[4] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb5");
            Bomb[5] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb6");
            Bomb[6] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb7");
            Bomb[7] = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb8");

            world = new World();
            world.Load(Content);
            world.SetPlayerTextures(runningTextures);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //world.Update();

            _input.HandleKeyboardInput();
            world.Update(_input);
            _input.ResetActions();

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

            bombCounter++;
            if (bombCounter > bombFrameSpeed)
            {
                bombCounter = 0;
                bombFrame++;
                if (bombFrame >= Bomb.Length)
                    bombFrame = 0;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Globals.spriteBatch.Begin();
            world.Draw();

            Globals.spriteBatch.Draw(Bomb[bombFrame], new Rectangle(300, 300, 300, 200), Color.White);
            Globals.spriteBatch.End();
            
            

            base.Draw(gameTime);
        }
    }
}