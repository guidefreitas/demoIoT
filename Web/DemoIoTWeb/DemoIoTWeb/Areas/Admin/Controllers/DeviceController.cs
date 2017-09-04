using DemoIoTWeb.Filters;
using DemoIoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DemoIoTWeb.Areas.Admin.ViewModel;
using DemoIoTWeb.Exceptions;
using DemoIoTWeb.Helpers;

namespace DemoIoTWeb.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class DeviceController : Controller
    {
        DemoIoTContext db = new DemoIoTContext();

        public ActionResult Index()
        {
            var devices = db.Devices.Include(m => m.User).ToList();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Detail(long id)
        {
            var device = db.Devices.Where(m => m.Id == id).Include(m => m.Updates).FirstOrDefault();
            if(device == null)
            {
                this.FlashError("Device not found");
                return RedirectToAction("Index");
            }

            return View(device);
        }

        [HttpGet]
        public ActionResult Create()
        {
            VMDevice vm = new VMDevice();
            vm.Users = db.Users.ToList();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(VMDevice vm)
        {
            try
            {

                if (String.IsNullOrWhiteSpace(vm.SerialNumber))
                    throw new ValidationException("SerialNumber", "Invalid serial number");

                if (db.Devices.Where(m => m.SerialNumber.Equals(vm.SerialNumber)).Count() > 0)
                    throw new ValidationException("SerialNumber", "Serial number already taken");

                var selectedUser = db.Users.Where(m => m.Id == vm.SelectedUserId).FirstOrDefault();
                if (selectedUser == null)
                    throw new ValidationException("User", "User not found");

                Device device = new Device();
                device.SerialNumber = vm.SerialNumber;
                device.User = selectedUser;
                device.Description = vm.Description;
                db.Devices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
            }

            vm.Users = db.Users.ToList();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var loggedUser = this.ViewBag.User as User;

            var device = db.Devices.Where(m => m.Id == id).FirstOrDefault();
            if(device == null)
            {
                this.FlashError("Device not found");
                return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
            }

            if(!loggedUser.isAdmin && device.User.Id != loggedUser.Id)
            {
                this.FlashError("You can't edit this device");
                return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
            }

            VMDevice vm = new VMDevice();
            vm.Users = db.Users.ToList();
            vm.Id = device.Id;
            vm.SerialNumber = device.SerialNumber;
            vm.SelectedUserId = device.User.Id;
            vm.Description = device.Description;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(long id, VMDevice vm)
        {
            try
            {
                var loggedUser = this.ViewBag.User as User;

                var device = db.Devices.Where(m => m.Id == id).FirstOrDefault();
                if (device == null)
                {
                    this.FlashError("Device not found");
                    return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
                }

                if (!loggedUser.isAdmin && device.User.Id != loggedUser.Id)
                {
                    this.FlashError("You can't edit this device");
                    return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
                }

                if (String.IsNullOrWhiteSpace(vm.SerialNumber))
                    throw new ValidationException("SerialNumber", "Invalid serial number");

                var selectedUser = db.Users.Where(m => m.Id == vm.SelectedUserId).FirstOrDefault();
                if (selectedUser == null)
                    throw new ValidationException("User", "User not found");

                device.SerialNumber = vm.SerialNumber;
                device.User = selectedUser;
                device.Description = vm.Description;
                db.SaveChanges();
                this.FlashInfo("Device updated");
                return RedirectToAction("Index", new { area = "Admin", controller = "Device" });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
            }

            vm.Users = db.Users.ToList();
            return View(vm);
        }
    }
}