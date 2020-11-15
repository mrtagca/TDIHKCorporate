using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class SliderController : SiteBaseController
    {

        public ActionResult CreateContent()
        {
            return View();
        }
    }
}