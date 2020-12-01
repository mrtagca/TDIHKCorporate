using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

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
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "ConferenceRoom" });


            return View(pageItem);
        }

        public ActionResult Jobangebote()
        {
            return View();
        }
    }
}