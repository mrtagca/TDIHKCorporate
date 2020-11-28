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
            //routes.MapRoute(
            //       name: "PageGerman",
            //       url: "seiten/{seolink}",
            //       defaults: new { controller = "Page", action = "Show" },
            //       new[] { "TDIHKCorporate.Controllers" }
            //   );


            ////Sayfalar Türkçe Routing
            //routes.MapRoute(
            //       name: "PageTurkish",
            //       url: "sayfalar/{seoLink}",
            //       defaults: new { controller = "Page", action = "Show", seoLink = UrlParameter.Optional },
            //    new[] { "TDIHKCorporate.Controllers" }
            //   );

            #endregion

            #region Üyelik / Mitgliedschaft
            routes.MapRoute(
                       name: "MemberShipGerman",
                       url: "mitgliedschaft",
                       defaults: new { controller = "MemberShip", action = "Index"},
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                   name: "MemberShipTurkish",
                   url: "uyelik",
                   defaults: new { controller = "MemberShip", action = "Index"},
                new[] { "TDIHKCorporate.Controllers" }
               );
            #endregion

            #region Etkinliklerimiz / Veranstaltungen
            routes.MapRoute(
                       name: "EventsGerman",
                       url: "veranstaltungen",
                       defaults: new { controller = "Event", action = "Index"},
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "EventsTurkish",
                      url: "etkinliklerimiz",
                      defaults: new { controller = "Event", action = "Index" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Yayınlarımız / Veröffentlichungen
            routes.MapRoute(
                       name: "PublicationsGerman",
                       url: "veröffentlichungen",
                       defaults: new { controller = "Publication", action = "Index"},
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "PublicationsTurkish",
                      url: "yayinlarimiz",
                      defaults: new { controller = "Publication", action = "Index"},
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Covid
            routes.MapRoute(
                    name: "covid",
                    url: "covid-19",
                    defaults: new { controller = "Covid", action = "Index", seoLink = UrlParameter.Optional },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "TDIHKCorporate.Controllers" }
            );
        }
    }
}
