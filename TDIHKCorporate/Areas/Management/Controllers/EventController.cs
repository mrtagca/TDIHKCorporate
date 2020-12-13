using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

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

            var result = addPage.Execute(@"INSERT INTO IHK.dbo.[Events]([Language],EventIdentifier,EventCategoryID,EventImagePath,EventThumbnailPath,EventTitle,EventDate,EventTime,EventContent,EventSeoLink,EventDescription,EventSeoKeywords,EventTags,EventQuota,EventCriticalQuota,CreatedDate,CreatedBy,IsActive) values (@Language,@EventIdentifier,@EventCategoryID,@EventImagePath,@EventThumbnailPath,@EventTitle,@EventDate,@EventTime,@EventContent,@EventSeoLink,@EventDescription,@EventSeoKeywords,@EventTags,@EventQuota,@EventCriticalQuota,@CreatedDate,@CreatedBy,@IsActive)", new
            {

                Language = events.Language,
                EventIdentifier = events.EventIdentifier,
                EventCategoryID = events.EventCategoryID,
                EventImagePath = events.EventImagePath,
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

        public ActionResult EventList()
        {
            DapperRepository<Events> events = new DapperRepository<Events>();

            List<Events> eventList = events.GetList(@"select * from [Events]
                                            order by CONVERT(datetime,CONVERT(nvarchar,EventDate)+' '+CONVERT(nvarchar,EventTime)) desc", null);

            return View(eventList);
        }

        public ActionResult Edit(int id)
        {
            DapperRepository<Events> events = new DapperRepository<Events>();
            Events ev = events.Get(@"select * from [Events] where EventID = @id", new { id = id });
            return View(ev);
        }

        [HttpPost]
        public string EditEvent(Events events)
        {
            DapperRepository<Events> addPage = new DapperRepository<Events>();

            var result = addPage.Execute(@"update [Events] set [Language]=@Language,EventCategoryID=@EventCategoryID,EventImagePath=@EventImagePath,EventDescription=@EventDescription,EventTitle=@EventTitle,EventDate=@EventDate,EventTime=@EventTime,EventContent=@EventContent,EventSeoLink=@EventSeoLink,EventSeoKeywords=@EventSeoKeywords,EventTags=@EventTags,EventQuota=@EventQuota,EventCriticalQuota=@EventCriticalQuota,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,IsActive=@IsActive
                where EventID = @EventID", new
            {
                EventID=events.EventID,
                Language = events.Language,
                EventCategoryID = events.EventCategoryID,
                EventImagePath = events.EventImagePath,
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

        [HttpPost]
        public string GetEventIdentifiers(string lang)
        {
            DapperRepository<EventIdentifier> getEventIdentifiers = new DapperRepository<EventIdentifier>();

            List<EventIdentifier> result = new List<EventIdentifier>();

            if (lang == "tr")
            {
                result = getEventIdentifiers.GetList(@"SELECT distinct CreatedDate,EventIdentifier as IdentifierName,EventTitle FROM [Events] (NOLOCK)
where [Language] = @lang and EventIdentifier is not null and EventIdentifier<>''
order by CreatedDate desc", new { lang = "de" });
            }
            else if (lang == "de")
            {
                result = getEventIdentifiers.GetList(@"SELECT distinct CreatedDate,EventIdentifier as IdentifierName,EventTitle FROM [Events] (NOLOCK)
where [Language] = @lang and EventIdentifier is not null and EventIdentifier<>''
order by CreatedDate desc", new { lang = "tr" });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }
    }
}