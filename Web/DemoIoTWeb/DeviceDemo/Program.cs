using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DeviceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aguardando...");
            Thread.Sleep(10000);
            Console.WriteLine("Iniciando cliente");
            MqttClient client = new MqttClient("localhost");
            var clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, "admin", "admin");
            string[] topic = { "sensor/light" };
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
            client.Subscribe(topic, qosLevels);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        }

        private static void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            if (e.Topic == "sensor/light")
            {
                Console.WriteLine("Sensor: {0}", System.Text.Encoding.Default.GetString(e.Message));
            }
        }
    }
}
