using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Images
    {
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public string ImageAlternateText { get; set; }
        public string ImageFileName { get; set; }
        public string ImageExtension { get; set; }
        public string ImageSeoLink { get; set; }
        public int FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

    }
}