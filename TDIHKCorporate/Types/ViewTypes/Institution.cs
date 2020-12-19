using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types.ViewTypes
{
    public class Institution
    {
        public int ID { get; set; }
        public string Language { get; set; }
        public string InstitutionTitle { get; set; }
        public string InstitutionDescription { get; set; }
        public DateTime InstitutionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}