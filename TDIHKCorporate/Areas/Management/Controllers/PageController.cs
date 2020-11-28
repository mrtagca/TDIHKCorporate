﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using DbAccess.Dapper.Repository;
using TDIHKCorporate.Types;
using System.Globalization;
using Newtonsoft.Json;
using TDIHKCorporate.Extensions;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class PageController : SiteBaseController
    {
        // GET: Management/Page
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewExtension.ConvertPartialViewToString(new PartialViewResult() { ViewName="_PartialHeader" }, "Home");

            DapperRepository<Pages> getPage = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages page = getPage.Get("select * from Pages where [Language] = @lang and PageId = @pageId", new { lang = name , pageId = id});

            return View(page);
        }

        public string GetPageCategories()
        {
            DapperRepository<PageCategories> getPageCategories = new DapperRepository<PageCategories>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            var result = getPageCategories.GetList(@"SELECT * FROM [IHK].[dbo].[PageCategories] (NOLOCK)
                                        WHERE [Language] = @lang", new { lang = name }).OrderBy(x => x.PageCategoryName);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        [HttpPost]
        public string AddPage(Pages pages)
        {
            DapperRepository<Pages> addPage = new DapperRepository<Pages>();

            string TimezoneId = System.Configuration.ConfigurationManager.AppSettings[System.Threading.Thread.CurrentThread.CurrentCulture.Name];
            pages.CreatedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimezoneId);
            pages.CreatedBy = 1;
            pages.IsActive = true;

            var result = addPage.Execute(@"INSERT INTO Pages([PageCategoryID],[PageImageId],[PageThumbnailPath],[PageTitle],[PageContent],[PageSeoLink],[PageSeoKeywords],[Language],[CreatedDate],[CreatedBy],[IsActive]) values(@PageCategoryID, @PageImageId, @PageThumbnailPath, @PageTitle, @PageContent, @PageSeoLink, @PageSeoKeywords, @Language, @CreatedDate, @CreatedBy,@IsActive)", new
            {

                PageCategoryID = pages.PageCategoryID,
                PageImageID = "",
                PageThumbnailPath = "",
                PageTitle = pages.PageTitle,
                PageContent = pages.PageContent,
                PageSeoLink = pages.PageSeoLink,
                PageSeoKeywords = pages.PageSeoKeywords,
                Language = pages.Language,
                CreatedDate = pages.CreatedDate,
                CreatedBy = pages.CreatedBy,
                IsActive = pages.IsActive

            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }
    }
}