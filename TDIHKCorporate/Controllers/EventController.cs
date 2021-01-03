using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;


namespace TDIHKCorporate.Controllers
{
    public class EventController : SiteBaseController
    {
        // GET: Event
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

        public ActionResult EventArchive()
        {
            return View(GetLastEvents(0)); //all events
        }

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
            try
            {
                eventRegistrations.CreatedDate = DateTime.Now;
                eventRegistrations.CreatedBy = 1;

                DapperRepository<EventRegistrations> evtReg = new DapperRepository<EventRegistrations>();
                int result = evtReg.Execute(@"insert into EventRegistrations (EventIdentifier,[Name],Surname,EmailAddress,CorporationName,CreatedDate,CreatedBy) values (@EventIdentifier,@Name,@Surname,@EmailAddress,@CorporationName,@CreatedDate,@CreatedBy)
", eventRegistrations );

                if (result > 0)
                {
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

        public ActionResult FutureEvents()
        {
            return View(GetFutureEvents(3));
        }

        public ActionResult LastEvents()
        {

            return View(GetLastEvents(8));
        }

        public ActionResult Kalender()
        {
            return View(GetKalenderEvents(9));
        }

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
                                            where [Language] = 'de' and IsActive=1 and convert(date,EventDate) >= convert(date,getdate())
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"select * from [Events]
                                            where [Language] = 'de' and IsActive=1 and convert(date,EventDate) >= convert(date,getdate())
                                              order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
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

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name }).Take(count).ToList();
                }
                else
                {
                    eventList = events.GetList(@"SELECT  evc.EventCategoryName,ev.* FROM [Events] ev
inner join EventCategories evc
on ev.EventCategoryID = evc.ID

where ev.[Language] = @lang and CONVERT(date,EventDate) >= CONVERT(date,GETDATE()) and ev.IsActive=1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { lang = name });
                }

                return eventList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Events> GetSearchEvents(string keyword)
        {
            try
            {
                DapperRepository<Events> events = new DapperRepository<Events>();
                List<Events> eventList = new List<Events>();

                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                string name = cultureInfo.TwoLetterISOLanguageName;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    eventList = events.GetList(@"SELECT * FROM [IHK].[dbo].[Events] (NOLOCK)
WHERE (EventDescription like '%'+@search+'%' or EventTitle like '%'+@search+'%' or EventContent like '%'+@search+'%' or EventSeoKeywords like '%'+@search+'%' or  EventTags like '%'+@search+'%') and [Language] = @lang and IsActive = 1
order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", new { search = keyword, lang=name }).ToList();
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