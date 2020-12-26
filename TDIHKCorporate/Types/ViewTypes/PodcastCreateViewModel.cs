using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class PodcastCreateViewModel
    {
        [Required]
        public string Language { get; set; }
        [Required]
        public HttpPostedFileBase file { get; set; }
        [Required]
        public string PodcastTitle { get; set; }
        [Required]
        public string PodcastDescription { get; set; }
    }
}