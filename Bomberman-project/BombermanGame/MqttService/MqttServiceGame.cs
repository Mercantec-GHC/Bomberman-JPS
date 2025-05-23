﻿using System;
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

namespace BombermanGame.MqttService
{
    internal class MqttServiceGame
    {
 

public class MqttClientService : BackgroundService
    {

        private IConfiguration _configuration;
        private IMqttClient _mqttClient;
        private readonly PlayerInput _playerInput;
        public MqttClientService(IConfiguration configuration, PlayerInput input)
        {
            _configuration = configuration;
            _playerInput = input;

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

                try
                {
                    var status = JsonSerializer.Deserialize<GameStatus>(payload);
                    if (status != null)
                    {
                        _playerInput.SetFromMQTT(status.Type, status.Value);
                    }
                    if (status.Type == "tilt_move")
                            _playerInput.MoveDirection = status.Value;
                        else if (status.Type == "bomb_press")
                            _playerInput.BombPlaced = true;
                        else if (status.Type == "powerup_used")
                            _playerInput.PowerUpUsed = true;                       
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
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
}
