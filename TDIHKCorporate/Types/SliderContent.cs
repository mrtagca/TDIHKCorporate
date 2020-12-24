using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class SliderContent
    {
        public int SliderContentID { get; set; }
        public string SliderContentTitle { get; set; }
        public string Language { get; set; }
        public string ImagePath { get; set; }
        public int SliderID { get; set; }
        public string SliderUrl { get; set; }
        public int SliderPriority { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}