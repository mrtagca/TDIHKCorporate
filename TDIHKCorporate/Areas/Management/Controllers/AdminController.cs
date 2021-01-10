using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.Areas.Management.Filters;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    
    [RouteArea("Management")]
    public class AdminController : ManagementBaseController
    {
        public ActionResult ChangeLanguage(string language)
        {
            new SiteLanguage().SetLanguage(language);
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Management/Admin

        [AuthorizationFilter]
        public ActionResult Index()
        {
            DapperRepository<AdminPanelSummary> summary = new DapperRepository<AdminPanelSummary>();
            AdminPanelSummary adminPanelSummary = summary.Get(@"declare @pageCount int,@newsCount int,@eventCount int,@contactCount int

select @pageCount = COUNT(*) from Pages where IsActive = 1 
select @newsCount = COUNT(*)  from News where IsActive = 1 
select @eventCount = COUNT(*) from [Events] where IsActive = 1 
select @contactCount = COUNT(*) from ContactForm 
 
 select @pageCount as [PageCount],@newsCount as [NewsCount],@eventCount as [EventCount],@contactCount as [ContactCount]", null);

            ViewBag.PageCount = adminPanelSummary.PageCount;
            ViewBag.NewsCount = adminPanelSummary.NewsCount;
            ViewBag.EventCount = adminPanelSummary.EventCount;
            ViewBag.ContactCount = adminPanelSummary.ContactCount;

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            DapperRepository<Users> users = new DapperRepository<Users>();
            Users usr = users.Get(@"select roles.RoleName,usr.* from Users usr (nolock)
                                    inner join Roles roles (nolock)
                                    on usr.RoleID = roles.ID
                                    where usr.Username = @Username and usr.[Password] = @Password and usr.IsActive = 1", new { Username = username, Password = password });

            if (usr != null)
            {

                HttpContext.Session["RoleName"] = usr.RoleName;
                HttpContext.Session["UserID"] = usr.UserID;
                HttpContext.Session["RoleID"] = usr.RoleID;
                HttpContext.Session["Username"] = usr.Username;
                HttpContext.Session["Email"] = usr.Email;

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı veya şifre yanlış!";
                return View();
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session.RemoveAll();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}