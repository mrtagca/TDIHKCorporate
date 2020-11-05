using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    [RouteArea("Management")]
    public class AdminController : SiteBaseController
    {
        public ActionResult ChangeLanguage(string lang)
        {
            new SiteLanguage().SetLanguage(lang);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            return RedirectToAction("Index", controllerName);
        }

        // GET: Management/Admin
        public ActionResult Index()
        {
            string a = "asdasd";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


    }
}   