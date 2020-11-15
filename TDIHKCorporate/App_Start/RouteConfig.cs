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



            #region Sayfalar

            //Sayfalar Almanca Routing
            routes.MapRoute(
                   name: "PageGerman",
                   url: "seiten/{seolink}",
                   defaults: new { controller = "Page", action = "Show" },
                   new[] { "TDIHKCorporate.Controllers" }
               );


            //Sayfalar Türkçe Routing
            routes.MapRoute(
                   name: "PageTurkish",
                   url: "sayfalar/{seolink}",
                   defaults: new { controller = "Page", action = "Show" },
                   new[] { "TDIHKCorporate.Controllers" }
               ); 

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                new[] { "TDIHKCorporate.Controllers" }
            );
        }
    }
}
