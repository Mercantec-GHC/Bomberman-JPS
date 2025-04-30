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
int playerLives = 3;
UUID uuid;  

void setup() {
  Serial.begin(9600);
  carrier.noCase();
 carrier.begin();
  displayLives();
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

   
  /* 
 // Generate
  uuid.seed(random(0, 65535));  
  uuid.generate();
  */
}

  void sendMQTTMessage(const String& type, const String& value) {
    StaticJsonDocument<128> doc;
    doc["type"] = type;
    doc["value"] = value;

    char buffer[128];
    serializeJson(doc, buffer);
    client.publish(mqttTopic, buffer);
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
       sendMQTTMessage("bomb_press", "true");
    }
   }
  }

  void UsePowerUp(){
      
    if(carrier.Buttons.onTouchDown(TOUCH0)){     
      Serial.println("Powerup Activated");

      int powerUp = random(1, 5);

      String powerUpMsg;

      if (powerUp == 1) {
        powerUpMsg = "Speed";
      }else if (powerUp == 2) {
        powerUpMsg = "GhostWalk";
      }else if (powerUp == 3) {
        powerUpMsg = "Invinsible";
      }else if (powerUp == 4) {
        powerUpMsg = "Health Up";
      }
        sendMQTTMessage("powerup_used", powerUpMsg);
    }
   }

   void displayLives(){
    carrier.display.fillScreen(ST77XX_BLACK);
    carrier.display.setTextColor(ST77XX_WHITE);
    carrier.display.setTextSize(2);
    carrier.display.setCursor(20, 100);
    carrier.display.print("Lives: ");
    carrier.display.print(playerLives);
   }

   void loseLife(){
    if (playerLives > 0) {
    playerLives--;
    Serial.println("Lost a life");
    sendMQTTMessage("life", "Lost a life");
    displayLives();
    if (playerLives == 0) {
      Serial.println("All lives lost");
      sendMQTTMessage("life", "All lives lost");
      carrier.display.fillScreen(ST77XX_RED);
      carrier.display.setCursor(30, 100);
      carrier.display.setTextColor(ST77XX_WHITE);
      carrier.display.setTextSize(3);
      carrier.display.print("GAME OVER");
    }
   }
   } 

  void Gyro(){
    unsigned long currentMillis = millis();
  if (currentMillis - lastGyroUpdate >= gyroInterval) {
    lastGyroUpdate = currentMillis;

if (IMU.accelerationAvailable()) {
    IMU.readAcceleration(x,y,z);  

    String direction = "Idle";
      if (y > tiltThreshold) direction = "Left";
      else if (y < -tiltThreshold) direction = "Right";
      else if (x > tiltThreshold) direction = "Up";
      else if (x < -tiltThreshold) direction = "Down";

      Serial.println("MOVE: " + direction);
      sendMQTTMessage("tilt_move", direction);
    
     
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
  if (carrier.Buttons.onTouchDown(TOUCH1)) {
  loseLife();
}
}
