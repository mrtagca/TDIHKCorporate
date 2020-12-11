using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class NewsCategories
    {
        public int ID { get; set; }
        [Required]
        public string NewsCategoryName { get; set; }
        public int ParentCategoryID { get; set; }
        [Required]
        public string Language { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}