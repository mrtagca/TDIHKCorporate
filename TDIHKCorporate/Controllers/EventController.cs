using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Compress;
using TDIHKCorporate.Helpers.Mail;
using TDIHKCorporate.Types;


namespace TDIHKCorporate.Controllers
{
    public class EventController : SiteBaseController
    {
        [OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult Index()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "Events" });

            try
            {
                ViewBag.BreadcrumbList = GetBreadCrumbs(pageItem.PageID, name);
            }
            catch (Exception ex)
            {

            }

            return View(pageItem);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public List<BreadCrumb> GetBreadCrumbs(int PageID, string language)
        {
            DapperRepository<BreadCrumb> breadCrumb = new DapperRepository<BreadCrumb>();
            List<BreadCrumb> breadCrumbList = breadCrumb.GetList(@"select mi.MenuLevel,mi.MenuName,pg.[Language],pg.PageSeoLink from MenuItems mi(nolock)
                                                                left join Pages pg (nolock)
                                                                on mi.PageID = pg.PageID
                                                                where (ID in (select distinct ParentMenuItemID from MenuItems
                                                                where ID in (select ParentMenuItemID from MenuItems where PageID=@PageID) or PageID = @PageID) or                       mi.PageID = @PageID) and mi.[Language]=@Language
                                                                order by mi.MenuLevel", new { PageID = PageID, Language = language });

            return breadCrumbList;
        }

        [OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult EventArchive()
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            DapperRepository<SearchItem> searchItemList = new DapperRepository<SearchItem>();
            List<SearchItem> searchItems = searchItemList.GetList(@"select * from
                                                (
                                                SELECT 'News' as [Type],NewsTitle as Title,NewsSeoLink as SeoLink,CreatedDate as [Date] FROM News  (NOLOCK)
                                                where [Language] = @lang and  IsActive = 1 and CONVERT(date,CreatedDate) < CONVERT(date,GETDATE())
 
                                                union

                                                SELECT 'Events' as [Type],EventTitle as Title,EventSeoLink as SeoLink,CreatedDate as [Date] FROM [Events]  (NOLOCK)
                                                where [Language] = @lang and  IsActive = 1  and CONVERT(date,EventDate) < CONVERT(date,GETDATE())
                                                ) as X
                                                order by X.[Date] desc", new { lang = name });

            return View(searchItems); //all events
        }

        [OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult EventRegister(string seolink)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            DapperRepository<Events> events = new DapperRepository<Events>();
            Events evt = events.Get(@"select * from [Events] (nolock) where [Language] = @Language and EventSeoLink = @EventSeoLink", new
            {
                Language = name,
                EventSeoLink = seolink
            });

            return View(evt);
        }


        [HttpPost]
        public string EventRegisterForm(EventRegistrations eventRegistrations)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string lang = cultureInfo.TwoLetterISOLanguageName;

            try
            {
                eventRegistrations.CreatedDate = DateTime.Now;
                eventRegistrations.CreatedBy = 1;

                DapperRepository<EventRegistrations> evtReg = new DapperRepository<EventRegistrations>();
                int result = evtReg.Execute(@"insert into EventRegistrations (EventIdentifier,[Name],Surname,EmailAddress,CorporationName,CreatedDate,CreatedBy) values (@EventIdentifier,@Name,@Surname,@EmailAddress,@CorporationName,@CreatedDate,@CreatedBy)
", eventRegistrations );

                if (result > 0)
                {
                    DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                    EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                    {
                        TemplateIdentifier = "StandardMembership",
                        Language = lang
                    });

                    emailTemplate.TemplateHtml =
                        emailTemplate.TemplateHtml
                        .Replace("<##Name##>", eventRegistrations.Name)
                        .Replace("<##Surname##>", eventRegistrations.Surname)
                        .Replace("<##Email##>", eventRegistrations.EmailAddress)
                        .Replace("<##CorporationName##>", eventRegistrations.CorporationName);

                    MailSender mailSender = new MailSender("Event");

                    EmailTemplates emailTemplateInfo = new EmailTemplates();
                    List<string> list = new List<string>();
                    list.Add(ConfigurationManager.AppSettings["EventMailBox"]);

                    mailSender.SendMail(emailTemplate, list);

                    return JsonConvert.SerializeObject(true);

                }
                else
                {
                    return JsonConvert.SerializeObject(false);
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult GetEventCategoriesArea()
        {

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            DapperRepository<EventCategories> eventCategories = new DapperRepository<EventCategories>();
            List<EventCategories> eventCategoryList = eventCategories.GetList(@"SELECT top 4 * FROM EventCategories where [Language] = @lang and IsActive = 1 order by CreatedDate desc", new { lang=name });

            return View(eventCategoryList);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult FutureEvents()
        {
            return View(GetFutureEvents(3));
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult LastEvents()
        {
            return View(GetLastEvents(9));
        }

        [OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult Kalender()
        {
            return View(GetKalenderEvents(9));
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public List<Events> GetKalenderEvents(int count)
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

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
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
                                            where [Language] = @lang and IsActive=1
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"select * from [Events] 
                                            where [Language] = @lang and IsActive=1
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public List<Events> GetLastEvents(int count)
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

where ev.[Language] = @lang and CONVERT(date,EventDate) < CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.* FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) < CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
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
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.*,(select COUNT(*) as RegisterCount from EventRegistrations (nolock) where EventIdentifier = ev.EventIdentifier) as RegisterCount FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime))", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.*,(select COUNT(*) as RegisterCount from EventRegistrations (nolock) where EventIdentifier = ev.EventIdentifier) as RegisterCount FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime))", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult SearchEvents(string search)
        {
            try
            {
                ViewBag.SearchKeyword = search;

                DapperRepository<Events> events = new DapperRepository<Events>();
                List<Events> eventList = new List<Events>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    eventList = events.GetList(@"SELECT * FROM [IHK].[dbo].[Events] (NOLOCK)
WHERE (EventDescription like '%'+@search+'%' or EventTitle like '%'+@search+'%' or EventContent like '%'+@search+'%' or EventSeoKeywords like '%'+@search+'%' or  EventTags like '%'+@search+'%') and [Language] = @lang and IsActive = 1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { search = search, lang=name }).ToList();
                }

                return View(eventList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        [Compress]
        public ActionResult EventsByCategoryName(string category)
        {
            try
            {
                ViewBag.EventCategory = category.ToUpper();

                DapperRepository<Events> events = new DapperRepository<Events>();
                List<Events> eventList = new List<Events>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                if (!string.IsNullOrWhiteSpace(category))
                {
                    eventList = events.GetList(@"select ev.* from [Events] ev (nolock)
inner join EventCategories evc (nolock)
on ev.EventCategoryID = evc.ID
where ev.[Language] = @lang and evc.[Language] = @lang and evc.EventCategoryName like '%'+@category+'%' ", new { category = category, lang = name }).ToList();
                }

                return View(eventList);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult SiteMap()
        {
            DapperRepository<Events> events = new DapperRepository<Events>();

            List<Events> eventList = events.GetList(@"select * from [Events] (nolock) where IsActive = 1 order by CreatedDate desc", null);

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

            foreach (var e in eventList)
            {
                xr.WriteStartElement("url");
                if (e.Language == "tr")
                {
                    xr.WriteElementString("loc", Request.Url.Scheme + "://" + Request.Url.Authority + "/tr/etkinlikler/" + e.EventSeoLink);
                }
                else
                {
                    xr.WriteElementString("loc",Request.Url.Scheme+"://"+ Request.Url.Authority + "/de/events/" + e.EventSeoLink);
                }
                xr.WriteElementString("lastmod", e.CreatedDate.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "daily");
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