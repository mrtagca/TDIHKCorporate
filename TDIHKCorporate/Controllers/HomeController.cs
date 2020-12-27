using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class HomeController : SiteBaseController
    {
        public ActionResult ChangeLanguage(string language)
        {
            new SiteLanguage().SetLanguage(language);

            if (Request.UrlReferrer != null)
            {
                string url = Request.UrlReferrer.ToString();

                if (url.Contains("seiten/") || url.Contains("sayfalar/"))
                {
                    if (language == "tr") //change tr
                    {
                        string seo = url.Substring(url.LastIndexOf("/") + 1);

                        DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                        Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) and IsActive = 1 ", new { lang = language, seoUrl = seo });



                        if (page != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/sayfalar/" + page.PageSeoLink + "");
                        }
                        else
                        {
                            HttpContext.Session["Mainlanguage"] = "de";
                            Session["Mainculture"] = "DE";
                            return Redirect(Request.UrlReferrer.ToString());
                        }


                    }

                    if (language == "de") //change tr
                    {
                        string seo = url.Substring(url.LastIndexOf("/") + 1);

                        DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                        Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });

                        if (page != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/seiten/" + page.PageSeoLink + "");
                        }
                        else
                        {
                            HttpContext.Session["Mainlanguage"] = "tr";
                            Session["Mainculture"] = "TR";
                            return Redirect(Request.UrlReferrer.ToString());
                        }
                    }
                }

                if (url.Contains("nachrichten/") || url.Contains("haberler/"))
                {
                    if (language == "tr") //change tr
                    {
                        string seo = url.Substring(url.LastIndexOf("/") + 1);

                        DapperRepository<News> newsRepo = new DapperRepository<News>();
                        News news = newsRepo.Get(@"SELECT * FROM  News (NOLOCK)
where [Language] = @lang and NewsIdentifier in (SELECT top 1 NewsIdentifier FROM News (NOLOCK) where NewsSeoLink = @seoUrl) and IsActive=1 ", new { lang = language, seoUrl = seo });

                        if (news != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/haberler/" + news.NewsSeoLink + "");
                        }
                        else
                        {
                            HttpContext.Session["Mainlanguage"] = "de";
                            Session["Mainculture"] = "DE";
                            return Redirect(Request.UrlReferrer.ToString());
                        }


                    }

                    if (language == "de") //change tr
                    {
                        string seo = url.Substring(url.LastIndexOf("/") + 1);

                        DapperRepository<News> newsRepo = new DapperRepository<News>();
                        News news = newsRepo.Get(@"SELECT * FROM  News (NOLOCK)
where [Language] = @lang and NewsIdentifier in (SELECT top 1 NewsIdentifier FROM News (NOLOCK) where NewsSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });


                        if (news != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/nachrichten/" + news.NewsSeoLink + "");
                        }
                        else
                        {
                            HttpContext.Session["Mainlanguage"] = "tr";
                            Session["Mainculture"] = "TR";
                            return Redirect(Request.UrlReferrer.ToString());
                        }


                    }
                }
            }
            else
            {

                return Redirect(Request.Url.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        //public ActionResult ChangeLanguage(string language)
        //{

        //    new SiteLanguage().SetLanguage(language);
        //    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

        //    string url = Request.UrlReferrer.ToString();


        //    return RedirectToAction("Index", "Home");

        //}

        // GET: Home
        public ActionResult Index()
        {
            string lang = "";
            string cult = "";

            if (Request.Url.ToString().Contains("de"))
            {
                lang = "de";
                cult = "DE";

            }
            else if (Request.Url.ToString().Contains("tr"))
            {
                lang = "tr";
                cult = "TR";
            }
            else
            {
                if (HttpContext.Session["Mainlanguage"] == null && HttpContext.Session["Mainculture"] == null)
                {
                    lang = "de";
                    cult = "DE";
                }
                else
                {
                    lang = HttpContext.Session["Mainlanguage"].ToString();
                    cult = HttpContext.Session["Mainculture"].ToString();
                }

            }

            HttpContext.Session["Mainlanguage"] = lang;
            HttpContext.Session["Mainculture"] = cult;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");


            return View();
        }

        public ActionResult MenuList()
        {
            DapperRepository<MenuItems> getPageCategories = new DapperRepository<MenuItems>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<MenuItems> menuItems = getPageCategories.GetList(@"select mi.*,p.PageSeoLink,p.IsActive as PageIsActive from MenuItems mi (NOLOCK)
                                        inner join Menus m (NOLOCK)
                                        on mi.MenuID = m.ID
                                        left join Pages p (NOLOCK)
                                        on mi.PageID = p.PageID

                                        where m.ID = 1 and mi.[Language] = @lang", new { lang = name }).OrderBy(x => x.MenuItemPriority).ToList();


            return PartialView("_PartialInvestData", menuItems);
        }

        public ActionResult ShowHomePageSlider()
        {
            DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<SliderContent> sliderItems = sliderContent.GetList(@"SELECT sc.* FROM SliderContent sc (NOLOCK) 
                                                                        inner join Sliders sl (NOLOCK)
                                                                        on sc.SliderID = sl.SliderID

                                                                        where sl.SliderName ='Home Slider' and sl.[Language] = @lang and sc.[Language] = @lang and sc.IsActive=1
                                                                        order by SliderPriority", new { lang = name });


            return PartialView("_PartialHomePageSlider", sliderItems);
        }

        public ActionResult ShowPremiumMembersSlider()
        {
            DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<SliderContent> sliderItems = sliderContent.GetList(@"SELECT sc.* FROM SliderContent sc (NOLOCK) 
                                                                        inner join Sliders sl (NOLOCK)
                                                                        on sc.SliderID = sl.SliderID

                                                                        where sl.SliderName ='Premium Mitglieder' and sl.[Language] = @lang and sc.[Language] = @lang and sc.IsActive=1
                                                                        order by SliderPriority", new { lang = name });


            return PartialView("_PartialPremiumMembers", sliderItems);
        }

        public ActionResult ShowPremiumAdvantageSlider()
        {
            DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<SliderContent> sliderItems = sliderContent.GetList(@"SELECT sc.* FROM SliderContent sc (NOLOCK) 
                                                             inner join Sliders sl (NOLOCK)
                                                             on sc.SliderID = sl.SliderID
                                                             where sl.SliderName ='MitgliedschaftAdvantage' and sl.[Language] = @lang and sc.[Language] = @lang and sc.IsActive=1
                                                             order by SliderPriority", new { lang = name });


            return PartialView("_PartialMitgliedschaftAdvantage", sliderItems);
        }

        public ActionResult ShowHomePageKalender()
        {
            List<Events> eventList = GetFutureEvents(8);

            return PartialView("_PartialShowHomePageKalender", eventList);
        }

        public List<Events> GetFutureEvents(int count)
        {
            try
            {
                DapperRepository<Events> events = new DapperRepository<Events>();
                List<Events> eventList = new List<Events>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                if (count > 0)
                {
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.* FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE())
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.* FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE())
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult GetHomepagePodcasts()
        {
            //
            DapperRepository<Podcasts> podcastRepo = new DapperRepository<Podcasts>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<Podcasts> podcastList = podcastRepo.GetList(@"select top 3 * from Podcasts (NOLOCK) where IsActive=1 and [Language] = @Language order by CreatedDate desc", new
            {
                Language = name
            });
            return PartialView("_PartialHomePodcasts", podcastList);
        }

        public ActionResult ShowHomePageNachrichten()
        {

            try
            {
                DapperRepository<News> news = new DapperRepository<News>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                List<News> newsList = news.GetList(@"select top 3 * from News (nolock) where [Language] = @Language order by CreatedDate desc", new { Language = name });

                return PartialView("_PartialShowHomePageNachrichten", newsList);
            }
            catch (Exception ex)
            {
                return PartialView("_PartialShowHomePageNachrichten", null);
            }


        }
    }
}