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

            #region Coronavirus Nachrichten
            routes.MapRoute(
                    name: "CoronaNewsGerman",
                    url: "{lang}/seiten/coronavirus-nachrichten",
                    defaults: new { controller = "Publication", action = "CoronaNews", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "CoronaNewsTurkey",
                    url: "{lang}/sayfalar/koronavirus-haberleri",
                    defaults: new { controller = "Publication", action = "CoronaNews", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );
            #endregion

            #region Event Categories / Etkinlik Kategorileri
            routes.MapRoute(
                    name: "EventsByCategoryGerman",
                    url: "{lang}/events/categories/{category}",
                    defaults: new { controller = "Event", action = "EventsByCategoryName", lang = language, category = UrlParameter.Optional },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "EventsByCategoryTurkey",
                    url: "{lang}/etkinlikler/kategoriler/{category}",
                    defaults: new { controller = "Event", action = "EventsByCategoryName", lang = language, category = UrlParameter.Optional },
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
                    name: "RealNewsGerman",
                    url: "{lang}/seiten/nachrichten/",
                    defaults: new { controller = "Publication", action = "RealNachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "RealNewsTurkey",
                    url: "{lang}/sayfalar/haberler/",
                    defaults: new { controller = "Publication", action = "RealNachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            #endregion

            #region Events Nachrichten / Etkinlik Haberler
            routes.MapRoute(
                    name: "NewsGerman",
                    url: "{lang}/seiten/events-nachrichten/",
                    defaults: new { controller = "Publication", action = "Nachrichten", lang = language },
                 new[] { "TDIHKCorporate.Controllers" }
                );

            routes.MapRoute(
                    name: "NewsTurkey",
                    url: "{lang}/sayfalar/etkinlik-haberleri/",
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
