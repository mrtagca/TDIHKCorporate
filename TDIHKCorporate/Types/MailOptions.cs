using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class MailOptions
    {
        public int MailOptionsID { get; set; }
        public string MailType { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
        public string CredentialUsername { get; set; }
        public string CredentialPassword { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}