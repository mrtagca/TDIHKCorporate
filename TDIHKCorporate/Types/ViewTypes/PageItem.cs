using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class PageItem
    {
        public int PageID { get; set; }
        public string PageIdentifier { get; set; }
        public string PageTitle { get; set; }
        public string PageLanguage { get; set; }
        public string PageSeoLink { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Creator { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Updater { get; set; }
        public bool IsActive { get; set; }

    }
}