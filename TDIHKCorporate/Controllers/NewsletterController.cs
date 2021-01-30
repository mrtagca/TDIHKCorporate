using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
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
                DapperRepository<NewsletterRegistrations> newsletterAdd = new DapperRepository<NewsletterRegistrations>();

                string IP = Request.UserHostAddress;

                int result = newsletterAdd.Execute(@"insert into NewsletterRegistrations (EmailAddress,[IP]) values (@email,@IP)", new { email = email, IP = IP });
                if (result > 0)
                {
                    return true;
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

        public ActionResult Vorteille()
        {
            return PartialView("_PartialVorteille");
        }
    }
}