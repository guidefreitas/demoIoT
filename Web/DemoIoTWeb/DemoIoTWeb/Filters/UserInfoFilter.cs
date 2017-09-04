using DemoIoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Filters
{
    public class UserInfoFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var session = filterContext.RequestContext.HttpContext.Session;

            //Verifica se existe algum cookie com a identificação AuthID
            if (request.Cookies.AllKeys.Contains("AuthID"))
            {
                var cookieAuthId = request.Cookies["AuthID"].Value;

                //Busca o usuário no banco com o token de autenticação existente no cookie
                DemoIoTContext db = new DemoIoTContext();
                var usuarioDb = db.Users.Where(m => m.AuthId == cookieAuthId).FirstOrDefault();

                //Se o usuário existir
                if (usuarioDb != null && usuarioDb.AuthId == cookieAuthId)
                {
                    //Guarta o usuario em uma variável acessível pelos controllers
                    filterContext.Controller.ViewBag.User = usuarioDb;
                }
            }
        }
    }
}