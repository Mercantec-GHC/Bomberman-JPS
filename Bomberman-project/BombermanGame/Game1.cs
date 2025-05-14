using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombermanGame.Source;
using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.Input;
using System.Collections.Generic;
using System;
using BombermanGame.Source.Engine.Map;

namespace BombermanGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private PlayerInput _input;
        List<Bomb> activeBombs = new List<Bomb>();
        Texture2D[] bombTextures;

        List<Vector2> activeExplosions = new List<Vector2>();

        Texture2D explosionCenter;
        Texture2D explosionHorizontal;
        Texture2D explosionVertical;

        Dictionary<Vector2, int> explosionTimers = new Dictionary<Vector2, int>();
        int explosionDuration = 30;
        int tileSize = 70;
        World world;

        Texture2D[] runningTextures;

        double bombCooldown = 2000;
        double timeSinceLastBomb = 0;
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

            runningTextures = new Texture2D[2];
            runningTextures[0] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human");
            runningTextures[1] = Content.Load<Texture2D>("2d/Animation/PlayerAnimation/Human2nd");

            bombTextures = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                bombTextures[i] = Content.Load<Texture2D>($"2d/Animation/Bomb/Bomb{i + 1}");
            }

            explosionCenter = Content.Load<Texture2D>("2d/Animation/Bomb/BombCenter");
            explosionHorizontal = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb9");
            explosionVertical = Content.Load<Texture2D>("2d/Animation/Bomb/Bomb10");

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
            timeSinceLastBomb += gameTime.ElapsedGameTime.TotalMilliseconds;

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

            Vector2 playerPos = world._player.Position;

            int tileX = (int)((playerPos.X + tileSize / 2) / tileSize);
            int tileY = (int)((playerPos.Y + tileSize / 2) / tileSize);
            Vector2 bombPos = new Vector2(tileX * tileSize, tileY * tileSize);

            // Check if the tile is not a wall
            if (_input.BombPlaced && timeSinceLastBomb >= bombCooldown && !world.Tilemap.IsWallAtTile(tileX, tileY))
            {
                Vector2 newBombPos = bombPos;
                activeBombs.Add(new Bomb(newBombPos));
                timeSinceLastBomb = 0;
            }

            // Update and check for finished bombs
            for (int i = activeBombs.Count - 1; i >= 0; i--)
            {
                activeBombs[i].Update(bombTextures);
                if (activeBombs[i].IsFinsihed)
                {
                    CreateExplosion(activeBombs[i].Position);
                    activeBombs.RemoveAt(i);
                }
            }

            // Update and remove explosions
            for (int i = activeExplosions.Count - 1; i >= 0; i--)
            {
                Vector2 pos = activeExplosions[i];

                if (explosionTimers.ContainsKey(pos))
                {
                    explosionTimers[pos]++;

                    if (explosionTimers[pos] > explosionDuration)
                    {
                        explosionTimers.Remove(pos);
                        activeExplosions.RemoveAt(i);
                    }
                }
                else
                {
                    activeExplosions.RemoveAt(i); // extra safety fallback
                }
            }

            _input.ResetActions();

            base.Update(gameTime);
        }




        private void CreateExplosion(Vector2 centerPos)
        {
            void AddExplosionTile(Vector2 pos)
            {
                if (!explosionTimers.ContainsKey(pos))
                {
                    activeExplosions.Add(pos);
                    explosionTimers[pos] = 0;

                    int tileX = (int)(pos.X / tileSize);
                    int tileY = (int)(pos.Y / tileSize);

                    if (world.Tilemap.IsBreakableBlockAtTile(tileX, tileY))
                    {
                        // Break the block (replace it with a ground tile)
                        world.Tilemap.BreakBlockAtTile(tileX, tileY);
                        return;
                    }
                }
            }

            AddExplosionTile(centerPos); // Center of the explosion

            Vector2[] directions = new[]
            {
                new Vector2(tileSize, 0), // Right
                new Vector2(-tileSize, 0), // Left
                new Vector2(0, tileSize), // Down
                new Vector2(0, -tileSize) // Up
            };

            foreach (var dir in directions)
            {
                for (int i = 1; i <= 3; i++) // Explosion can go up to 3 tiles away
                {
                    Vector2 next = centerPos + dir * i;
                    if (IsBlocked(next))
                        break; // Stop explosion if blocked by wall
                    AddExplosionTile(next);
                }
            }
        }

        private bool IsBlocked(Vector2 pos)
        {
            int tileX = (int)(pos.X / tileSize);
            int tileY = (int)(pos.Y / tileSize);

            if (tileX < 0 || tileY < 0 || tileX >= world.Tilemap.MapWidth || tileY >= world.Tilemap.MapHeight)
                return true;

            return world.Tilemap.IsWallAtTile(tileX, tileY);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin();
            world.Draw();

            foreach (var bomb in activeBombs)
            {
                bomb.Draw(Globals.spriteBatch, bombTextures);
            }

            Vector2 center = Vector2.Zero;
            if (activeExplosions.Count > 0)
                center = activeExplosions[0]; // Assume first added is center

            foreach (var pos in activeExplosions)
            {
                Texture2D textureToDraw;

                if (pos == center)
                    textureToDraw = explosionCenter;
                else if (Math.Abs(pos.Y - center.Y) < 1)
                    textureToDraw = explosionHorizontal;
                else
                    textureToDraw = explosionVertical;

                Globals.spriteBatch.Draw(textureToDraw, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), Color.White);
            }

            Globals.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
