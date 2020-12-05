using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class CreateContentViewModel
    {
        [Required]
        public int SliderID { get; set; }
        [Required]
        public string SliderContentTitle { get; set; }
        [Required]
        public string SliderContentLanguage { get; set; }
        [Required]
        public HttpPostedFileBase sliderFile { get; set; }
        [Required]
        public int SliderPriority { get; set; }
        [Required]
        public string SliderURL { get; set; }
    }
}