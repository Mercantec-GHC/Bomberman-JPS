#include <Arduino_MKRIoTCarrier.h>
#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>
#include <PubSubClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "CommonClasses.h"

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
const unsigned long gyroInterval = 700;
unsigned long lastBombUpdate = 0;
const unsigned long BombCooldown = 3000;
float x, y, z;
float tiltThreshold = 0.3;
UUID uuid;  

void setup() {
  Serial.begin(9600);
  carrier.noCase();
 carrier.begin();
  
  // Initialize IMU
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    while (1); 
  }

Serial.println("Connecting to WiFi...");
   WiFi.begin(ssid, password);  // Use your Wi-Fi credentials
   while (WiFi.status() != WL_CONNECTED) {
     delay(1000);
     Serial.print(".");
   }
   Serial.println("Connected to WiFi");
 
   client.setServer(mqtt_server, mqtt_port);  // Set the MQTT server and port
   
 // Generate
  uuid.seed(random(0, 65535));  
  uuid.generate();
  
}


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
    void PlaceBomb(){
      unsigned long currentMillis = millis();

    if(carrier.Buttons.onTouchDown(TOUCH2)){
      if (currentMillis - lastBombUpdate >= BombCooldown) {
      lastBombUpdate = currentMillis;
      Serial.println("Bomb");
      String Message = "Bomb";
       client.publish("game/status", Message.c_str());
    }
   }
  }

  void Gyro(){
    unsigned long currentMillis = millis();
  if (currentMillis - lastGyroUpdate >= gyroInterval) {
    lastGyroUpdate = currentMillis;

if (IMU.accelerationAvailable()) {
    IMU.readAcceleration(x,y,z);  

    String Message; 

     if (y > tiltThreshold) {
      Serial.println("MOVE LEFT");
      String Message = "Move Left";
       client.publish("game/status", Message.c_str());
    } else if (y < -tiltThreshold) {
      Serial.println("MOVE RIGHT");
      String Message = "Move Right";
       client.publish("game/status", Message.c_str());
    } else if (x > tiltThreshold) {
      Serial.println("MOVE UP");
      String Message = "Move Up";
       client.publish("game/status", Message.c_str());
    } else if (x < -tiltThreshold) {
      Serial.println("MOVE DOWN");
      String Message = "Move Down";
       client.publish("game/status", Message.c_str());
    }else {
      Serial.println("IDLE");
      String Message = "Idle";
       client.publish("game/status", Message.c_str());
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
  

}
