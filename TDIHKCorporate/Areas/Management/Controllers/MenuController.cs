using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class MenuController : SiteBaseController
    {
        public ActionResult MenuList()
        {
            DapperRepository<Menus> getMenus = new DapperRepository<Menus>();

            List<Menus> result = getMenus.GetList(@"SELECT * FROM [IHK].[dbo].[Menus] (NOLOCK) order by MenuName", null);

            return View(result);
        }

        public string GetMenuItems(int menuID)
        {
            DapperRepository<MenuItemsByMenuId> getMenuItemsByMenuId = new DapperRepository<MenuItemsByMenuId>();

            List<MenuItemsByMenuId> result = getMenuItemsByMenuId.GetList(@"
                                                select p.PageTitle,mi.[Language],mi.MenuItemPriority,mi.MenuName,mi.MenuLevel,mi.IsSubMenu from [IHK].[dbo].[MenuItems] mi (nolock)
                                                inner join  [IHK].[dbo].[Pages] p (NOLOCK)
                                                on mi.PageID = p.PageID
                                                where mi.MenuID = @menuId", new { menuId = menuID });

            return JsonConvert.SerializeObject(result);
        }
    }
}