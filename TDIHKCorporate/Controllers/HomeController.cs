using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    
    public class HomeController : SiteBaseController
    {
        public ActionResult ChangeLanguage(string language)
        {
            HttpContext.Session["Mainlanguage"] = "";
            HttpContext.Session["Mainculture"] = "";

            new SiteLanguage().SetLanguage(language);

            if (Request.UrlReferrer != null)
            {
                string url = Request.UrlReferrer.ToString();

                #region Sayfalar
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
                #endregion

                #region Haberler
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
                #endregion

                #region Etkinlikler
                if (url.Contains("etkinlikler/") || url.Contains("events/"))
                {
                    if (language == "tr") //change tr
                    {
                        string seo = url.Substring(url.LastIndexOf("/") + 1);

                        DapperRepository<Events> eventRepo = new DapperRepository<Events>();
                        Events evt = eventRepo.Get(@"SELECT * FROM  [Events] (NOLOCK)
where [Language] = @lang and EventIdentifier in (SELECT top 1 EventIdentifier FROM [Events] (NOLOCK) where EventSeoLink = @seoUrl) and IsActive=1 ", new { lang = language, seoUrl = seo });

                        if (evt != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/etkinlikler/" + evt.EventSeoLink + "");
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

                        DapperRepository<Events> eventRepo = new DapperRepository<Events>();
                        Events evt = eventRepo.Get(@"SELECT * FROM  [Events] (NOLOCK)
where [Language] = @lang and EventIdentifier in (SELECT top 1 EventIdentifier FROM [Events] (NOLOCK) where EventSeoLink = @seoUrl) and IsActive=1 ", new { lang = language, seoUrl = seo });


                        if (evt != null)
                        {
                            return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/events/" + evt.EventSeoLink + "");
                        }
                        else
                        {
                            HttpContext.Session["Mainlanguage"] = "tr";
                            Session["Mainculture"] = "TR";
                            return Redirect(Request.UrlReferrer.ToString());
                        }


                    }
                }
                #endregion

                if (url.Contains("mitgliedschaft/standard") || url.Contains("uyelik/standart"))
                {
                    if (language == "tr") //change tr
                    {
                        return Redirect("https://" + Request.UrlReferrer.Authority + "/tr/uyelik/standart-uyelik-formu");
                    }
                    else
                    {
                        return Redirect("https://" + Request.UrlReferrer.Authority + "/de/mitgliedschaft/standard-mitgliedschaft-form");
                    }
                }

                if (url.Contains("mitgliedschaft/premium") || url.Contains("uyelik/premium"))
                {
                    if (language == "tr") //change tr
                    {
                        return Redirect("https://" + Request.UrlReferrer.Authority + "/tr/uyelik/premium-uyelik-formu");
                    }
                    else
                    {
                        return Redirect("https://" + Request.UrlReferrer.Authority + "/de/mitgliedschaft/premium-mitgliedschaft-form");
                    }
                }


                string lang = "";
                string cult = "";

                if (language == "tr") //change tr
                {
                    lang = "tr";
                    cult = "TR";

                    HttpContext.Session["Mainlanguage"] = lang;
                    HttpContext.Session["Mainculture"] = cult;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");

                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    lang = "de";
                    cult = "DE";

                    HttpContext.Session["Mainlanguage"] = lang;
                    HttpContext.Session["Mainculture"] = cult;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");

                    return Redirect(Request.UrlReferrer.ToString());
                }


            }
            else
            {

                return Redirect(Request.Url.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult Index()
        {
            try
            {
                //string lang = "";
                //string cult = "";

                //if (Request.Url.ToString().Contains("de"))
                //{
                //    lang = "de";
                //    cult = "DE";

                //}
                //else if (Request.Url.ToString().Contains("tr"))
                //{
                //    lang = "tr";
                //    cult = "TR";
                //}
                //else
                //{
                //    if (HttpContext.Session["Mainlanguage"] == null && HttpContext.Session["Mainculture"] == null)
                //    {
                //        lang = "de";
                //        cult = "DE";
                //    }
                //    else
                //    {
                //        lang = HttpContext.Session["Mainlanguage"].ToString();
                //        cult = HttpContext.Session["Mainculture"].ToString();
                //    }

                //}

                //HttpContext.Session["Mainlanguage"] = lang;
                //HttpContext.Session["Mainculture"] = cult;
                //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");
                //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{lang}-{cult}");

                //ViewBag.Error = lang + " - " + cult;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [OutputCache(Duration = 60, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult SearchForPages(string search)
        {
            ViewBag.SearchText = search;

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            DapperRepository<SearchItem> searchItemList = new DapperRepository<SearchItem>();
            List<SearchItem> searchItems = searchItemList.GetList(@"SELECT 'Pages' as [Type],PageTitle as Title,PageSeoLink as SeoLink FROM Pages  (NOLOCK)
where  (PageTitle like '%'+@search+'%' or PageContent like '%'+@search+'%' or PageSeoLink like '%'+@search+'%' or PageSeoKeywords like '%'+@search+'%' ) and [Language] = @lang
union
SELECT 'News' as [Type],NewsTitle as Title,NewsSeoLink as SeoLink FROM News  (NOLOCK)
where  (NewsTitle like '%'+@search+'%' or NewsContent like '%'+@search+'%' or NewsSeoLink like '%'+@search+'%' or NewsSeoKeywords like '%'+@search+'%' ) and [Language] = @lang
union
SELECT 'Events' as [Type],EventTitle as Title,EventSeoLink as SeoLink FROM [Events]  (NOLOCK)
where  (EventTitle like '%'+@search+'%' or EventContent like '%'+@search+'%' or EventSeoLink like '%'+@search+'%' or EventSeoKeywords like '%'+@search+'%' ) and [Language] = @lang", new { search = search, lang = name });


            return View(searchItems);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult ShowHomePageKalender()
        {
            List<Events> eventList = GetFutureEvents(8);

            return PartialView("_PartialShowHomePageKalender", eventList);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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
                    eventList = events.GetList(@"select * from [Events]
                                            where [Language] = @lang and IsActive=1 and convert(date,EventDate) >= convert(date,getdate())
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime))", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"select * from [Events]
                                            where [Language] = @lang and IsActive=1 and convert(date,EventDate) >= convert(date,getdate())
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
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

        public ActionResult SiteMap()
        {
            DapperRepository<Pages> pages = new DapperRepository<Pages>();

            List<Pages> pageList = pages.GetList(@"select * from Pages (nolock) where IsActive = 1 order by CreatedDate desc", null);

            Response.Clear();
            //Response.ContentTpye ile bu Action'ın View'ını XML tabanlı olarak ayarlıyoruz.
            Response.ContentType = "text/xml";
            XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");//urlset etiketi açıyoruz
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
            /* sitemap dosyamızın olmazsa olmazını ekledik. Şeması bu dedik buraya kadar.  */

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", Request.Url.Authority);
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "daily");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();

            //Burada veritabanımızdaki Personelleri SiteMap'e ekliyoruz.

            foreach (var p in pageList)
            {
                xr.WriteStartElement("url");
                if (p.Language == "tr")
                {
                    xr.WriteElementString("loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/tr/sayfalar/" + p.PageSeoLink);
                }
                else
                {
                    xr.WriteElementString("loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/de/seiten/" + p.PageSeoLink);
                }
                xr.WriteElementString("lastmod", p.CreatedDate.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "weekly");
                xr.WriteEndElement();
            }

            xr.WriteEndDocument();
            //urlset etiketini kapattık
            xr.Flush();
            xr.Close();
            Response.End();
            return View();
        }

    }
}