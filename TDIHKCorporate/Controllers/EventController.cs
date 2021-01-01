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


            return View(pageItem);
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
            return View(GetEvents(9));
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
    }
}