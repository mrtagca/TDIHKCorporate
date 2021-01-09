using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class ContactController : ManagementBaseController
    {
        // GET: Management/Contact
        public ActionResult ContactFormList()
        {
            DapperRepository<ContactForm> podcastList = new DapperRepository<ContactForm>();
            List<ContactForm> result = podcastList.GetList(@"SELECT * FROM ContactForm (NOLOCK) order by CreatedDate desc", null).ToList();

            return View(result);
        }

        public ActionResult Show(int id)
        {
            DapperRepository<ContactForm> contact = new DapperRepository<ContactForm>();
            ContactForm  result = contact.Get(@"SELECT * FROM ContactForm (NOLOCK) where ContactFormID = @ContactFormID", new { ContactFormID  = id});

            contact.Execute(@"update ContactForm set IsRead = 1 where ContactFormID = @ContactFormID", new { ContactFormID = id });

            return View(result);
        }
    }
}