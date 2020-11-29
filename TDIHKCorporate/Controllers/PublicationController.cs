﻿using DbAccess.Dapper.Repository;
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
            return View();
        }

        public ActionResult Infoblatter()
        {
            return View();
        }

        public ActionResult Nachrichten()
        {
            return View();
        }

        public ActionResult Podcasts()
        {
            return View();
        }
    }
}