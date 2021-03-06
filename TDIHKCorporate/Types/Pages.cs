﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Pages
    {
        public int PageID { get; set; }
        public int PageCategoryID { get; set; }
        public string PageImagePath { get; set; }
        public string PageThumbnailPath { get; set; }
        public string PageIdentifier { get; set; }
        public string PageTitle { get; set; }

        [System.Web.Mvc.AllowHtml]
        public string PageContent { get; set; }
        public string PageSeoLink { get; set; }
        public string PageSeoKeywords { get; set; }
        public string Language { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDynamicPage { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public bool NewsletterPart { get; set; }
        public bool VorteillePart { get; set; }
    }
}