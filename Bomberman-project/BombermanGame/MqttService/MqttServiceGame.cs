using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using BombermanGame.Source.Engine.Input;
using System.Collections.Generic;

namespace BombermanGame.MqttService
{
    internal class MqttServiceGame
    {
        public class MqttClientService : BackgroundService
        {
            private IConfiguration _configuration;
            private IMqttClient _mqttClient;
            private readonly List<PlayerInput> _playerInputs;

            public MqttClientService(IConfiguration configuration, List<PlayerInput> inputs)
            {
                _configuration = configuration;
                _playerInputs = inputs;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(_configuration["HiveMQ:Host"], 8883)
                    .WithCredentials(_configuration["HiveMQ:Username"], _configuration["HiveMQ:Password"])
                    .WithTls()
                    .Build();

                _mqttClient = new MqttFactory().CreateMqttClient();

                _mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    Console.WriteLine($"Received MQTT message: {payload}");

                    try
                    {
                        var status = JsonSerializer.Deserialize<GameStatus>(payload);

                        int playerIndex = status.PlayerId - 1;

                        if (playerIndex >= 0 && playerIndex < _playerInputs.Count)
                        {
                            Console.WriteLine($"Player {status.PlayerId} input: Type={status.Type}, Value={status.Value}");
                            var input = _playerInputs[playerIndex];
                            input.SetFromMQTT(status.Type, status.Value);
                        }
                        else
                        {
                            Console.WriteLine($"Received PlayerId {status.PlayerId} which is out of range.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to parse message: {ex.Message}");
                    }

                    await Task.CompletedTask;
                };

                _mqttClient.ConnectedAsync += async e =>
                {
                    Console.WriteLine("Connected to MQTT broker.");
                    await _mqttClient.SubscribeAsync("game/status");
                    Console.WriteLine("Subscribed to topic: game/status");
                };

                _mqttClient.DisconnectedAsync += async e =>
                {
                    Console.WriteLine("Disconnected. Trying to reconnect...");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    await _mqttClient.ConnectAsync(options, stoppingToken);
                };

                try
                {
                    await _mqttClient.ConnectAsync(options, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"MQTT Connection failed: {ex.Message}");
                }
            }
        }

        public class GameStatus
        {
            // These must match exactly the JSON keys from Arduino (case-sensitive!)
            [JsonPropertyName("PlayerId")]
            public int PlayerId { get; set; }

            [JsonPropertyName("Type")]
            public string Type { get; set; }

            [JsonPropertyName("Value")]
            public string Value { get; set; }
        }
    }
}
