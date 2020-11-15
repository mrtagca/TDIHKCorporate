using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class MenuItemsByMenuId
    {
        public string PageTitle { get; set; }
        public string Language { get; set; }
        public int MenuItemPriority { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public bool IsSubMenu { get; set; }
    }
}