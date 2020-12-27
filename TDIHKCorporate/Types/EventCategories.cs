using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class EventCategories
    {
        public int ID { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string EventCategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}