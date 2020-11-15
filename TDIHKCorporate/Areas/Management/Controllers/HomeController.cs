using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class HomeController : SiteBaseController
    {
        // GET: Management/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}