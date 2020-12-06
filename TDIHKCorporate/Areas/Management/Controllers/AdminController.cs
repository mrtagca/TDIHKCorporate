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
    public class AdminController : ManagementBaseController
    {
        public ActionResult ChangeLanguage(string language)
        {
            new SiteLanguage().SetLanguage(language);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Management/Admin
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


    }
}   