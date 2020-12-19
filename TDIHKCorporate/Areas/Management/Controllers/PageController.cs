using System;
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
using TDIHKCorporate.Types.ViewTypes;
using System.Text;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class PageController : ManagementBaseController
    {
        // GET: Management/Page
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {

            DapperRepository<Pages> getPage = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages page = getPage.Get("select * from Pages where  PageId = @pageId", new { lang = name, pageId = id });

            return View(page);
        }

        [HttpPost]
        public string EditPage(Pages pages)
        {

            try
            {
                DapperRepository<Pages> addPage = new DapperRepository<Pages>();


                int result = addPage.Execute(@"update Pages set PageImagePath=@PageImagePath,PageCategoryID = @pageCategoryID,PageIdentifier=@pageIdentifier,PageTitle=@pageTitle,PageContent=@pageContent,PageSeoLink=@pageSeoLink,PageSeoKeywords=@pageSeoKeywords,[Language]=@language,UpdatedDate=@updatedDate,UpdatedBy=@UpdatedBy,IsActive=@IsActive
                                        where PageID = @pageID", new
                {
                    pageID = pages.PageID,
                    PageCategoryID = pages.PageCategoryID,
                    PageImagePath = pages.PageImagePath,
                    PageThumbnailPath = "",
                    pageIdentifier = pages.PageIdentifier,
                    PageTitle = pages.PageTitle,
                    PageContent = pages.PageContent,
                    PageSeoLink = pages.PageSeoLink,
                    PageSeoKeywords = pages.PageSeoKeywords,
                    Language = pages.Language,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = 1,
                    IsActive = true

                });

                if (result > 0)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(true);
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(false);
                }
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(false);
            }
        }

        [HttpGet]
        public string GetPageCategories(string Language)
        {
            DapperRepository<PageCategories> getPageCategories = new DapperRepository<PageCategories>();

            var result = getPageCategories.GetList(@"SELECT * FROM [IHK].[dbo].[PageCategories] (NOLOCK) where [Language]=@lang", new { lang = Language }).OrderBy(x => x.PageCategoryName);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        [HttpPost]
        public string AddPage(Pages pages)
        {

            try
            {
                DapperRepository<Pages> addPage = new DapperRepository<Pages>();

                string TimezoneId = System.Configuration.ConfigurationManager.AppSettings[System.Threading.Thread.CurrentThread.CurrentCulture.Name];
                pages.CreatedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimezoneId);
                pages.CreatedBy = 1;
                pages.IsActive = true;

                int result = addPage.Execute(@"INSERT INTO Pages([PageCategoryID],[PageIdentifier],[PageImagePath],[PageThumbnailPath],[PageTitle],[PageContent],[PageSeoLink],[PageSeoKeywords],[Language],[CreatedDate],[CreatedBy],[IsActive]) values(@PageCategoryID,@PageIdentifier, @PageImagePath, @PageThumbnailPath, @PageTitle, @PageContent, @PageSeoLink, @PageSeoKeywords, @Language, @CreatedDate, @CreatedBy,@IsActive)", new
                {

                    PageCategoryID = pages.PageCategoryID,
                    PageIdentifier = pages.PageIdentifier,
                    PageImagePath = pages.PageImagePath,
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

                if (result > 0)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(true);
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(false);
                }
            }
            catch (Exception ex)
            {

                return Newtonsoft.Json.JsonConvert.SerializeObject(false);
            }

        }

        public ActionResult PageList()
        {
            DapperRepository<PageItem> pageList = new DapperRepository<PageItem>();
            var result = pageList.GetList(@"SELECT  
                                            pg.PageID,pg.PageIdentifier,
                                            pg.PageTitle,pg.[Language] as PageLanguage,
                                            pg.PageSeoLink,
                                            convert(nvarchar,pg.CreatedDate,120) as CreatedDate,
                                            case when (pg.CreatedBy = usr.UserID) then usr.Username else null end as Creator,
                                            convert(nvarchar,pg.UpdatedDate,120) as UpdatedDate,
                                            case when (pg.UpdatedBy = usr.UserID) then usr.Username else null end as Updater,
                                            pg.IsActive

                                            FROM Pages pg (NOLOCK)
                                            left join PageCategories pgc (NOLOCK)
                                            on pg.PageCategoryID = pgc.ID
                                            left join Users usr
                                            on pg.CreatedBy = usr.UserID or pg.UpdatedBy = usr.UserID
                                            ", null).OrderBy(x => x.PageTitle).ToList();


            return View(result);
        }
        public string GetPages(string language)
        {
            try
            {
                DapperRepository<Pages> page = new DapperRepository<Pages>();
                List<Pages> pageList = page.GetList(@"select * from Pages (NOLOCK) 
                                    where [Language] = @lang", new { lang = language });

                return JsonConvert.SerializeObject(pageList);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        public ActionResult AddPageCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPageCategory(PageCategories pageCategories)
        {
            try
            {
                DapperRepository<PageCategories> pageCategory = new DapperRepository<PageCategories>();

                int result = pageCategory.Execute(@"insert into PageCategories (PageCategoryName,[Language],CreatedDate,CreatedBy,IsActive)
  values (@pageCategoryName,@language,@createdDate,@createdBy,@isActive)", new { pageCategoryName = pageCategories.PageCategoryName, language = pageCategories.Language, createdDate = DateTime.Now, createdBy = 1, isActive = true });

                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
                else
                {
                    ViewBag.Error = "Error";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error:" + ex.Message;
                return View();
            }
        }

        public ActionResult Preview(string encodedHtml)
        {
            byte[] data = System.Convert.FromBase64String(encodedHtml);
            string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            ViewBag.Html = base64Decoded;
            return View();
        }

        [HttpPost]
        public string GetPageIdentifiers(string lang)
        {
            DapperRepository<PageIdentifier> getPageIdentifiers = new DapperRepository<PageIdentifier>();

            List<PageIdentifier> result = new List<PageIdentifier>();

            if (lang == "tr")
            {
                result = getPageIdentifiers.GetList(@"SELECT distinct CreatedDate,PageIdentifier as IdentifierName,PageTitle FROM Pages (NOLOCK)
where [Language] = @lang
order by CreatedDate desc", new { lang = "de" });
            }
            else if (lang == "de")
            {
                result = getPageIdentifiers.GetList(@"SELECT distinct CreatedDate,PageIdentifier as IdentifierName,PageTitle FROM Pages (NOLOCK)
where [Language] = @lang
order by CreatedDate desc", new { lang = "tr" });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string PassivePage(int pageID)
        {
            try
            {
                DapperRepository<Pages> page = new DapperRepository<Pages>();

                int result = page.Execute(@"update Pages set IsActive = 0 where PageID = @pageID", new { pageID = pageID });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public string ActivatePage(int pageID)
        {
            try
            {
                DapperRepository<Pages> page = new DapperRepository<Pages>();

                int result = page.Execute(@"update Pages set IsActive = 1 where PageID = @pageID", new { pageID = pageID });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}