using DemoIoTWeb.Models;
using DemoIoTWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Controllers
{
    public class DeviceController : Controller
    {
        private DemoIoTContext db = new DemoIoTContext();
        public ActionResult Detail(long id)
        {
            var device = db.Devices.Where(m => m.Id == id)
                                   .Include(m => m.Updates)
                                   .FirstOrDefault();

            String chartLabels = "[";
            String chartData = "[";
            
            for(int i=0;i<device.Updates.Count;i++)
            {
                var update = device.Updates.ElementAt(i);
                chartData += update.Value;
                chartLabels += "\"" + update.DateTime.ToString("dd/MM/yyyy HH:mm:ss") + "\"";
                if (i != device.Updates.Count - 1)
                {
                    chartData += ",";
                    chartLabels += ",";
                }
                    
            }
            chartData += "]";
            chartLabels += "]";

            ViewBag.ChartData = chartData;
            ViewBag.ChartLabels = chartLabels;

            return View(device);
        }

        [HttpGet]
        public ActionResult Led()
        {
            DeviceLedVM vm = new DeviceLedVM();
            vm.Green = "000";
            vm.Red = "000";
            vm.Blue = "000";
            return View(vm);
        }

        [HttpPost]
        public ActionResult Led(DeviceLedVM vm)
        {
            /*
            MQTTConnection conn = new MQTTConnection(MQTTServer.MQTT_HOST, MQTTServer.MQTT_PORT, MQTTServer.MQTT_USER, MQTTServer.MQTT_PASS);
            conn.Connect();
            var msg = vm.Red + "|" + vm.Green + "|" + vm.Blue;
            conn.Publish("sensor/rgb", msg);
            */
            return View(vm);
        }
    }
}