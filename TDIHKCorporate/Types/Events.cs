using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Types
{
    public class Events
    {
        public string EventCategoryName { get; set; }
        public int EventID { get; set; }
        public string Language { get; set; }
        public int EventCategoryID { get; set; }
        public string EventImagePath { get; set; }
        public string EventThumbnailPath { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }

        [AllowHtml]
        public string EventContent { get; set; }
        public string EventSeoLink { get; set; }
        public string EventDescription { get; set; }
        public string EventSeoKeywords { get; set; }
        public string EventTags { get; set; }
        public int EventQuota { get; set; }
        public int EventCriticalQuota { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}