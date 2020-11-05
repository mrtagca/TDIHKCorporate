using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace TDIHKCorporate.BaseControllers.MultiLanguage
{
    public class SiteBaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string langParameter = "language";
            if (Request.QueryString[langParameter] != null)
            {
                if (Request.QueryString[langParameter] == "tr")
                {
                    Session[langParameter] = "tr";
                    Session["culture"] = "TR";
                }
                else if (Request.QueryString[langParameter] == "de")
                {
                    Session[langParameter] = "de";
                    Session["culture"] = "DE";
                }
                else if (Request.QueryString["language"] == "en")
                {
                    Session[langParameter] = "en";
                    Session["culture"] = "US";
                }
                else if (Request.QueryString["language"] == "fr")
                {
                    Session[langParameter] = "fr";
                    Session["culture"] = "FR";
                }
            }

            var language = Session[langParameter] ?? "tr";
            var culture = Session["culture"] ?? "TR";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
            return base.BeginExecuteCore(callback, state);
        }
    }
}