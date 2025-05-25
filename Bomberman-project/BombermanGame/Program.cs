using BombermanGame.Source.Engine.Input;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Collections.Generic;
using static BombermanGame.MqttService.MqttServiceGame;

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

            int playerCount = 4;
            var inputs = new List<PlayerInput>();
            for (int i = 0; i < playerCount; i++)
                inputs.Add(new PlayerInput());

            var mqttService = new MqttClientService(config, inputs);
            mqttService.StartAsync(CancellationToken.None); // start MQTT listener async

            using var game = new Main(inputs);
            game.Run();
        }
    }
}
