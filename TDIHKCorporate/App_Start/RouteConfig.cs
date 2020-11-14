using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TDIHKCorporate
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

            lang = lang.Substring(0, 2);

            routes.MapRoute(
                name: "Default",
                url: "{lang}",
                defaults: new { lang = "de", controller = "Home", action = "Index"}
            );

        }
    }
}
