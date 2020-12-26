using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Members
    {
        public int MemberID { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string MemberLogoPath { get; set; }
        public string MemberTitle { get; set; }
        public string MemberAddress { get; set; }
        public string MemberWebSite { get; set; }
        public string MemberPhone1 { get; set; }
        public string MemberPhone2 { get; set; }
        public string MemberEmailAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

    }
}