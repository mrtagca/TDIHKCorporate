using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class AdminPanelSummary
    {
        public int PageCount { get; set; }
        public int NewsCount { get; set; }
        public int EventCount { get; set; }
        public int ContactCount { get; set; }
    }
}