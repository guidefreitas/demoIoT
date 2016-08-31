using DemoIoTWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Areas.Api.Controllers
{
    public class DeviceController : Controller
    {
        private DemoIoTContext db = new DemoIoTContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDevice(long id)
        {
            try
            {
                var device = db.Devices
                               .Where(m => m.Id == id)
                               .Include(m => m.Updates)
                               .FirstOrDefault();

                if(device == null)
                {
                    return Json(new
                    {
                        status = "ERROR",
                        message = "Device not found"
                    }, JsonRequestBehavior.AllowGet);
                }

                var dataDevice = new
                {
                    Id = device.Id,
                    SerialNumber = device.SerialNumber,
                    Description = device.Description,
                    Updates = new List<Object>()
                };

                foreach(var update in device.Updates)
                {
                    dataDevice.Updates.Add(new
                    {
                        Id = update.Id,
                        DeviceId = device.Id,
                        Value = update.Value,
                        DateTime = update.DateTime.ToString("dd/MM/yyyy HH:mm:ss")
                    });
                }

                return Json(dataDevice, JsonRequestBehavior.AllowGet);

            }catch(Exception ex)
            {
                return Json(new
                {
                    status = "ERROR",
                    message = "Error: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                var devices = db.Devices
                                .OrderBy(m => m.Id)
                                .ToList();

                var jsonDevices = new List<Object>();
                foreach(var device in devices)
                {
                    var jsonDevice = new
                    {
                        Id = device.Id,
                        Description = device.Description,
                        SerialNumber = device.SerialNumber,
                        Updates = new List<Object>()
                    };

                    foreach(var update in device.Updates)
                    {
                        var jsonUpdate = new
                        {
                            Id = update.Id,
                            Value = update.Value,
                            DateTime = update.DateTime.ToString("dd/MM/yyyy HH:mm:ss")
                        };

                        jsonDevice.Updates.Add(jsonUpdate);
                    }

                    jsonDevices.Add(jsonDevice);

                }

                return Json(jsonDevices, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = "ERROR",
                    message = "Error: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CreateUpdate()
        {
            try
            {
                String jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                Newtonsoft.Json.Linq.JObject token = JObject.Parse(jsonData);
                var deviceSerialNumber = (string)token.SelectToken("DeviceSerialNumber");
                var value = (string)token.SelectToken("Value");

                var device = db.Devices
                               .Where(m => m.SerialNumber == deviceSerialNumber)
                               .FirstOrDefault();

                if(device == null)
                {
                    return Json(new
                    {
                        status = "ERROR",
                        message = "Device not found"
                    });
                }

                var update = new DeviceUpdate();
                update.Value = value;
                update.DateTime = DateTime.Now;
                device.Updates.Add(update);
                db.SaveChanges();

                return Json(
                    new
                    {
                        status = "OK",
                        message = "Device update created"
                    });

            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = "ERROR",
                    message = "Error: " + ex.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Create()
        {
            try
            {
                String jsonData = new StreamReader(Request.InputStream).ReadToEnd();
                Newtonsoft.Json.Linq.JObject token = JObject.Parse(jsonData);
                var deviceSerialNumber = (string)token.SelectToken("SerialNumber");
                var deviceDescription = (string)token.SelectToken("Description");
                Device device = new Device();
                device.SerialNumber = deviceSerialNumber;
                device.Description = deviceDescription;
                db.Devices.Add(device);
                db.SaveChanges();
                return Json(
                    new
                    {
                        status = "OK",
                        message = "Device created"
                    });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = "ERROR",
                    message = "Error: " + ex.Message
                });
            }
        }
    }
}