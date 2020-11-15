using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class MenuItems
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int PageID { get; set; }
        public int ParentMenuItemID { get; set; }
        public int MenuItemPriority { get; set; }
        public string Language { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public bool IsSubMenu { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string PageSeoLink { get; set; }
    }
}