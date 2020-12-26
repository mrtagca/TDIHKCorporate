using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Types
{
    public class EmailTemplates
    {
        public int TemplateID { get; set; } 
        public string TemplateIdentifier { get; set; }
        public string Language { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string TemplateHtml { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}