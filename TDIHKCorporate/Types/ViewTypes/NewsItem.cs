using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class NewsItem
    {
        public int NewsID { get; set; }
        public string NewsIdentifier { get; set; }
        public string NewsTitle { get; set; }
        public string NewsLanguage { get; set; }
        public string NewsSeoLink { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Creator { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Updater { get; set; }
    }
}