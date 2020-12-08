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
             

            #region Üyelik / Mitgliedschaft

            routes.MapRoute(
                   name: "MemberShipGerman",
                   url: "seiten/mitgliedschaft",
                   defaults: new { controller = "MemberShip", action = "Index" },
                new[] { "TDIHKCorporate.Controllers" }
               );

            routes.MapRoute(
                   name: "MemberShipTurkish",
                   url: "sayfalar/uyelik",
                   defaults: new { controller = "MemberShip", action = "Index" },
                new[] { "TDIHKCorporate.Controllers" }
               );
            #endregion

            #region Etkinliklerimiz / Veranstaltungen
            routes.MapRoute(
                       name: "EventsGerman",
                       url: "seiten/veranstaltungen",
                       defaults: new { controller = "Event", action = "Index" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "EventsTurkish",
                      url: "sayfalar/etkinliklerimiz",
                      defaults: new { controller = "Event", action = "Index" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Yayınlarımız / Veröffentlichungen
            routes.MapRoute(
                       name: "PublicationsGerman",
                       url: "seiten/veröffentlichungen",
                       defaults: new { controller = "Publication", action = "Index" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "PublicationsTurkish",
                      url: "sayfalar/yayinlarimiz",
                      defaults: new { controller = "Publication", action = "Index" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Covid
            routes.MapRoute(
                    name: "covidGerman",
                    url: "seiten/covid-19",
                    defaults: new { controller = "Covid", action = "Index" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                   name: "covidTurkish",
                   url: "sayfalar/covid-19",
                   defaults: new { controller = "Covid", action = "Index" },
                new[] { "TDIHKCorporate.Controllers" }
               );
            #endregion

            #region Hakkımızda / Uber Uns
            routes.MapRoute(
                       name: "UberUnsGerman",
                       url: "seiten/uber-uns",
                       defaults: new { controller = "AboutUs", action = "Index" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "UberUnsTurkish",
                      url: "sayfalar/hakkimizda",
                      defaults: new { controller = "AboutUs", action = "Index" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Toplantı Salonu / Konferenzraum
            routes.MapRoute(
                       name: "KonferenzraumGerman",
                       url: "seiten/konferenzraum",
                       defaults: new { controller = "Services", action = "Konferenzraum" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "KonferenzraumTurkish",
                      url: "sayfalar/toplanti-salonu",
                      defaults: new { controller = "Services", action = "Konferenzraum" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Türkei / Türkiye
            routes.MapRoute(
                       name: "TurkeyGerman",
                       url: "seiten/turkei",
                       defaults: new { controller = "Services", action = "Türkei" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "TurkeyTurkish",
                      url: "sayfalar/turkiye",
                      defaults: new { controller = "Services", action = "Türkei" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Deutschland / Almanya
            routes.MapRoute(
                       name: "DeutschGerman",
                       url: "seiten/deutschland",
                       defaults: new { controller = "Services", action = "Deutschland" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "DeutschTurkish",
                      url: "sayfalar/almanya",
                      defaults: new { controller = "Services", action = "Deutschland" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Musterverträge / Örnek Sözleşmeler
            routes.MapRoute(
                       name: "ContractsGerman",
                       url: "seiten/mustervertrage",
                       defaults: new { controller = "Services", action = "Mustervertrage" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "ContractsTurkish",
                      url: "sayfalar/ornek-sozlesmeler",
                      defaults: new { controller = "Services", action = "Mustervertrage" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Forderung and Finanzierung / Talep ve Finansman
            routes.MapRoute(
                       name: "FinanceGerman",
                       url: "seiten/forderung-and-finanzierung",
                       defaults: new { controller = "Services", action = "ForderungAndFinanzierung" },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "FinanceTurkish",
                      url: "sayfalar/talep-ve-finansman",
                      defaults: new { controller = "Services", action = "ForderungAndFinanzierung" },
                   new[] { "TDIHKCorporate.Controllers" }
                  );


            #endregion

            #region Tobb2b
            routes.MapRoute(
                    name: "tobb2bGerman",
                    url: "seiten/tobb2b",
                    defaults: new { controller = "Services", action = "Tobb2b" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "tobb2bTurkish",
                    url: "sayfalar/tobb2b",
                    defaults: new { controller = "Services", action = "Tobb2b" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Standart Mitgliedschaft / Standart Üyelik
            routes.MapRoute(
                    name: "StandardMitgliedschaftGerman",
                    url: "seiten/standard-mitgliedschaft",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "StandardMitgliedschaftTurkey",
                    url: "sayfalar/standart-uyelik",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Premium Mitgliedschaft / Premium Üyelik
            routes.MapRoute(
                    name: "PremiumMitgliedschaftGerman",
                    url: "seiten/premium-mitgliedschaft",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "PremiumMitgliedschaftTurkey",
                    url: "sayfalar/premium-uyelik",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Jahresberichte / Yıllık Raporlar
            routes.MapRoute(
                    name: "JahresberichteGerman",
                    url: "seiten/jahresberichte",
                    defaults: new { controller = "Publication", action = "Jahresberichte" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "JahresberichteTurkey",
                    url: "sayfalar/yillik-raporlar",
                    defaults: new { controller = "Publication", action = "Jahresberichte" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Infoblätter / Bilgi Sayfaları
            routes.MapRoute(
                    name: "InfoblatterGerman",
                    url: "seiten/infoblatter",
                    defaults: new { controller = "Publication", action = "Infoblatter" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "InfoblatterTurkey",
                    url: "sayfalar/bilgi-sayfalari",
                    defaults: new { controller = "Publication", action = "Infoblatter" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Ein- und Ausreisebestimmungen / Giriş-Çıkış Kuralları
            routes.MapRoute(
                    name: "EinUndAusreisebestimmungenGerman",
                    url: "seiten/ein-und-ausreisebestimmungen",
                    defaults: new { controller = "Covid", action = "EinUndAusreisebestimmungen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "EinUndAusreisebestimmungenTurkey",
                    url: "sayfalar/giris-cikis-kurallari",
                    defaults: new { controller = "Covid", action = "EinUndAusreisebestimmungen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Mabnahmen / Aktiviteler
            routes.MapRoute(
                    name: "MabnahmenGerman",
                    url: "seiten/mabnahmen",
                    defaults: new { controller = "Covid", action = "Mabnahmen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "MabnahmenTurkey",
                    url: "sayfalar/aktiviteler",
                    defaults: new { controller = "Covid", action = "Mabnahmen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Wichtige Informationsquellen / Önemli Bilgi Kaynakları
            routes.MapRoute(
                    name: "WichtigeInformationsquellenGerman",
                    url: "seiten/wichtige-informationsquellen",
                    defaults: new { controller = "Covid", action = "WichtigeInformationsquellen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "WichtigeInformationsquellenTurkey",
                    url: "sayfalar/onemli-bilgi-kaynaklari",
                    defaults: new { controller = "Covid", action = "WichtigeInformationsquellen" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Kalender / Etkinlik Takvimi
            routes.MapRoute(
                    name: "KalenderGerman",
                    url: "seiten/kalender",
                    defaults: new { controller = "Event", action = "Kalender" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "KalenderTurkey",
                    url: "sayfalar/etkinlik-takvimi",
                    defaults: new { controller = "Event", action = "Kalender" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Markteintritt / Marketing
            routes.MapRoute(
                    name: "MarkteintrittGerman",
                    url: "seiten/markteintritt",
                    defaults: new { controller = "Services", action = "Markteintritt" },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "MarkteintrittTurkey",
                    url: "sayfalar/marketing",
                    defaults: new { controller = "Services", action = "Markteintritt" },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

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
                   url: "sayfalar/{seoLink}",
                   defaults: new { controller = "Page", action = "Show", seoLink = UrlParameter.Optional },
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
