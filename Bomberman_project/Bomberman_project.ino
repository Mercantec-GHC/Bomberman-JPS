#include <Arduino_MKRIoTCarrier.h>
#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include "config.h"

const char ssid[] = WIFI_SSID;
const char password[] = WIFI_PASSWORD;
const char mqtt_server[] = MQTT_SERVER;
const int mqtt_port = 8883;
const char mqtt_username[] = MQTT_USERNAME;
const char mqtt_password[] = MQTT_PASSWORD;
const char mqttTopic[] = "game/status";

WiFiSSLClient wifiClient;
PubSubClient client(wifiClient);

MKRIoTCarrier carrier;


void reconnect() {
  while (!client.connected()) {
    Serial.println("Attempting MQTT connection...");
    if (client.connect("ArduinoMKR", mqtt_username, mqtt_password)) {
      Serial.println("Connected to MQTT broker");
      client.subscribe(mqttTopic);  // Subscribe to the topic
    } else {
      Serial.print("Failed, status code: ");
      Serial.println(client.state());
      delay(5000);
    }
  }
}


float x, y, z;
float tiltThreshold = 0.3;
void setup() {
  Serial.begin(9600);
  IMU.begin();
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    while (1);
}
  
}


  Serial.println("Connecting to WiFi...");
  WiFi.begin(ssid, password);  // Use your Wi-Fi credentials
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("Connected to WiFi");

  client.setServer(mqtt_server, mqtt_port);  // Set the MQTT server and port


    
    


void loop() {
 if (IMU.accelerationAvailable()) {
    IMU.readAcceleration(x,y,z);   

     if (y > tiltThreshold) {
      Serial.println("MOVE LEFT");
    } else if (y < -tiltThreshold) {
      Serial.println("MOVE RIGHT");
    } else if (x > tiltThreshold) {
      Serial.println("MOVE UP");
    } else if (x < -tiltThreshold) {
      Serial.println("MOVE DOWN");
    }else {
      Serial.println("IDLE");
    }    
  }
  delay(200);
  

  if (!client.connected()) {
  reconnect();
  }
  client.loop();

  String Message = "Hello World!";
  client.publish("game/status", Message.c_str());
  delay(30000);
  

}
