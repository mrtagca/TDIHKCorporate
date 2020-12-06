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
    public class EventController : ManagementBaseController
    {
        // GET: Management/Event
        public ActionResult Create()
        {
            return View();
        }

        public string GetEventCategories(string language)
        {
            DapperRepository<EventCategories> getEventCategories = new DapperRepository<EventCategories>();

            var result = getEventCategories.GetList(@"SELECT * FROM [IHK].[dbo].[EventCategories] (NOLOCK)
                                        WHERE [Language] = @lang", new { lang = language }).OrderBy(x => x.EventCategoryName);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string AddEvent(Events events)
        {
            DapperRepository<Events> addPage = new DapperRepository<Events>();


            var result = addPage.Execute(@"INSERT INTO IHK.dbo.[Events]([Language],EventCategoryID,EventImagePath,EventThumbnailPath,EventTitle,EventDate,EventTime,EventContent,EventSeoLink,EventDescription,EventSeoKeywords,EventTags,EventQuota,EventCriticalQuota,CreatedDate,CreatedBy,IsActive) values (@Language,@EventCategoryID,@EventImagePath,@EventThumbnailPath,@EventTitle,@EventDate,@EventTime,@EventContent,@EventSeoLink,@EventDescription,@EventSeoKeywords,@EventTags,@EventQuota,@EventCriticalQuota,@CreatedDate,@CreatedBy,@IsActive)", new
            {

                Language = events.Language,
                EventCategoryID = events.EventCategoryID,
                EventImagePath = "",
                EventThumbnailPath = "",
                EventTitle = events.EventTitle,
                EventDate = events.EventDate,
                EventTime = events.EventTime,
                EventContent = events.EventContent,
                EventSeoLink = events.EventSeoLink,
                EventDescription = events.EventDescription,
                EventSeoKeywords = events.EventSeoKeywords.ToLower(),
                EventTags = events.EventTags.ToLower(),
                EventQuota = events.EventQuota,
                EventCriticalQuota = events.EventCriticalQuota,
                CreatedDate = DateTime.Now,
                CreatedBy = 1,
                IsActive = true
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(true);
        }

       
    }
}