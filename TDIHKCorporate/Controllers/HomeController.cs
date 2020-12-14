using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            string url = Request.UrlReferrer.ToString();

            if (url.Contains("seiten/") || url.Contains("sayfalar/"))
            {
                if (language == "tr") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                    Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/sayfalar/" + page.PageSeoLink + "");
                }

                if (language == "de") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                    Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/seiten/" + page.PageSeoLink + "");
                }
            }

            if (url.Contains("nachrichten/") || url.Contains("haberler/"))
            {
                if (language == "tr") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<News> newsRepo = new DapperRepository<News>();
                    News news = newsRepo.Get(@"SELECT * FROM  News (NOLOCK)
where [Language] = @lang and NewsIdentifier in (SELECT top 1 NewsIdentifier FROM News (NOLOCK) where NewsSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/haberler/" + news.NewsSeoLink + "");
                }

                if (language == "de") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<News> newsRepo = new DapperRepository<News>();
                    News news = newsRepo.Get(@"SELECT * FROM  News (NOLOCK)
where [Language] = @lang and NewsIdentifier in (SELECT top 1 NewsIdentifier FROM News (NOLOCK) where NewsSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/nachrichten/" + news.NewsSeoLink + "");
                }
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

                                                                        where sl.SliderName ='Home Slider' and sl.[Language] = @lang and sc.[Language] = @lang
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

                                                                        where sl.SliderName ='Premium Mitglieder' and sl.[Language] = @lang and sc.[Language] = @lang
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
                                                             where sl.SliderName ='MitgliedschaftAdvantage' and sl.[Language] = @lang and sc.[Language] = @lang
                                                             order by SliderPriority", new { lang = name });


            return PartialView("_PartialMitgliedschaftAdvantage", sliderItems);
        }

        public ActionResult ShowHomePageKalender()
        {
            List<Events> eventList = GetEvents(8);

            return PartialView("_PartialShowHomePageKalender", eventList);
        }

        public List<Events> GetEvents(int count)
        {
            try
            {
                DapperRepository<Events> events = new DapperRepository<Events>();
                List<Events> eventList = new List<Events>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                if (count > 0)
                {
                    eventList = events.GetList(@"select * from [Events]
                                            where [Language] = @lang
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"select * from [Events]
                                            where [Language] = @lang
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}