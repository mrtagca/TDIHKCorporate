using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Podcasts
    {
        public int ID { get; set; }
        public string Language { get; set; }
        public string PodcastFilePath { get; set; }
        public string PodcastTitle { get; set; }
        public string PodcastDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}