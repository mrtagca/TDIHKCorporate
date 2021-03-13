using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Helpers.Breadcrumb
{
    public static class BreadcrumbHelper
    {
        public static List<BreadCrumb> GetBreadCrumbs(int PageID, string language)
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