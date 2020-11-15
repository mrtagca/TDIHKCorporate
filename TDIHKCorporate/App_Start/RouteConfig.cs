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
                name: "Page",
                url: "{seolink}",
                defaults: new { controller = "Page", action = "Show" },
                new[] { "TDIHKCorporate.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                new[] { "TDIHKCorporate.Controllers" }
            );
        }
    }
}
