using DbAccess.Dapper.Repository;
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

        public ActionResult FutureEvents()
        {
            return View();
        }

        public ActionResult LastEvents()
        {
            
            return View();
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