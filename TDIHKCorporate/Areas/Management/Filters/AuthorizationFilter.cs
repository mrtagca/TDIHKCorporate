using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TDIHKCorporate.Areas.Management.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                Roles = filterContext.HttpContext.Session["RoleName"].ToString();
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["UserID"] == null)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Admin";
                routeValues["action"] = "Login";
                routeValues["area"] = "Management";

                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
            else
            {
                Roles = filterContext.HttpContext.Session["RoleName"].ToString();
            }

        }
    }
}
