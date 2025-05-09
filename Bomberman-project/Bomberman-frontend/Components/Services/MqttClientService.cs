using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using DomainModels;

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
            //Console.WriteLine($"Received MQTT message: {payload}");

            try
            {
                var status = JsonSerializer.Deserialize<GameStatus>(payload);
                if (status != null)
                {
                    if (status.Type == "tilt_move")
                        _playerInput.MoveDirection = status.Value;
                    else if (status.Type == "bomb_press")
                        _playerInput.BombPlaced = true;
                    else if (status.Type == "powerup_used")
                        _playerInput.PowerUpUsed = true;
                    //switch (status.Type)
                    //{
                    //    case "tilt_move":
                    //        Console.WriteLine($"Player moved: {status.Value}");
                    //        break;
                    //    case "bomb_press":
                    //        Console.WriteLine("Player placed a bomb.");
                    //        break;
                    //    case "powerup_used":
                    //        Console.WriteLine($"Power-up used: {status.Value}");
                    //        break;
                    //    case "life":
                    //        Console.WriteLine($"Life update: {status.Value}");
                    //        break;
                    //    case "game":
                    //        Console.WriteLine($"Game Restarted");
                    //        break;
                    //    default:
                    //        Console.WriteLine($"Unknown type: {status.Type}");
                    //        break;
                    //}
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