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

        public ActionResult AddMenuItem()
        {
            return View();
        }

        public string GetAllMenus()
        {
            try
            {
                DapperRepository<Menus> menus = new DapperRepository<Menus>();
                List<Menus> menuList = menus.GetList(@"SELECT * FROM Menus", null);

                return JsonConvert.SerializeObject(menuList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GetAllMenuItems(string language)
        {
            try
            {
                DapperRepository<MenuItems> menuItems = new DapperRepository<MenuItems>();
                List<MenuItems> menuItemsList = menuItems.GetList(@"SELECT * FROM MenuItems (NOLOCK)
                                        where [Language] = @lang and MenuLevel in (0,1)
                                        order by [Language],MenuLevel,MenuName", new { lang = language });

                return JsonConvert.SerializeObject(menuItemsList);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string AddMenuItem(MenuItems menuItem)
        {

            try
            {
                DapperRepository<MenuItems> item = new DapperRepository<MenuItems>();
                int result = item.Execute(@"insert into MenuItems (MenuID,PageID,ParentMenuItemID,MenuItemPriority,[Language],MenuName,MenuLevel,IsSubMenu) 
										values (@menuID,@pageID,@parentMenuItemID,@menuItemPriority,@language,@menuName,@menuLevel,@IsSubMenu)", menuItem);

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}