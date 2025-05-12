using BombermanGame.Source.Engine.Input;
using System;

namespace BombermanGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var input = new PlayerInput();
            using var game = new Main(input);
            game.Run();
        }
    }
}