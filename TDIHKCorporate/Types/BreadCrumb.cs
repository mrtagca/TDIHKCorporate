using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class BreadCrumb
    {
        public int MenuLevel { get; set; }
        public string MenuName { get; set; }
        public string Language { get; set; }
        public string PageSeoLink { get; set; }
    }
}