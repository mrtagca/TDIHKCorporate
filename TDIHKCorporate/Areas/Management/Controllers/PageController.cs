using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class PageController : Controller
    {
        // GET: Management/Page
        public ActionResult Create()
        {
            return View();
        }
    }
}