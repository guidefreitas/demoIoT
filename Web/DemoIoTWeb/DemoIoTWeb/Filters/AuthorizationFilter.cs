using DemoIoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var request = filterContext.RequestContext.HttpContext.Request;
                var session = filterContext.RequestContext.HttpContext.Session;

                //Encontra um cookie com a identificação AuthID, se não encontrar
                //será disparada uma excessão, caindo no catch.
                var cookieAuthId = request.Cookies["AuthID"].Value;

                //Verfica se existe algum usuário no banco com o mesmo token de identificação do cookie
                DemoIoTContext db = new DemoIoTContext();
                var usuarioDb = db.Users.Where(m => m.AuthId == cookieAuthId).FirstOrDefault();

                //Se não existir nenhum usuário com esse token, ou seja, o usuarioDB virá nulo, redireciona
                //para a página de login
                if (usuarioDb == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "", controller = "Authentication", action = "Login" }));
                }
            }
            catch
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "", controller = "Authentication", action = "Login" }));
            }
        }
    }
}