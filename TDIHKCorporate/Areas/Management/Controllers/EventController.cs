using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class EventController : SiteBaseController
    {
        // GET: Management/Event
        public ActionResult Create()
        {
            return View();
        }

        public string GetEventCategories()
        {
            DapperRepository<EventCategories> getEventCategories = new DapperRepository<EventCategories>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            var result = getEventCategories.GetList(@"SELECT * FROM [IHK].[dbo].[EventCategories] (NOLOCK)
                                        WHERE [Language] = @lang", new { lang = name }).OrderBy(x => x.EventCategoryName);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string AddEvent(Events events)
        {
            DapperRepository<Events> addPage = new DapperRepository<Events>();

            string TimezoneId = System.Configuration.ConfigurationManager.AppSettings[System.Threading.Thread.CurrentThread.CurrentCulture.Name];
            events.CreatedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimezoneId);
            events.CreatedBy = 1;
            events.IsActive = true;

            var result = addPage.Execute(@"INSERT INTO IHK.dbo.[Events]([Language],EventCategoryID,EventImageId,EventThumbnailPath,EventTitle,EventDate,EventTime,EventContent,EventSeoLink,EventDescription,EventSeoKeywords,EventTags,EventQuota,EventCriticalQuota,CreatedDate,CreatedBy,IsActive) values (@Language,@EventCategoryID,@EventImageId,@EventThumbnailPath,@EventTitle,@EventDate,@EventTime,@EventContent,@EventSeoLink,@EventDescription,@EventSeoKeywords,@EventTags,@EventQuota,@EventCriticalQuota,@CreatedDate,@CreatedBy,@IsActive)", new
            {

                Language = events.Language,
                EventCategoryID = events.EventCategoryID,
                EventImageId = 0,
                EventThumbnailPath = "",
                EventTitle = events.EventTitle,
                EventDate = events.EventDate,
                EventTime = events.EventTime,
                EventContent = events.EventContent,
                EventSeoLink = events.EventSeoLink,
                EventDescription = events.EventDescription,
                EventSeoKeywords = events.EventSeoKeywords,
                EventTags = events.EventTags,
                EventQuota = events.EventQuota,
                EventCriticalQuota = events.EventCriticalQuota,
                CreatedDate = events.CreatedDate,
                CreatedBy = events.CreatedBy,
                IsActive = events.IsActive
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(true);
        }
    }
}