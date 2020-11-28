using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.Models.Language;

namespace TDIHKCorporate.BaseControllers.MultiLanguage
{
    public class SiteBaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string langParameter = "language";
            string cultureParameter = "culture";
            if (Request.QueryString[langParameter] != null)
            {
                if (Request.QueryString[langParameter] == "tr")
                {
                    HttpContext.Session[langParameter] = "tr";
                    Session[cultureParameter] = "TR";
                }
                else if (Request.QueryString[langParameter] == "de")
                {
                    HttpContext.Session[langParameter] = "de";
                    HttpContext.Session[cultureParameter] = "DE";
                }
                else if (Request.QueryString["language"] == "en")
                {
                    HttpContext.Session[langParameter] = "en";
                    HttpContext.Session[cultureParameter] = "US";
                }
                else if (Request.QueryString[langParameter] == "fr")
                {
                    HttpContext.Session[langParameter] = "fr";
                    HttpContext.Session[cultureParameter] = "FR";
                }
            }

            string language = "";
            string culture = "";

            if (HttpContext.Session[langParameter] == null && HttpContext.Session[cultureParameter] == null)
            {
                language = "de";
                culture = "DE";

                HttpContext.Session[langParameter] = language;
                HttpContext.Session[cultureParameter] = culture;
            }
            else
            {
                language = HttpContext.Session[langParameter].ToString();
                culture = HttpContext.Session[cultureParameter].ToString();
            }


            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{language}-{culture}");

            //new SiteLanguage().SetLanguage(language);
            return base.BeginExecuteCore(callback, state);
        }
    }
}