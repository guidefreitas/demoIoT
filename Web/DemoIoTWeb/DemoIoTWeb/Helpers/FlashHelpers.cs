using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb.Helpers
{
    public static class FlashHelpers
    {
        //A variável TempData (existente em todos os controllers) salva uma informação
        //entre diferentes requisições
        //Obs: A informação é salva apenas para a UMA requisição seguinte
        public static void FlashInfo(this Controller controller, string message)
        {
            controller.TempData["info"] = message;
        }
        public static void FlashWarning(this Controller controller, string message)
        {
            controller.TempData["warning"] = message;
        }
        public static void FlashError(this Controller controller, string message)
        {
            controller.TempData["error"] = message;
        }

        public static HtmlString Flash(this HtmlHelper helper)
        {

            var message = "";
            var className = "";
            if (helper.ViewContext.TempData["info"] != null)
            {
                message = helper.ViewContext.TempData["info"].ToString();
                className = "alert-success";
            }
            else if (helper.ViewContext.TempData["warning"] != null)
            {
                message = helper.ViewContext.TempData["warning"].ToString();
                className = "alert-warning";
            }
            else if (helper.ViewContext.TempData["error"] != null)
            {
                message = helper.ViewContext.TempData["error"].ToString();
                className = "alert-danger";
            }
            var sb = new StringBuilder();
            if (!String.IsNullOrEmpty(message))
            {
                sb.AppendLine(@"<div class='alert " + className + " alert-dismissible' role='alert'>");
                sb.AppendLine(@"<button type = ""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">");
                sb.AppendLine(@"<span aria-hidden=""true"">&times;</span>");
                sb.AppendLine(@"</button>");
                sb.AppendLine(@"<strong>" + HttpUtility.HtmlEncode(message) + "</strong>");
                sb.AppendLine(@"</div>");
            }
            return new HtmlString(sb.ToString());
        }
    }
}