using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class JobOffers
    {
        public int JobOfferID { get; set; }

        public string IsMemberTxt { get; set; }
        public bool IsMember { get; set; }
        public int MemberID { get; set; }

        public HttpPostedFileBase file { get; set; }
        public string LogoPath { get; set; }
        public string Position { get; set; }
        public string PositionDescription { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}