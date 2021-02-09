using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TDIHKCorporate.BaseControllers.MultiLanguage;
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
                ViewBag.BreadcrumbList = GetBreadCrumbs(pageItem.PageID, name);
            }
            catch (Exception ex)
            {
                 
            }

            return View(pageItem);
        }

        //[ChildActionOnly]
        //[OutputCache(Duration = 60)]
        public List<BreadCrumb> GetBreadCrumbs(int PageID, string language)
        {
            DapperRepository<BreadCrumb> breadCrumb = new DapperRepository<BreadCrumb>();
            List<BreadCrumb> breadCrumbList = breadCrumb.GetList(@"select mi.MenuLevel,mi.MenuName,pg.[Language],pg.PageSeoLink from MenuItems mi(nolock)
                                                                left join Pages pg (nolock)
                                                                on mi.PageID = pg.PageID
                                                                where (ID in (select distinct ParentMenuItemID from MenuItems
                                                                where ID in (select ParentMenuItemID from MenuItems where PageID=@PageID) or PageID = @PageID) or                       mi.PageID = @PageID) and mi.[Language]=@Language
                                                                order by mi.MenuLevel", new { PageID = PageID, Language = language });

            return breadCrumbList;
        }
    }
}