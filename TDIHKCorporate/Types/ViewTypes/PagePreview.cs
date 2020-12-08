using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class PagePreview
    {
        [AllowHtml]
        public string Html { get; set; }
    }
}