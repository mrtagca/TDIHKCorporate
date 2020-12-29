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
                    HttpContext.Session["Main" + langParameter] = "tr";
                    Session["Main" + cultureParameter] = "TR";
                }
                else if (Request.QueryString[langParameter] == "de")
                {
                    HttpContext.Session["Main" +langParameter] = "de";
                    HttpContext.Session["Main" + cultureParameter] = "DE";
                }
                else if (Request.QueryString["language"] == "en")
                {
                    HttpContext.Session["Main" +langParameter] = "en";
                    HttpContext.Session["Main" + cultureParameter] = "US";
                }
                else if (Request.QueryString[langParameter] == "fr")
                {
                    HttpContext.Session["Main" +langParameter] = "fr";
                    HttpContext.Session["Main" + cultureParameter] = "FR";
                }
            }

            string language = "";
            string culture = "";

            if (HttpContext.Session["Main" + langParameter] == null && HttpContext.Session["Main" + cultureParameter] == null)
            {
                language = "de";
                culture = "DE";

                HttpContext.Session["Main"+langParameter] = language;
                HttpContext.Session["Main" + cultureParameter] = culture;
            }
            else
            {
                language = HttpContext.Session["Main" + langParameter].ToString();
                culture = HttpContext.Session["Main" + cultureParameter].ToString();
            }

            if (Request.FilePath!=null)
            {
                if (Request.FilePath.ToString().Contains("/tr"))
                {
                    language = "tr";
                    culture = "TR";

                    HttpContext.Session["Main" + langParameter] = language;
                    HttpContext.Session["Main" + cultureParameter] = culture;
                }
                else if (Request.FilePath.ToString().Contains("/de"))
                {
                    language = "de";
                    culture = "DE";

                    HttpContext.Session["Main" + langParameter] = language;
                    HttpContext.Session["Main" + cultureParameter] = culture;
                }

            }



            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{language}-{culture}");

            //new SiteLanguage().SetLanguage(language);
            return base.BeginExecuteCore(callback, state);
        }
    }
}