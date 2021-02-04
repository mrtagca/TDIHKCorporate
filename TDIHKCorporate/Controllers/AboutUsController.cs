using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
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
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Controllers
{
    public class AboutUsController : SiteBaseController
    {
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult Institution()
        {
            DapperRepository<Pages> inst = new DapperRepository<Pages>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages institution = inst.Get("select * from Pages where PageIdentifier=@PageIdentifier and [Language]=@Language", new
            {
                PageIdentifier = "Institution",
                Language = name
            });


            return View(institution);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult GetInstitutionTimeline()
        {
            DapperRepository<Institution> inst = new DapperRepository<Institution>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<Institution> institutionList = inst.GetList("select * from Institutions (NOLOCK) where IsActive = 1 and [Language] = @Language order by InstitutionDate desc", new { Language = name });


            return PartialView("_PartialInstitutionTimeline", institutionList);
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        public ActionResult Contact()
        {
            return View();
        }

        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Server, NoStore = true)]
        [HttpPost]
        public string AddContact(ContactForm contactForm)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                contactForm.IPAddress = Request.UserHostAddress;
                contactForm.CreatedDate = DateTime.Now;
                contactForm.IsRead = false;

                DapperRepository<ContactForm> contact = new DapperRepository<ContactForm>();

                int result = contact.Execute(@"insert into ContactForm ([Name],Surname,[Message],EmailAdress,IPAddress,CreatedDate) values (@Name,@Surname,@Message,@EmailAdress,@IPAddress,@CreatedDate)", contactForm);

                if (result > 0)
                {
                    DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                    EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                    {
                        TemplateIdentifier = "ContactFormInfo",
                        Language = lang
                    });

                    emailTemplate.TemplateHtml =
                        emailTemplate.TemplateHtml
                        .Replace("<##Name##>", contactForm.Name)
                        .Replace("<##Surname##>", contactForm.Surname)
                        .Replace("<##Email##>", contactForm.EmailAdress)
                        .Replace("<##Message##>", contactForm.EmailAdress);

                    MailSender mailSender = new MailSender("MemberShip");
                    List<string> list = new List<string>();
                    list.Add(ConfigurationManager.AppSettings["ContactMailBox"]);
                    bool mailSent = mailSender.SendMail(emailTemplate, list);

                    if (mailSent)
                    {
                        return JsonConvert.SerializeObject(true); 
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(false);

                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(false);
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}