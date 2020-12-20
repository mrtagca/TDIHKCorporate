using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class PodcastCreateViewModel
    {
        public string Language { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string PodcastTitle { get; set; }
        public string PodcastDescription { get; set; }
    }
}