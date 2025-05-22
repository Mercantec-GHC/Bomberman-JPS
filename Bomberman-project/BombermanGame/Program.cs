using BombermanGame.Source.Engine.Input;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Configuration.Json;
using static BombermanGame.MqttService.MqttServiceGame;
using System.Threading;

namespace BombermanGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

            var playerInput = new PlayerInput();

            var mqttService = new MqttClientService(config, playerInput);
            mqttService.StartAsync(CancellationToken.None);
            using var game = new Main(playerInput);
            game.Run();
        }
    }
}