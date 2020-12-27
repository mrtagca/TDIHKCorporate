﻿using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
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

            var result = getEventCategories.GetList(@"select * from EventCategories WITH(NOLOCK,INDEX=IX_EventCategories_Language)
WHERE [Language] = @lang order by EventCategoryName", new { lang = language });

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string AddEvent(Events events)
        {
            DapperRepository<Events> addPage = new DapperRepository<Events>();


            string identifier = "";

            if (string.IsNullOrWhiteSpace(events.EventIdentifier))
            {
                identifier = Guid.NewGuid().ToString().ToUpper();
            }
            else
            {
                identifier = events.EventIdentifier;
            }

            var result = addPage.Execute(@"INSERT INTO IHK.dbo.[Events]([Language],EventIdentifier,EventCategoryID,EventImagePath,EventThumbnailPath,EventTitle,EventDate,EventTime,EventContent,EventSeoLink,EventDescription,EventSeoKeywords,EventTags,EventQuota,EventCriticalQuota,CreatedDate,CreatedBy,IsActive) values (@Language,@EventIdentifier,@EventCategoryID,@EventImagePath,@EventThumbnailPath,@EventTitle,@EventDate,@EventTime,@EventContent,@EventSeoLink,@EventDescription,@EventSeoKeywords,@EventTags,@EventQuota,@EventCriticalQuota,@CreatedDate,@CreatedBy,@IsActive)", new
            {

                Language = events.Language,
                EventIdentifier = identifier,
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
                EventID = events.EventID,
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

            lang = (lang == "tr") ? "de" : "tr";

            result = getEventIdentifiers.GetList(@"SELECT distinct CreatedDate,EventIdentifier as IdentifierName,EventTitle FROM [Events] WITH(NOLOCK)
where [Language] = @lang and EventIdentifier<>''
order by CreatedDate desc", new { lang = lang });

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string PassiveEvent(int eventID)
        {
            try
            {
                DapperRepository<Pages> page = new DapperRepository<Pages>();

                int result = page.Execute(@"update Events set IsActive = 0,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where EventID = @EventID", new { EventID = eventID,UpdatedDate=DateTime.Now,UpdatedBy=1 });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public string ActivateEvent(int eventID)
        {
            try
            {
                DapperRepository<Pages> page = new DapperRepository<Pages>();

                int result = page.Execute(@"update Events set IsActive = 1,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where EventID = @EventID", new { EventID = eventID, UpdatedDate = DateTime.Now, UpdatedBy = 1 });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public ActionResult AddEventCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEventCategory(EventCategories eventCategories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    eventCategories.CreatedDate = DateTime.Now;
                    eventCategories.CreatedBy = 1;
                    eventCategories.IsActive = true;

                    DapperRepository<EventCategories> evcRepo = new DapperRepository<EventCategories>();

                    var result = evcRepo.Execute(@"insert into EventCategories ([Language],EventCategoryName,CreatedDate,CreatedBy,IsActive) values (@Language,@EventCategoryName,@CreatedDate,@CreatedBy,@IsActive)", eventCategories);


                    ViewBag.Success = "Success";
                    return View(); 
                }
                else
                {
                    ViewBag.Error = "Please check the values!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail: "+ex.Message;
                return View();
            }
        }


    }
}