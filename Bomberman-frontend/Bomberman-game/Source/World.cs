using BombermanGame.Source.Engine;
using BombermanGame.Source.Engine.PlayerManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;
using BombermanGame.Source.Engine.Input;

namespace BombermanGame.Source
{
    public class World
    {

        public Player _player;

        //public void Load(ContentManager content)
        //{
        //    var texture = content.Load<Texture2D>("2d/RedTrans");
        //    _player = new  Player (texture, new  Vector2(300, 300));
        //}
        //public void Update(PlayerInput input)
        //{
        //    _player.Update(input.MoveDirection);
        //}

        //public void Draw()
        //{
        //    _player.Draw(Globals.spriteBatch);
        //}
    }
}
