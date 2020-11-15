using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class HomeController : SiteBaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[AllowAnonymous]
        //[ChildActionOnly]
        public ActionResult MenuList()
        {
            DapperRepository<MenuItems> getPageCategories = new DapperRepository<MenuItems>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<MenuItems> menuItems = getPageCategories.GetList(@"select mi.*,p.PageSeoLink from MenuItems mi (NOLOCK)
                                        inner join Menus m (NOLOCK)
                                        on mi.MenuID = m.ID
                                        left join Pages p (NOLOCK)
                                        on mi.PageID = p.PageID

                                        where m.ID = 1 and mi.[Language] = @lang", new { lang = name }).OrderBy(x => x.MenuItemPriority).ToList();


            return PartialView("_PartialInvestData", menuItems);
        }
    }
}