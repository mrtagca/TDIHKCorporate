using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Controllers
{
    public class AboutUsController : SiteBaseController
    {
        

        public ActionResult Institution()
        {
            DapperRepository<Pages> inst = new DapperRepository<Pages>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages institution = inst.Get("select * from Pages where PageIdentifier=@PageIdentifier and [Language]=@Language", new
            {
                PageIdentifier = "Institution",
                Language = name
            });


            return View(institution);
        }

        public ActionResult GetInstitutionTimeline()
        {
            DapperRepository<Institution> inst = new DapperRepository<Institution>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<Institution> institutionList = inst.GetList("select * from Institutions (NOLOCK) where IsActive = 1 and [Language] = @Language order by InstitutionDate desc",new { Language =name });


            return PartialView("_PartialInstitutionTimeline", institutionList);
        }
    }
}