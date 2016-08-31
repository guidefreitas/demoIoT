using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Charlotte;
using DemoIoTWeb.Models;

namespace DemoIoTWeb.Controllers
{
    public class HomeController : Controller
    {
        private DemoIoTContext db = new DemoIoTContext();

        public ActionResult Index()
        {
           

            var devices = db.Devices.OrderBy(m => m.Id).ToList();

            return View(devices);
        }
    }
}