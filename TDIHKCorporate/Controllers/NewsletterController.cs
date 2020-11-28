using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index()
        {
            return View();
        }
    }
}