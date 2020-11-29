using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Controllers
{
    public class AboutUsController : SiteBaseController
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Institution()
        {
            return View();
        }
    }
}