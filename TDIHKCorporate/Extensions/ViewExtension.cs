using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace TDIHKCorporate.Extensions
{
    public static class ViewExtension
    {
        //public static string RenderToString(this PartialViewResult partialView,Controller)
        //{
        //    var httpContext = HttpContext.Current;

        //    if (httpContext == null)
        //    {
        //        throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
        //    }

        //    var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

        //    var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

        //    var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

        //    var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

        //    var sb = new StringBuilder();

        //    using (var sw = new StringWriter(sb))
        //    {
        //        using (var tw = new HtmlTextWriter(sw))
        //        {
        //            view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
        //        }
        //    }

        //    return sb.ToString();
        //}


        public static string ConvertPartialViewToString(PartialViewResult partialView,string controllerName)
        {
            using (var sw = new StringWriter())
            {
                var httpContext = HttpContext.Current;


                var contName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

                contName = "Home";

                var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, contName);

                var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

                partialView.View = ViewEngines.Engines
                  .FindPartialView(controllerContext, partialView.ViewName).View;

                

                var vc = new ViewContext(
                  controllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return partialViewString;
            }
        }
    }
}