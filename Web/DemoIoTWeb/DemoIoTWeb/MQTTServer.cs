using Charlotte;
using DemoIoTWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Communication;
using uPLibrary.Networking.M2Mqtt.Managers;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DemoIoTWeb
{
    public class SubscriberModule : MqttModule
    {
        

        public SubscriberModule() : base(MQTTServer.MQTT_HOST, MQTTServer.MQTT_PORT, MQTTServer.MQTT_USER, MQTTServer.MQTT_PASS)
        {
            DemoIoTContext db = new DemoIoTContext();
            On[MQTTServer.MQTT_TOPIC_LIGHT] = _ =>
            {
                var message =  (String) _.Message;
                var json = JObject.Parse(message);
                var deviceSerialNumber = (string)json.SelectToken("DeviceSerialNumber");
                var value = (string)json.SelectToken("Value");

                var device = db.Devices
                               .Where(m => m.SerialNumber == deviceSerialNumber)
                               .FirstOrDefault();

                if (device == null)
                    return;

                var update = new DeviceUpdate();
                update.Value = value;
                update.DateTime = DateTime.Now;
                device.Updates.Add(update);
                db.SaveChanges();
            };
        }
    }

    public class MQTTServer
    {
        public static String MQTT_HOST = "localhost";
        public static int MQTT_PORT = 1883;
        public static String MQTT_USER = "admin";
        public static String MQTT_PASS = "admin";
        public static String MQTT_TOPIC_RGB = "sensor/rgb";
        public static String MQTT_TOPIC_LIGHT = "sensor/light";

        private MqttBroker broker;
        private Charlotte.Mqtt client;
        private DemoIoTContext db;
        public MQTTServer()
        {
            broker = new MqttBroker();
            MqttUacManager manager = new MqttUacManager();
            manager.UserAuthentication(MQTT_USER, MQTT_PASS);
            broker.UserAuth = manager.UserAuth;
            db = new DemoIoTContext();
        }

        public void Start()
        {
            broker.Start();
            SubscriberModule subsModule = new SubscriberModule();
            subsModule.Run();
            
            
        }
    }
}