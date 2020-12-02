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
                    defaults: new { controller = "Covid", action = "Index"},
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Hakkımızda / Uber Uns
            routes.MapRoute(
                       name: "UberUnsGerman",
                       url: "uber-uns",
                       defaults: new { controller = "AboutUs", action = "Index" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "UberUnsTurkish",
                      url: "hakkimizda",
                      defaults: new { controller = "AboutUs", action = "Index" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Toplantı Salonu / Konferenzraum
            routes.MapRoute(
                       name: "KonferenzraumGerman",
                       url: "konferenzraum",
                       defaults: new { controller = "Services", action = "Konferenzraum" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "KonferenzraumTurkish",
                      url: "toplanti-salonu",
                      defaults: new { controller = "Services", action = "Konferenzraum" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Türkei / Türkiye
            routes.MapRoute(
                       name: "TurkeyGerman",
                       url: "turkei",
                       defaults: new { controller = "Services", action = "Türkei" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "TurkeyTurkish",
                      url: "turkiye",
                      defaults: new { controller = "Services", action = "Türkei" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Deutschland / Almanya
            routes.MapRoute(
                       name: "DeutschGerman",
                       url: "deutschland",
                       defaults: new { controller = "Services", action = "Deutschland" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "DeutschTurkish",
                      url: "almanya",
                      defaults: new { controller = "Services", action = "Deutschland" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Musterverträge / Örnek Sözleşmeler
            routes.MapRoute(
                       name: "ContractsGerman",
                       url: "mustervertrage",
                       defaults: new { controller = "Services", action = "Mustervertrage" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "ContractsTurkish",
                      url: "ornek-sozlesmeler",
                      defaults: new { controller = "Services", action = "Mustervertrage" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Forderung and Finanzierung / Talep ve Finansman
            routes.MapRoute(
                       name: "FinanceGerman",
                       url: "forderung-and-finanzierung",
                       defaults: new { controller = "Services", action = "ForderungAndFinanzierung" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "FinanceTurkish",
                      url: "talep-ve-finansman",
                      defaults: new { controller = "Services", action = "ForderungAndFinanzierung" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Tobb2b
            routes.MapRoute(
                    name: "tobb2b",
                    url: "tobb2b",
                    defaults: new { controller = "Services", action = "Tobb2b"},
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Standart Mitgliedschaft / Standart Üyelik
            routes.MapRoute(
                    name: "StandardMitgliedschaftGerman",
                    url: "standard-mitgliedschaft",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft"},
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "StandardMitgliedschaftTurkey",
                    url: "standart-uyelik",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Premium Mitgliedschaft / Premium Üyelik
            routes.MapRoute(
                    name: "PremiumMitgliedschaftGerman",
                    url: "premium-mitgliedschaft",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft"},
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "PremiumMitgliedschaftTurkey",
                    url: "premium-uyelik",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft"},
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
