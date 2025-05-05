using MQTTnet;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

public class MqttClientService
{
    private IMqttClient _mqttClient;

    public async Task ConnectAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttClientFactory();
        _mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("c51faff71929461aa2c0afa84bb16875.s1.eu.hivemq.cloud", 8883)
            .WithCredentials("Silasbillum", "Silasbillum1")
            .Build();

        _mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            try
            {
                var status = JsonSerializer.Deserialize<GameStatus>(payload);
                if (status != null)
                {
                    switch (status.Type)
                    {
                        case "tilt_move":
                            Console.WriteLine($"Player moved: {status.Value}");
                            break;
                        case "bomb_press":
                            Console.WriteLine("Player placed a bomb.");
                            break;
                        case "powerup_used":
                            Console.WriteLine($"Power-up used: {status.Value}");
                            break;
                        case "life":
                            Console.WriteLine($"Life update: {status.Value}");
                            break;
                        default:
                            Console.WriteLine($"Unknown type: {status.Type}");
                            break;
                    }
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
            Console.WriteLine("✅ Connected to MQTT broker.");
            await _mqttClient.SubscribeAsync("game/status");
            Console.WriteLine("📡 Subscribed to topic: game/status");
        };

        _mqttClient.DisconnectedAsync += async e =>
        {
            Console.WriteLine("⚠️ Disconnected. Trying to reconnect...");
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            await _mqttClient.ConnectAsync(options, stoppingToken);
        };

        try
        {
            await _mqttClient.ConnectAsync(options, stoppingToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ MQTT Connection failed: {ex.Message}");
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
