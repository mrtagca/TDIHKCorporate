using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;

namespace TDIHKCorporate.Controllers
{
    public class HomeController : SiteBaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}