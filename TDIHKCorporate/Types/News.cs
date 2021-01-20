using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Types
{
    public class News
    {
        public int NewsID { get; set; }
        public int NewsCategoryID { get; set; }
        public string NewsImagePath { get; set; }
        public string NewsThumbnailPath { get; set; }
        public string NewsIdentifier { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        [AllowHtml]
        public string NewsContent { get; set; }
        public string NewsSeoLink { get; set; }
        public string NewsSeoKeywords { get; set; }
        public string Language { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsEventNew { get; set; }
        public bool IsActive { get; set; }

        public string NewsCategoryName { get; set; }

    }
}