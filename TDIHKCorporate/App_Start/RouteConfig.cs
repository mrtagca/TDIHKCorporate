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


            //#region Üyelik / Mitgliedschaft

            //routes.MapRoute(
            //           name: "MemberShipGerman",
            //           url: "{lang}/seiten/mitgliedschaft",
            //           defaults: new { controller = "MemberShip", action = "Index", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //       name: "MemberShipTurkish",
            //       url: "{lang}/sayfalar/uyelik",
            //       defaults: new { controller = "MemberShip", action = "Index", lang = language },
            //    new[] { "TDIHKCorporate.Controllers" }
            //   );

            //#endregion

            //#region Etkinliklerimiz / Veranstaltungen
            //routes.MapRoute(
            //           name: "EventsGerman",
            //           url: "{lang}/seiten/veranstaltungen",
            //           defaults: new { controller = "Event", action = "Index", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "EventsTurkish",
            //          url: "{lang}/sayfalar/etkinliklerimiz",
            //          defaults: new { controller = "Event", action = "Index", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Yayınlarımız / Veröffentlichungen
            //routes.MapRoute(
            //           name: "PublicationsGerman",
            //           url: "{lang}/seiten/veröffentlichungen",
            //           defaults: new { controller = "Publication", action = "Index", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "PublicationsTurkish",
            //          url: "{lang}/sayfalar/yayinlarimiz",
            //          defaults: new { controller = "Publication", action = "Index", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Covid
            //routes.MapRoute(
            //        name: "covidGerman",
            //        url: "{lang}/seiten/covid-19",
            //        defaults: new { controller = "Covid", action = "Index", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //       name: "covidTurkish",
            //       url: "{lang}/sayfalar/covid-19",
            //       defaults: new { controller = "Covid", action = "Index", lang = language },
            //    new[] { "TDIHKCorporate.Controllers" }
            //   );
            //#endregion

            //#region Hakkımızda / Uber Uns
            //routes.MapRoute(
            //           name: "UberUnsGerman",
            //           url: "{lang}/seiten/uber-uns",
            //           defaults: new { controller = "AboutUs", action = "Index", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "UberUnsTurkish",
            //          url: "{lang}/sayfalar/hakkimizda",
            //          defaults: new { controller = "AboutUs", action = "Index", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Toplantı Salonu / Konferenzraum
            //routes.MapRoute(
            //           name: "KonferenzraumGerman",
            //           url: "{lang}/seiten/konferenzraum",
            //           defaults: new { controller = "Services", action = "Konferenzraum", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "KonferenzraumTurkish",
            //          url: "{lang}/sayfalar/toplanti-salonu",
            //          defaults: new { controller = "Services", action = "Konferenzraum", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Türkei / Türkiye
            //routes.MapRoute(
            //           name: "TurkeyGerman",
            //           url: "{lang}/seiten/turkei",
            //           defaults: new { controller = "Services", action = "Türkei", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "TurkeyTurkish",
            //          url: "{lang}/sayfalar/turkiye",
            //          defaults: new { controller = "Services", action = "Türkei", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Deutschland / Almanya
            //routes.MapRoute(
            //           name: "DeutschGerman",
            //           url: "{lang}/seiten/deutschland",
            //           defaults: new { controller = "Services", action = "Deutschland", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "DeutschTurkish",
            //          url: "{lang}/sayfalar/almanya",
            //          defaults: new { controller = "Services", action = "Deutschland", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Musterverträge / Örnek Sözleşmeler
            //routes.MapRoute(
            //           name: "ContractsGerman",
            //           url: "{lang}/seiten/mustervertrage",
            //           defaults: new { controller = "Services", action = "Mustervertrage", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "ContractsTurkish",
            //          url: "{lang}/sayfalar/ornek-sozlesmeler",
            //          defaults: new { controller = "Services", action = "Mustervertrage", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Forderung and Finanzierung / Talep ve Finansman
            //routes.MapRoute(
            //           name: "FinanceGerman",
            //           url: "{lang}/seiten/forderung-and-finanzierung",
            //           defaults: new { controller = "Services", action = "ForderungAndFinanzierung", lang = language },
            //        new[] { "TDIHKCorporate.Controllers" }
            //       );

            //routes.MapRoute(
            //          name: "FinanceTurkish",
            //          url: "{lang}/sayfalar/talep-ve-finansman",
            //          defaults: new { controller = "Services", action = "ForderungAndFinanzierung", lang = language },
            //       new[] { "TDIHKCorporate.Controllers" }
            //      );


            //#endregion

            //#region Tobb2b
            //routes.MapRoute(
            //        name: "tobb2bGerman",
            //        url: "{lang}/seiten/tobb2b",
            //        defaults: new { controller = "Services", action = "Tobb2b", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "tobb2bTurkish",
            //        url: "{lang}/sayfalar/tobb2b",
            //        defaults: new { controller = "Services", action = "Tobb2b", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Standart Mitgliedschaft / Standart Üyelik
            //routes.MapRoute(
            //        name: "StandardMitgliedschaftGerman",
            //        url: "{lang}/seiten/standard-mitgliedschaft",
            //        defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "StandardMitgliedschaftTurkey",
            //        url: "{lang}/sayfalar/standart-uyelik",
            //        defaults: new { controller = "MemberShip", action = "StandardMitgliedschaft", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Premium Mitgliedschaft / Premium Üyelik
            //routes.MapRoute(
            //        name: "PremiumMitgliedschaftGerman",
            //        url: "{lang}/seiten/premium-mitgliedschaft",
            //        defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "PremiumMitgliedschaftTurkey",
            //        url: "{lang}/sayfalar/premium-uyelik",
            //        defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaft", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Jahresberichte / Yıllık Raporlar
            //routes.MapRoute(
            //        name: "JahresberichteGerman",
            //        url: "{lang}/seiten/jahresberichte",
            //        defaults: new { controller = "Publication", action = "Jahresberichte", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "JahresberichteTurkey",
            //        url: "{lang}/sayfalar/yillik-raporlar",
            //        defaults: new { controller = "Publication", action = "Jahresberichte", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Infoblätter / Bilgi Sayfaları
            //routes.MapRoute(
            //        name: "InfoblatterGerman",
            //        url: "{lang}/seiten/infoblatter",
            //        defaults: new { controller = "Publication", action = "Infoblatter", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "InfoblatterTurkey",
            //        url: "{lang}/sayfalar/bilgi-sayfalari",
            //        defaults: new { controller = "Publication", action = "Infoblatter", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Ein- und Ausreisebestimmungen / Giriş-Çıkış Kuralları
            //routes.MapRoute(
            //        name: "EinUndAusreisebestimmungenGerman",
            //        url: "{lang}/seiten/ein-und-ausreisebestimmungen",
            //        defaults: new { controller = "Covid", action = "EinUndAusreisebestimmungen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "EinUndAusreisebestimmungenTurkey",
            //        url: "{lang}/sayfalar/giris-cikis-kurallari",
            //        defaults: new { controller = "Covid", action = "EinUndAusreisebestimmungen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Mabnahmen / Aktiviteler
            //routes.MapRoute(
            //        name: "MabnahmenGerman",
            //        url: "{lang}/seiten/mabnahmen",
            //        defaults: new { controller = "Covid", action = "Mabnahmen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "MabnahmenTurkey",
            //        url: "{lang}/sayfalar/aktiviteler",
            //        defaults: new { controller = "Covid", action = "Mabnahmen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Wichtige Informationsquellen / Önemli Bilgi Kaynakları
            //routes.MapRoute(
            //        name: "WichtigeInformationsquellenGerman",
            //        url: "{lang}/seiten/wichtige-informationsquellen",
            //        defaults: new { controller = "Covid", action = "WichtigeInformationsquellen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "WichtigeInformationsquellenTurkey",
            //        url: "{lang}/sayfalar/onemli-bilgi-kaynaklari",
            //        defaults: new { controller = "Covid", action = "WichtigeInformationsquellen", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Kalender / Etkinlik Takvimi
            //routes.MapRoute(
            //        name: "KalenderGerman",
            //        url: "{lang}/seiten/kalender",
            //        defaults: new { controller = "Event", action = "Kalender", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "KalenderTurkey",
            //        url: "{lang}/sayfalar/etkinlik-takvimi",
            //        defaults: new { controller = "Event", action = "Kalender", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region Markteintritt / Marketing
            //routes.MapRoute(
            //        name: "MarkteintrittGerman",
            //        url: "{lang}/seiten/markteintritt",
            //        defaults: new { controller = "Services", action = "Markteintritt", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "MarkteintrittTurkey",
            //        url: "{lang}/sayfalar/marketing",
            //        defaults: new { controller = "Services", action = "Markteintritt", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion

            //#region TD IHK Portal 
            //routes.MapRoute(
            //        name: "TDIHKPortalGerman",
            //        url: "{lang}/seiten/td-ihk-portal",
            //        defaults: new { controller = "Services", action = "TdIhkPortal", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );

            //routes.MapRoute(
            //        name: "TDIHKPortalTurkey",
            //        url: "{lang}/sayfalar/td-ihk-portal",
            //        defaults: new { controller = "Services", action = "TdIhkPortal", lang = language },
            //     new[] { "TDIHKCorporate.Controllers" }
            //    );
            //#endregion
             

            #region Anasayfa / Homepage
            routes.MapRoute(
                    name: "HomePageLanguage",
                    url: "{lang}/",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Home Search / Search
            routes.MapRoute(
                    name: "SearchGerman",
                    url: "{lang}/suche",
                    defaults: new { controller = "Home", action = "SearchForPages", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "SearchTurkey",
                    url: "{lang}/ara",
                    defaults: new { controller = "Home", action = "SearchForPages", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Event Archive / Etkinlik Arşivi
            routes.MapRoute(
                    name: "EventArchiveGerman",
                    url: "{lang}/seiten/event-archive",
                    defaults: new { controller = "Event", action = "EventArchive", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "EventArchiveTurkey",
                    url: "{lang}/sayfalar/etkinlik-arsivi",
                    defaults: new { controller = "Event", action = "EventArchive", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Contact / İletişim
            routes.MapRoute(
                    name: "ContactGerman",
                    url: "{lang}/seiten/kontakt",
                    defaults: new { controller = "AboutUs", action = "Contact", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "ContactTurkey",
                    url: "{lang}/sayfalar/iletisim",
                    defaults: new { controller = "AboutUs", action = "Contact", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Standart Mitgliedschaft Form / Standart Üyelik Formu
            routes.MapRoute(
                    name: "StandardMitgliedschaftFormGerman",
                    url: "{lang}/mitgliedschaft/standard-mitgliedschaft-form",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaftAntragsformular", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "StandardMitgliedschaftFormTurkey",
                    url: "{lang}/uyelik/standart-uyelik-formu",
                    defaults: new { controller = "MemberShip", action = "StandardMitgliedschaftAntragsformular", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Premium Mitgliedschaft Form / Premiuum Üyelik Formu
            routes.MapRoute(
                    name: "PremiumMitgliedschaftFormGerman",
                    url: "{lang}/mitgliedschaft/premium-mitgliedschaft-form",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaftAntragsformular", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "PremiumMitgliedschaftFormTurkey",
                    url: "{lang}/uyelik/premium-uyelik-formu",
                    defaults: new { controller = "MemberShip", action = "PremiumMitgliedschaftAntragsformular", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Üye Listesi / Mitgliederliste
            routes.MapRoute(
                    name: "EventDetailGerman",
                    url: "{lang}/events/{seolink}",
                    defaults: new { controller = "Event", action = "EventRegister", seolink = UrlParameter.Optional, lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "EventDetailTurkey",
                    url: "{lang}/etkinlikler/{seolink}",
                    defaults: new { controller = "Event", action = "EventRegister", seolink = UrlParameter.Optional, lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Üye Listesi / Mitgliederliste
            routes.MapRoute(
                    name: "MemberListGerman",
                    url: "{lang}/seiten/mitgliederliste",
                    defaults: new { controller = "MemberShip", action = "Mitgliederliste", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "MemberListTurkey",
                    url: "{lang}/sayfalar/uye-listesi",
                    defaults: new { controller = "MemberShip", action = "Mitgliederliste", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Institution / Kurum
            routes.MapRoute(
                    name: "InstitutionGerman",
                    url: "{lang}/seiten/institution",
                    defaults: new { controller = "AboutUs", action = "Institution", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "InstitutionTurkey",
                    url: "{lang}/sayfalar/kurum",
                    defaults: new { controller = "AboutUs", action = "Institution", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Jobangebote / İş teklifleri
            routes.MapRoute(
                    name: "JobangeboteGerman",
                    url: "{lang}/seiten/jobangebote",
                    defaults: new { controller = "Services", action = "Jobangebote", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "JobangeboteTurkey",
                    url: "{lang}/sayfalar/is-teklifleri",
                    defaults: new { controller = "Services", action = "Jobangebote", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Coronavirus Nachrichten
            routes.MapRoute(
                    name: "CoronaNewsGerman",
                    url: "{lang}/seiten/coronavirus-nachrichten",
                    defaults: new { controller = "Covid", action = "CoronavirusNachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "CoronaNewsTurkey",
                    url: "{lang}/sayfalar/korona-virus-haberleri",
                    defaults: new { controller = "Covid", action = "CoronavirusNachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Kalender / Etkinlik Takvimi
            routes.MapRoute(
                    name: "KalenderGerman",
                    url: "{lang}/seiten/kalender",
                    defaults: new { controller = "Event", action = "Kalender", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "KalenderTurkey",
                    url: "{lang}/sayfalar/etkinlik-takvimi",
                    defaults: new { controller = "Event", action = "Kalender", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Etkinliklerimiz / Veranstaltungen
            routes.MapRoute(
                       name: "EventsGerman",
                       url: "{lang}/seiten/events",
                       defaults: new { controller = "Event", action = "Index", lang = language },
                    new[] { "TDIHKCorporate.Controllers" }
                   );

            routes.MapRoute(
                      name: "EventsTurkish",
                      url: "{lang}/sayfalar/etkinliklerimiz",
                      defaults: new { controller = "Event", action = "Index", lang = language },
                   new[] { "TDIHKCorporate.Controllers" }
                  );
            #endregion

            #region Nachrichten Detail / Haberler Detau
            routes.MapRoute(
                    name: "NewsDetailGerman",
                    url: "{lang}/nachrichten/{seolink}",
                    defaults: new { controller = "Publication", action = "NachrichtenDetail", seolink = UrlParameter.Optional, lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "NewsDetailTurkey",
                    url: "{lang}/haberler/{seolink}",
                    defaults: new { controller = "Publication", action = "NachrichtenDetail", seolink = UrlParameter.Optional, lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            #endregion

            #region Nachrichten / Haberler
            routes.MapRoute(
                    name: "NewsGerman",
                    url: "{lang}/seiten/nachrichten/",
                    defaults: new { controller = "Publication", action = "Nachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "NewsTurkey",
                    url: "{lang}/sayfalar/haberler/",
                    defaults: new { controller = "Publication", action = "Nachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            #endregion

            #region Podcast
            routes.MapRoute(
                    name: "PodcastGerman",
                    url: "{lang}/seiten/podcasts/",
                    defaults: new { controller = "Publication", action = "Podcasts", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "PodcastTurkey",
                    url: "{lang}/sayfalar/podcastler/",
                    defaults: new { controller = "Publication", action = "Podcasts", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            #endregion

            #region Sayfalar
            //Sayfalar Almanca Routing
            routes.MapRoute(
                   name: "PageGerman",
                   url: "{lang}/seiten/{seolink}",
                   defaults: new { controller = "Page", action = "Show", lang = language },
                   new[] { "TDIHKCorporate.Controllers" }
               );


            //Sayfalar Türkçe Routing
            routes.MapRoute(
                   name: "PageTurkish",
                   url: "{lang}/sayfalar/{seoLink}",
                   defaults: new { controller = "Page", action = "Show", seoLink = UrlParameter.Optional, lang = language },
                new[] { "TDIHKCorporate.Controllers" }
               );
            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] { "TDIHKCorporate.Controllers" }
            );
        }
    }
}
