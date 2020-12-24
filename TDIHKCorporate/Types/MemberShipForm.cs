using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class MemberShipForm
    {
        public int ID { get; set; }
        public string IPAdress { get; set; }

        public bool IsCorporate { get; set; }
        [Required]
        public string MemberType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HomePhone { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string WebSite { get; set; }
        [Required]
        public string Fax { get; set; }
        [Required]
        public string ResponsiblePersonel { get; set; }
        [Required]
        public string PersonelPosition { get; set; }
        [Required]
        public string CorporationIncome { get; set; }
        public string CorporationLogoPath { get; set; }
        public string MemberSuggestion1 { get; set; }
        public string MemberSuggestion2 { get; set; }
        public string MemberSuggestion3 { get; set; }
        public string SuggestionInfo { get; set; }
        public string SuggestionLocationAndTime { get; set; }
        public bool IsMailSent { get; set; }
        public DateTime MailSentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}