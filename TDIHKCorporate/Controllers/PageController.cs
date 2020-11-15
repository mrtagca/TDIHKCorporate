using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Controllers
{
    //[Route("Detail/{Film_Id}/{seo_text}")]

    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Show(string seolink)
        {
            return View();
        }
    }
}