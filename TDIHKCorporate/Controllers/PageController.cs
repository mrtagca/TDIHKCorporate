using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Breadcrumb;
using TDIHKCorporate.Helpers.Compress;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{

    public class PageController : SiteBaseController
    {
        //[OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        //[Compress]
        public ActionResult Show(string seoLink)
        {

            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
            where PageSeoLink = @pageSeoLink and [Language]=@lang", new { pageSeoLink = seoLink, lang = name });

            try
            {
                ViewBag.BreadcrumbList = BreadcrumbHelper.GetBreadCrumbs(pageItem.PageID, name);
            }
            catch (Exception ex)
            {
                 
            }

            return View(pageItem);
        }
    }
}