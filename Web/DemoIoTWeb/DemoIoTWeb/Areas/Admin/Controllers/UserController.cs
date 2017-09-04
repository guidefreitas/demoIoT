using DemoIoTWeb.Areas.Admin.ViewModel;
using DemoIoTWeb.Exceptions;
using DemoIoTWeb.Filters;
using DemoIoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DemoIoTWeb.Helpers;

namespace DemoIoTWeb.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class UserController : Controller
    {
        DemoIoTContext db = new DemoIoTContext();
        public ActionResult Index()
        {
            var users = db.Users.OrderBy(m => m.Email).ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if(!(this.ViewBag.User as User).isAdmin)
            {
                this.FlashError("You are not an administrator");
                return RedirectToAction("Index");
            }

            VMUser vm = new VMUser();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(VMUser vm)
        {
            try
            {
                if (!vm.Password.Equals(vm.PasswordConfirmation))
                    throw new ValidationException("Password", "Password and confimation must be the same");

                if (db.Users.Where(m => m.Email == vm.Email).Count() > 0)
                    throw new ValidationException("Email", "Email already taken");

                User user = new User();
                user.Email = vm.Email;
                user.Password = Crypto.HashPassword(vm.Password);
                db.Users.Add(user);
                db.SaveChanges();
                this.FlashInfo("User created!");
                return RedirectToAction("Index");

            }catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
            }
            vm.Password = "";
            vm.PasswordConfirmation = "";
            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var loggedUser = this.ViewBag.User as User;
            if(!loggedUser.isAdmin || loggedUser.Id != id)
            {
                this.FlashError("You can only edit your own user");
                return RedirectToAction("Index");
            }

            var user = db.Users.Where(m => m.Id == id).FirstOrDefault();
            if (user == null)
            {
                this.FlashError("User not found");
                return RedirectToAction("Index");
            }

            VMUser vm = new VMUser();
            vm.Email = user.Email;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(VMUser vm)
        {
            var loggedUser = this.ViewBag.User as User;
            if (!loggedUser.isAdmin || loggedUser.Id != vm.Id)
            {
                this.FlashError("You can only edit your own user");
                return RedirectToAction("Index");
            }

            try
            {
                if (!vm.Password.Equals(vm.PasswordConfirmation))
                    throw new ValidationException("Password", "Password and confimation must be the same");

                User user = db.Users.Where(m => m.Id == vm.Id).FirstOrDefault();

            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
            }
            return View(vm);
        }
    }
}