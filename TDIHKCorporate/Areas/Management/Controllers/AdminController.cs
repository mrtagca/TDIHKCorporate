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

    }
}