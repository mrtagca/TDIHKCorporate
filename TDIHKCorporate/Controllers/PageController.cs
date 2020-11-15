using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Controllers
{

    public class PageController : SiteBaseController
    {
        // GET: Page
        public ActionResult Show(string seolink)
        {
            return View();
        }
    }
}