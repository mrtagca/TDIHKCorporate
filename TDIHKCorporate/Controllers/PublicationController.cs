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
    public class PublicationController : SiteBaseController
    {
        // GET: Publication
        public ActionResult Index()
        {

            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "Publications" });


            return View(pageItem);
        }

        public ActionResult Jahresberichte()
        {

            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "YearReports" });


            return View(pageItem);
        }

        public ActionResult Infoblatter()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "InformationNotes" });


            return View(pageItem);
        }

        public ActionResult Nachrichten()
        {
            DapperRepository<News> page = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = page.GetList(@"select nw.* from News nw (NOLOCK)
                                                    inner join NewsCategories nwc (NOLOCK)
                                                    on nw.NewsCategoryID = nwc.ID
                                                    where nw.[Language] = @language and nwc.[Language] = @language
                                                    order by CreatedDate desc
                                                    OFFSET 5 ROWS", new { language = name });


            return View(newsList);
        }

        public ActionResult GetNachrichtenHead()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = news.GetList(@"select top 5 nwc.NewsCategoryName,nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID

                                                where nw.[Language] = @language and nwc.[Language] = @language
                                                order by CreatedDate desc", new { language = name });


            return PartialView("_PartialNachrichtenHead", newsList);
        }

        public ActionResult GetNewsForNewsDetail()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = news.GetList(@"select top 3 nwc.NewsCategoryName,nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID

                                                where nw.[Language] = @language and nwc.[Language] = @language
                                                order by CreatedDate desc", new { language = name });


            return PartialView("_PartialNewsForNewsDetail", newsList);
        }


        public ActionResult NachrichtenDetail(string seoLink)
        {
            DapperRepository<News> newsRepo = new DapperRepository<News>();
            News news = newsRepo.Get(@"select * from News (NOLOCK) where NewsSeoLink = @NewsSeoLink", new {
                NewsSeoLink = seoLink
            });
            return View(news);
        }

        public ActionResult Podcasts()
        {
            return View();
        }


    }
}