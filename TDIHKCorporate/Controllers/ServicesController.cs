using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Controllers
{
    public class ServicesController : SiteBaseController
    {
        // GET: Services
        public ActionResult Dienstleistungen()
        {
            return View();
        }

        public ActionResult Markteintritt()
        {
            return View();
        }

        public ActionResult Türkei()
        {
            return View();
        }

        public ActionResult Deutschland()
        {
            return View();
        }

        public ActionResult Mustervertrage()
        {
            return View();
        }

        public ActionResult ForderungAndFinanzierung()
        {
            return View();
        }

        public ActionResult Aubenhandelsportal()
        {
            return View();
        }

        public ActionResult TdIhkPortal()
        {
            return View();
        }

        public ActionResult Tobb2b()
        {
            return View();
        }

        public ActionResult Konferenzraum()
        {
            return View();
        }

        public ActionResult Jobangebote()
        {
            return View();
        }
    }
}