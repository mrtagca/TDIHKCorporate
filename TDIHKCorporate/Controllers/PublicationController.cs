﻿using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Compress;
using TDIHKCorporate.Helpers.Rss;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class PublicationController : SiteBaseController
    {
        
        [Compress]
        public ActionResult Nachrichten()
        {
            DapperRepository<News> page = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = page.GetList(@"select top 33 * from
                                                (
                                                select nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID
                                                where nw.[Language] = @language and nwc.[Language] = @language and nw.IsEventNew=1 and nw.IsActive = 1
                                                order by CreatedDate desc
                                                OFFSET 3 ROWS
                                                ) as X", new { language = name });


            return View(newsList);
        }

        //[ChildActionOnly]
        public ActionResult GetNachrichtenHead()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = news.GetList(@"select top 3 nwc.NewsCategoryName,nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID

                                                where nw.[Language] = @language and nwc.[Language] = @language and nw.IsEventNew=1 and nw.IsActive = 1
                                                order by CreatedDate desc", new { language = name });


            return PartialView("_PartialNachrichtenHead", newsList);
        }
        //[ChildActionOnly]
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

        [Compress]
        public ActionResult NachrichtenDetail(string seoLink)
        {
            DapperRepository<News> newsRepo = new DapperRepository<News>();
            News news = newsRepo.Get(@"select * from News (NOLOCK) where NewsSeoLink = @NewsSeoLink", new {
                NewsSeoLink = seoLink
            });
            return View(news);
        }

        [Compress]
        public ActionResult Podcasts()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "Podcasts" });

            return View(pageItem);
        }

        //[ChildActionOnly]
        public ActionResult GetPodcasts()
        {
            DapperRepository<Podcasts> podcastRepo = new DapperRepository<Podcasts>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<Podcasts> podcastList = podcastRepo.GetList(@"select * from Podcasts (NOLOCK) where IsActive=1 and [Language] = @Language order by CreatedDate desc", new
            {
                Language = name
            });
            return PartialView("_PartialPodcasts", podcastList);
        }

        //[ChildActionOnly]
        public ActionResult GetCoronaNachrichtenHead()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = news.GetList(@"select top 3 nwc.NewsCategoryName,nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID
                                                where nw.[Language] = @language and nwc.[Language] = @language and nwc.NewsCategoryName = 'COVID-19'
                                                order by CreatedDate desc", new { language = name });


            return PartialView("_PartialCoronaNachrichtenHead", newsList);
        }

        [Compress]
        public ActionResult CoronaNews()
        {
            DapperRepository<News> page = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            //List<News> newsList = page.GetList(@"select top 33 * from
            //                                    (
            //                                    select nw.* from News nw (NOLOCK)
            //                                    inner join NewsCategories nwc (NOLOCK)
            //                                    on nw.NewsCategoryID = nwc.ID
            //                                    where nw.[Language] = @language and nwc.[Language] = @language and nwc.NewsCategoryName = 'COVID-19'
            //                                    order by CreatedDate desc
            //                                    OFFSET 3 ROWS
            //                                    ) as X", new { language = name });

            List<News> newsList = new List<News>();
            if (name == "de")
            {
                newsList.AddRange(new RssFeeder().GetRobertKochRss().OrderByDescending(x=>x.CreatedDate));
                newsList.AddRange(new RssFeeder().GetWhoRss().OrderByDescending(x => x.CreatedDate));
            }
            else
            {
                newsList.AddRange(new RssFeeder().GetSaglikGovTrRss().OrderByDescending(x => x.CreatedDate));
                newsList.AddRange(new RssFeeder().GetWhoRss().OrderByDescending(x => x.CreatedDate));
            }

            return View(newsList.OrderByDescending(x=>x.CreatedDate).ToList());
        }

        [Compress]
        public ActionResult RealNachrichten()
        {
            DapperRepository<News> page = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = page.GetList(@"select top 33 * from
                                                (
                                                select nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID
                                                where nw.[Language] = @language and nwc.[Language] = @language and nw.IsEventNew=0 and nw.IsActive = 1
                                                order by CreatedDate desc
                                                OFFSET 3 ROWS
                                                ) as X", new { language = name });


            return View(newsList);
        }

        //[ChildActionOnly]
        public ActionResult GetRealNachrichtenHead()
        {
            DapperRepository<News> news = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<News> newsList = news.GetList(@"select top 3 nwc.NewsCategoryName,nw.* from News nw (NOLOCK)
                                                inner join NewsCategories nwc (NOLOCK)
                                                on nw.NewsCategoryID = nwc.ID

                                                where nw.[Language] = @language and nwc.[Language] = @language and nw.IsEventNew=0 and nw.IsActive = 1
                                                order by CreatedDate desc", new { language = name });


            return PartialView("_PartialRealNachrichtenHead", newsList);
        }

        public ActionResult SoundTest()
        {
            DapperRepository<Podcasts> podcastRepo = new DapperRepository<Podcasts>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<Podcasts> podcastList = podcastRepo.GetList(@"select * from Podcasts (NOLOCK) where IsActive=1 and [Language] = @Language order by CreatedDate desc", new
            {
                Language = name
            });
            return View(podcastList);
        }
    }
}