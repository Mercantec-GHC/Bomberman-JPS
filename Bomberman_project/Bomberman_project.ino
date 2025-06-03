#include <Arduino_MKRIoTCarrier.h>
#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>
#include <PubSubClient.h>
#include <ArduinoHttpClient.h>
#include <ArduinoJson.h>
#include "config.h"
#include "CommonClasses.h"


const int playerId = 1; // Change this to 1, 2, 3, or 4 for each arduino

const char ssid[] = WIFI_SSID;
const char password[] = WIFI_PASSWORD;
const char mqtt_server[] = MQTT_SERVER;
const int mqtt_port = 8883;
const char mqtt_username[] = MQTT_USERNAME;
const char mqtt_password[] = MQTT_PASSWORD;
const char mqttTopic[] = "game/status";
const char baseAPI[] = BASE_API_URI;

WiFiClient wifiClient;
WiFiSSLClient wifiSSlClient;
PubSubClient client(wifiSSlClient);
MKRIoTCarrier carrier;
HttpClient http = HttpClient(wifiClient, baseAPI, 8080);
JsonDocument doc;

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
  http.get("/api/Carrier/controller?id=12");
  
  if(http.responseStatusCode() == 200)
  {
    Serial.println("Controller already exists");
  }
  else 
  {
    InitialCreationOfController();
  }

  

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
  if (WiFi.status() != WL_CONNECTED) {
    WiFi.begin(ssid, password);
    delay(2000);
    return;
  }

  // Create a unique client ID using the player ID
  String clientId = "ArduinoMKR_" + String(playerId);

  Serial.print("Connecting with client ID: ");
  Serial.println(clientId);

  if (client.connect(clientId.c_str(), mqtt_username, mqtt_password)) {
    Serial.println("Connected to MQTT broker");

    static bool alreadySubscribed = false;
    if (!alreadySubscribed) {
      client.subscribe(mqttTopic);
      alreadySubscribed = true;
    }
  } else {
    Serial.print("Failed to connect, status code: ");
    Serial.println(client.state());
  }
}

void InitialCreationOfController(){

    doc["id"] = 12;
    doc["playerColor"] = "red";
    doc["playerId"] = "2c8452ac-4330-47a9-8cc0-23abb46a154e";
    doc["ledBrightness"] = 0.8;
    doc["gyroScopeId"] = 12;
    doc["buttonsId"] = 12;

    JsonObject gyroscope = doc["gyroscope"].to<JsonObject>();
    gyroscope["id"] = 12;
    gyroscope["xCordinate"] = 2.13;
    gyroscope["yCordinate"] = 4.27;
    gyroscope["zCordinate"] = 1.82;

    JsonObject buttons = doc["buttons"].to<JsonObject>();
    buttons["id"] = 12;
    buttons["name"] = "PowerUp";
    String output;
    serializeJson(doc, output);

    Serial.println(output);
    http.beginRequest();
    http.post("/api/Carrier/controller");
    http.sendHeader("Content-Type", "application/json");
    http.sendHeader("Content-Length", output.length());
    http.beginBody();
    http.print(output);
    http.endRequest();

    int statusCode = http.responseStatusCode();
    String response = http.responseBody();

    Serial.print("Status code: ");
    Serial.println(statusCode);
    Serial.print("Response: ");
    Serial.println(response);
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


String getPlayerColor(){
  String response;
  http.get("/api/Carrier/controller?id=12");
  if(http.responseStatusCode() == 200)
  {
    
   response =  http.responseBody();
   DeserializationError error = deserializeJson(doc, response);
   JsonObject result = doc["result"];
   String result_playerColor = result["playerColor"];
   response = result_playerColor;

    return response;
  }
  return response;
}

int getPlayerColorBrightness(){
  String response;
  int ledBrightness = 0;
  http.get("/api/Carrier/controller?id=12");
  if(http.responseStatusCode() == 200)
  {
    
   response =  http.responseBody();
   DeserializationError error = deserializeJson(doc, response);
   JsonObject result = doc["result"];
   String result_ledBrightness = result["ledBrightness"];
   ledBrightness = result_ledBrightness.toInt();
   Serial.println(ledBrightness);
    return ledBrightness;
  }
  return ledBrightness;
}
void Screen(){

  String path = "/api/Player?id=2c8452ac-4330-47a9-8cc0-23abb46a154e";
  http.get(path);

  int statusCode = http.responseStatusCode();
  String response = http.responseBody();

  StaticJsonDocument<512> doc;
  DeserializationError error = deserializeJson(doc, response);

carrier.display.setTextColor(0xFFFF);
  carrier.display.setTextSize(3);
  carrier.display.setCursor(10, 100);

   const char* userName = doc["userName"];
  carrier.display.print("Player:");
  carrier.display.print(userName);

  carrier.display.setTextColor(0xFFFF);
  carrier.display.setTextSize(3);
  carrier.display.setCursor(10, 130);

   String lives = doc["lives"];
   carrier.display.print("Lives:");
  carrier.display.print(lives);

}

void loop() {
  // Reconnect WiFi if dropped
if (WiFi.status() != WL_CONNECTED) {
  Serial.println("WiFi connection lost. Attempting to reconnect...");
  WiFi.begin(ssid, password);
  delay(2000); // wait before next try
  return; // skip rest of loop if WiFi is not available
}
  
  String path = "/api/Player?id=2c8452ac-4330-47a9-8cc0-23abb46a154e";
  http.get(path);



  int statusCode = http.responseStatusCode();
  String response = http.responseBody();

  StaticJsonDocument<512> doc;
  DeserializationError error = deserializeJson(doc, response);  


  Serial.print("Status code: ");
  Serial.println(statusCode);
  Serial.print("Response: ");
  Serial.println(response);

  if (!client.connected()) {
  static unsigned long lastReconnectAttempt = 0;
  unsigned long now = millis();

  Serial.println("Printing the color out: " + getPlayerColor());
  Serial.println(getPlayerColorBrightness());



   if(getPlayerColor() == "Red" || getPlayerColor() == "red")
    {
      carrier.leds.setBrightness(getPlayerColorBrightness());
      uint32_t color = carrier.leds.Color(255, 0, 0);
      carrier.leds.fill(color, 0, 5);
      carrier.leds.show();
    }
    else if(getPlayerColor() == "Blue" || getPlayerColor() == "blue")
    {
      carrier.leds.setBrightness(getPlayerColorBrightness());
      uint32_t color = carrier.leds.Color(0, 0, 255);
      carrier.leds.fill(color, 0, 5);
      carrier.leds.show();
    }
    else if(getPlayerColor() == "Green" || getPlayerColor() == "green")
    {
      carrier.leds.setBrightness(getPlayerColorBrightness());
      uint32_t color = carrier.leds.Color(0, 255, 0);
      carrier.leds.fill(color, 0, 5);
      carrier.leds.show();
    }

  if (now - lastReconnectAttempt > 5000) {
    lastReconnectAttempt = now;
    reconnect();
  }
}else{
  client.loop();
}
  
  carrier.Buttons.update();
  Screen();
  Gyro();
  PlaceBomb();   
  UsePowerUp();


}
