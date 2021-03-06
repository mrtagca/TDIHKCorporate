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
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class CovidController : SiteBaseController
    {
        //[OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        //[Compress]
        public ActionResult Index()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "Covid" });


            return View(pageItem);
        }

        //[OutputCache(Duration = 60, VaryByParam = "*", Location = OutputCacheLocation.Server, NoStore = true)]
        //[Compress]
        public ActionResult CoronavirusNachrichten()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "CoronaNews" });

            return View(pageItem);
        }
    }
}