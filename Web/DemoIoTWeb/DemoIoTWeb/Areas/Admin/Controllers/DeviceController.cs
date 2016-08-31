using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Areas.Admin.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Admin/Device
        public ActionResult Index()
        {
            return View();
        }
    }
}