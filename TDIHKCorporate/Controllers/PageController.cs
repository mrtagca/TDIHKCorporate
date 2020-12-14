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

    public class PageController : SiteBaseController
    {
        // GET: Page

        public ActionResult Show(string seoLink)
        {

            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
            where PageSeoLink = @pageSeoLink and [Language]=@lang", new { pageSeoLink = seoLink, lang = name });

            //Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
            //where[Language] = @language and PageSeoLink = @pageSeoLink", new { language = name, pageSeoLink = seoLink });

            return View(pageItem);
        }
    }
}