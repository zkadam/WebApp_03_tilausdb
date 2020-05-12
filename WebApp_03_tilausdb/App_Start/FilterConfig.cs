using System.Web;
using System.Web.Mvc;

namespace WebApp_03_tilausdb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
