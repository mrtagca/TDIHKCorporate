using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Members
    {
        public int MemberID { get; set; }
        public char AlphabetStarter { get; set; }
        public char RealAlphabetStarter { get; set; }

        [Required]
        public HttpPostedFileBase file { get; set; }
        public string MemberLogoPath { get; set; }
        [Required]
        public string MemberTitle { get; set; }
        [Required]
        public string MemberAddress { get; set; }
        [Required]
        public string MemberWebSite { get; set; }
        [Required]
        public string MemberPhone1 { get; set; }
        public string MemberPhone2 { get; set; }
        [Required]
        public string MemberEmailAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}