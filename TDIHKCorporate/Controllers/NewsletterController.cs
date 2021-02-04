using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Mail;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.FormTypes;

namespace TDIHKCorporate.Controllers
{
    public class NewsletterController : SiteBaseController
    {
        // GET: Newsletter

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool AddEmail(string email)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                DapperRepository<NewsletterRegistrations> newsletterAdd = new DapperRepository<NewsletterRegistrations>();

                string IP = Request.UserHostAddress;

                int result = newsletterAdd.Execute(@"insert into NewsletterRegistrations (EmailAddress,[IP]) values (@email,@IP)", new { email = email, IP = IP });
                if (result > 0)
                {
                    DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                    EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                    {
                        TemplateIdentifier = "NewsletterInfo",
                        Language = lang
                    });

                    emailTemplate.TemplateHtml =
                        emailTemplate.TemplateHtml
                        .Replace("<##Email##>", email);

                    MailSender mailSender = new MailSender("MemberShip");
                    List<string> list = new List<string>();
                    list.Add(ConfigurationManager.AppSettings["NewsletterMailBox"]);
                    bool mailSent = mailSender.SendMail(emailTemplate, list);

                    if (mailSent)
                    {
                        return true; 
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [HttpPost]
        public bool CheckEmail(string email)
        {
            try
            {
                DapperRepository<NewsletterRegistrations> newsletterCheck = new DapperRepository<NewsletterRegistrations>();
                NewsletterRegistrations newsletterRegistrations = newsletterCheck.Get(@"SELECT * FROM NewsletterRegistrations (NOLOCK) where EmailAddress = @email", new { email = email });

                if (newsletterRegistrations != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Vorteille()
        {
            return PartialView("_PartialVorteille");
        }
    }
}