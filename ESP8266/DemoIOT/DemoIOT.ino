#include "arduino.h"
#include <OneWire.h>
#include <ESP8266WiFi.h>
#include <PubSubClient.h>
#include <String.h>

#define PIN_LDR A0
#define PIN_RED 15
#define PIN_GREEN 12
#define PIN_BLUE 13
#define BUTTON 4
#define WLAN_SSID       "FUNCIONARIOS - 2.4Ghz"
#define WLAN_PASS       "corp@sociesc"
#define MQTT_SERVER     "10.8.109.22"
#define MQTT_PORT       1883
#define MQTT_USER       "admin"
#define MQTT_PASS       "admin"
#define MQTT_CLIENTID   "2d720ef0-6bae-11e6-bdf4-0800200c9a66"

int DEVICE_SERIAL_NUMBER = 1;

void setRgbLed(int r, int g, int b);
char RRR[3];
char GGG[3];
char BBB[3];
    
float lightValue = 0;
long lastMsg = 0;

WiFiClient wifi;
PubSubClient MQTT(wifi);
/*
 * O payload deve ser no formato
 * RRR|GGG|BBB (Ex: 255|000|000)
 */
void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("MQTT message received: ");
  Serial.println(topic);
  if(strcmp(topic, "sensor/rgb") == 0){
    Serial.print("Length: ");
    Serial.println(length);
    Serial.print("Payload: ");
    Serial.println((const char*) payload);
    char* payloadData = (char*)malloc(length);
    memcpy(payloadData, payload, length);
    Serial.print("PayloadData: ");
    Serial.println(payloadData);
    
    RRR[0] = payloadData[0];
    RRR[1] = payloadData[1];
    RRR[2] = payloadData[2];
    GGG[0] = payloadData[4];
    GGG[1] = payloadData[5];
    GGG[2] = payloadData[6];
    BBB[0] = payloadData[8];
    BBB[1] = payloadData[9];
    BBB[2] = payloadData[10];    
    
    int R = atoi(RRR);
    int G = atoi(GGG);
    int B = atoi(BBB);

    setRgbLed(R,G,B);
    
    free(payloadData);
  }
  
  
  
}

void wifiConnect(){
  Serial.print(F("Connecting to "));
  Serial.println(WLAN_SSID);
  WiFi.begin(WLAN_SSID, WLAN_PASS);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(F("."));
  }
  Serial.println(F("WiFi connected"));
  Serial.println(F("IP address: "));
  Serial.println(WiFi.localIP());
}

void MqttConnect(){
  MQTT.setServer(MQTT_SERVER, MQTT_PORT);
  MQTT.setCallback(callback);
  if(MQTT.connect(MQTT_CLIENTID ,MQTT_USER, MQTT_PASS)){
    Serial.println("MQTT connected");
    if(MQTT.subscribe("sensor/rgb")){
      Serial.println("MQTT subscribed to sensor/rgb");
    }else{
      Serial.println("MQTT subscription to sensor/rgb failed");
    }
  }else{
    Serial.println("MQTT connection failed");
  }
  
}

void setRgbLed(int r, int g, int b){
  analogWrite(PIN_RED, r);
  analogWrite(PIN_GREEN, g);
  analogWrite(PIN_BLUE, b); 
}

void setup() {
  Serial.begin(9600);
  while(!Serial){;}
  pinMode(PIN_BLUE, OUTPUT);
  pinMode(PIN_RED, OUTPUT);
  pinMode(PIN_GREEN, OUTPUT);
  Serial.println("Starting...");
  wifiConnect();
  MqttConnect();
  delay(500);
}

void sendData(int lightValue){

  String data = "{ 'DeviceSerialNumber' : '";
  data += DEVICE_SERIAL_NUMBER;
  data += "' , 'Value' : '";
  data += lightValue;
  data += "' }";
  Serial.print("Sending: ");
  Serial.println(data.c_str());
  MQTT.publish("sensor/light", data.c_str());
}

void loop() {
  if(!MQTT.connected()){
    MqttConnect();
    delay(10000);
  }
  
  MQTT.loop();
  long now = millis();
  if (now - lastMsg > 10000) {
    lastMsg = now;
    lightValue = analogRead(PIN_LDR);
    Serial.print("Sensor: ");
    Serial.println(lightValue);
    sendData(lightValue);
  }
}
