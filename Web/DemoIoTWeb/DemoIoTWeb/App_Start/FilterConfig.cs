using DemoIoTWeb.Filters;
using System.Web;
using System.Web.Mvc;

namespace DemoIoTWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserInfoFilter());
        }
    }
}
