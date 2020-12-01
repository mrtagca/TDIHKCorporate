using System.Web.Mvc;

namespace TDIHKCorporate.Areas.Management
{
    public class ManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Management";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

          //  context.MapRoute(
          //    name: "Management",
          //    url: "management",
          //    defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional }
          //);

            context.MapRoute(
              name: "management",
              url: "Management/{controller}/{action}/{id}",
              defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
          );

            context.MapRoute(
                "Management_default",
                "Management/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}