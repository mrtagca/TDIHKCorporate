using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class NewsController : SiteBaseController
    {
        // GET: News
        public ActionResult Show(string lang,string category,string seolink)
        {

            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            News newsItem = news.Get(@"SELECT * FROM News (NOLOCK)
            where NewsSeoLink = @newsSeoLink", new { newsSeoLink = seolink });

            return View(newsItem);
        }
    }
}