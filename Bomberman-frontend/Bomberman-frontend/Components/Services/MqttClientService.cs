using MQTTnet;
using MQTTnet.Protocol;

public class MqttClientService
{
    private IMqttClient _mqttClient;

    public async Task ConnectAsync()
    {
        var factory = new MqttClientFactory();
        _mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("c51faff71929461aa2c0afa84bb16875.s1.eu.hivemq.cloud", 8883)
            .WithCredentials("Silasbillum", "Silasbillum1")
            .Build();

        _mqttClient.DisconnectedAsync += async e =>
        {
            Console.WriteLine("Disconnected from MQTT Broker.");
            // Optionally try to reconnect
        };

        _mqttClient.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected successfully to MQTT Broker.");
        };

        await _mqttClient.ConnectAsync(options);
    }

    public async Task SubscribeAsync(string topic)
    {
        if (_mqttClient == null || !_mqttClient.IsConnected)
            throw new InvalidOperationException("Client not connected.");

        await _mqttClient.SubscribeAsync(topic);
    }

    public async Task PublishAsync(string topic, string payload)
    {
        if (_mqttClient == null || !_mqttClient.IsConnected)
            throw new InvalidOperationException("Client not connected.");

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtMostOnce)
            .Build();

        await _mqttClient.PublishAsync(message);
    }
}
