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

            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            language = language.Substring(0, 2);


            routes.MapRoute(
                name: "Page",
                url: "pages/{seolink}",
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
