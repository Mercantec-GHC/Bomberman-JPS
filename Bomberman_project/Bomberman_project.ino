#include <Arduino_MKRIoTCarrier.h>
#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "CommonClasses.h"

const int playerId = 2; // <<< CHANGE this to 2, 3, or 4 for other Arduinos

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

unsigned long lastGyroUpdate = 0;
String lastDirection = "";
unsigned long gyroInterval = 0;
unsigned long lastBombUpdate = 0;
const unsigned long BombCooldown = 3000;
float x, y, z;
float tiltThreshold = 0.2;

UUID uuid;  

void setup() {
  Serial.begin(9600);
  carrier.noCase();
  carrier.begin();

  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    delay(1000); 
  }

  Serial.println("Connecting to WiFi...");
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("Connected to WiFi");

  client.setServer(mqtt_server, mqtt_port);
}

void sendMQTTMessage(const String& type, const String& value) {
  if (!client.connected()) return;

  StaticJsonDocument<256> doc;
  doc["PlayerId"] = playerId;
  doc["Type"] = type;
  doc["Value"] = value;

  char buffer[256];
  serializeJson(doc, buffer);
  client.publish(mqttTopic, buffer);
}

void reconnect() {
  while (!client.connected()) {
    Serial.println("Attempting MQTT connection...");
    if (client.connect("ArduinoMKR", mqtt_username, mqtt_password)) {
      Serial.println("Connected to MQTT broker");
      client.subscribe(mqttTopic);
    } else {
      Serial.print("Failed, status code: ");
      Serial.println(client.state());
      delay(5000);
    }
  }
}

void PlaceBomb() {
  unsigned long currentMillis = millis();

  if (carrier.Buttons.onTouchDown(TOUCH2)) {
    if (currentMillis - lastBombUpdate >= BombCooldown) {
      lastBombUpdate = currentMillis;
      Serial.println("Bomb");
      sendMQTTMessage("bomb_press", "true");
    }
  }
}

void UsePowerUp() {
  if (carrier.Buttons.onTouchDown(TOUCH0)) {
    Serial.println("Powerup Activated");
    sendMQTTMessage("powerup_used", "true");
  }
}

void Gyro() {
  unsigned long currentMillis = millis();

  if (currentMillis - lastGyroUpdate >= gyroInterval) {
    lastGyroUpdate = currentMillis;

    if (IMU.accelerationAvailable()) {
      IMU.readAcceleration(x, y, z);

      String direction = "Idle";
      if (x > tiltThreshold) direction = "Right";
      else if (x < -tiltThreshold) direction = "Left";
      else if (y < -tiltThreshold) direction = "Down";
      else if (y > tiltThreshold) direction = "Up";

      if (direction != lastDirection && direction != "Idle") {
        Serial.println("MOVE: " + direction);
        sendMQTTMessage("tilt_move", direction);
        lastDirection = direction;
      }
    }
  }
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  carrier.Buttons.update();

  Gyro();
  PlaceBomb();   
  UsePowerUp();
}
