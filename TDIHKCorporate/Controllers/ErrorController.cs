using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Controllers
{
    public class ErrorController : SiteBaseController
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}