using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class NewsController : ManagementBaseController
    {
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            DapperRepository<News> getNews = new DapperRepository<News>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            News news = getNews.Get("select * from News where  NewsID = @NewsId", new { lang = name, NewsId = id });

            return View(news);
        }
        public ActionResult AddNewsCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewsCategory(NewsCategories newsCategories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DapperRepository<NewsCategories> newsCategory = new DapperRepository<NewsCategories>();

                    NewsCategories isThereCategory = newsCategory.Get(@"SELECT  * FROM NewsCategories (NOLOCK)
where [Language] = @lang and NewsCategoryName = @NewsCategoryName", new { lang = newsCategories.Language, NewsCategoryName = newsCategories.NewsCategoryName });

                    if (isThereCategory == null)
                    {
                        int result = newsCategory.Execute(@"insert into NewsCategories ([NewsCategoryName],[Language],[CreatedDate],[CreatedBy],[IsActive]) 
values (@NewsCategoryName,@Language,@CreatedDate,@CreatedBy,@IsActive)", new { NewsCategoryName = newsCategories.NewsCategoryName, language = newsCategories.Language, createdDate = DateTime.Now, createdBy = 1, IsActive = true });

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
                    else
                    {
                        ViewBag.Error = newsCategories.Language + " dilinde "+ newsCategories.NewsCategoryName + " adında bir haber kategorisi var!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Boş bırakılan alanlar var!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error:" + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public string GetNewsCategories(string Language)
        {
            DapperRepository<NewsCategories> getNewsCategories = new DapperRepository<NewsCategories>();

            var result = getNewsCategories.GetList(@"SELECT * FROM NewsCategories (NOLOCK) WHERE [Language] = @lang", new { lang = Language }).OrderBy(x => x.NewsCategoryName);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        [HttpPost]
        public string AddNews(News news)
        {

            try
            {
                string pageIdentifier = "";

                if (string.IsNullOrWhiteSpace(news.NewsIdentifier))
                {
                    pageIdentifier = Guid.NewGuid().ToString().ToUpper();
                }
                else
                {
                    pageIdentifier = news.NewsIdentifier;
                }
                DapperRepository<News> addNews = new DapperRepository<News>();

                string TimezoneId = System.Configuration.ConfigurationManager.AppSettings[System.Threading.Thread.CurrentThread.CurrentCulture.Name];
                news.CreatedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimezoneId);
                news.CreatedBy = 1;
                news.IsActive = true;

                int result = addNews.Execute(@"INSERT INTO News ([NewsCategoryID],[NewsIdentifier],[NewsImagePath],[NewsThumbnailPath],[NewsTitle],[NewsContent],[NewsSeoLink],[NewsSeoKeywords],[Language],[CreatedDate],[CreatedBy],[IsActive]) values(@NewsCategoryID,@NewsIdentifier, @NewsImagePath, @NewsThumbnailPath, @NewsTitle, @NewsContent, @NewsSeoLink, @NewsSeoKeywords, @Language, @CreatedDate, @CreatedBy,@IsActive)", new
                {

                    NewsCategoryID = news.NewsCategoryID,
                    NewsIdentifier = pageIdentifier,
                    NewsImagePath = news.NewsImagePath,
                    NewsThumbnailPath = news.NewsThumbnailPath,
                    NewsTitle = news.NewsTitle,
                    NewsContent = news.NewsContent,
                    NewsSeoLink = news.NewsSeoLink,
                    NewsSeoKeywords = news.NewsSeoKeywords,
                    Language = news.Language,
                    CreatedDate = news.CreatedDate,
                    CreatedBy = news.CreatedBy,
                    IsActive = news.IsActive

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

        [HttpPost]
        public string GetNewsIdentifiers(string lang)
        {
            DapperRepository<NewsIdentifier> getNewsIDentifiers = new DapperRepository<NewsIdentifier>();

            List<NewsIdentifier> result = new List<NewsIdentifier>();

            if (lang == "tr")
            {
                result = getNewsIDentifiers.GetList(@"SELECT distinct CreatedDate,NewsIdentifier as IdentifierName,NewsTitle FROM News (NOLOCK)
where [Language] = @lang
order by CreatedDate desc", new { lang = "de" });
            }
            else if (lang == "de")
            {
                result = getNewsIDentifiers.GetList(@"SELECT distinct CreatedDate,NewsIdentifier as IdentifierName,NewsTitle FROM News (NOLOCK)
where [Language] = @lang
order by CreatedDate desc", new { lang = "tr" });
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);

        }

        public string GetNews(string language)
        {
            try
            {
                DapperRepository<News> news = new DapperRepository<News>();
                List<News> newsList = news.GetList(@"select * from News (NOLOCK) 
                                    where [Language] = @lang", new { lang = language });

                return JsonConvert.SerializeObject(newsList);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [HttpPost]
        public string EditNews(News news)
        {

            try
            {
                DapperRepository<News> addNews = new DapperRepository<News>();


                int result = addNews.Execute(@"update News set NewsImagePath=@NewsImagePath,NewsCategoryID = @NewsCategoryID,NewsTitle=@NewsTitle,NewsContent=@NewsContent,NewsSeoLink=@NewsSeoLink,NewsSeoKeywords=@NewsSeoKeywords,[Language]=@language,UpdatedDate=@updatedDate,UpdatedBy=@UpdatedBy,IsActive=@IsActive
                                        where NewsID = @NewsID", new
                {
                    NewsID = news.NewsID,
                    NewsCategoryID = news.NewsCategoryID,
                    NewsImagePath = news.NewsImagePath,
                    NewsThumbnailPath = news.NewsThumbnailPath,
                    NewsTitle = news.NewsTitle,
                    NewsContent = news.NewsContent,
                    NewsSeoLink = news.NewsSeoLink,
                    NewsSeoKeywords = news.NewsSeoKeywords,
                    Language = news.Language,
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

        public ActionResult NewsList()
        {
            DapperRepository<NewsItem> newsList = new DapperRepository<NewsItem>();
            var result = newsList.GetList(@"SELECT  
                                            nw.NewsID,
											nw.NewsIdentifier,
                                            nw.NewsTitle,
											nw.[Language] as NewsLanguage,
                                            nw.NewsSeoLink,
                                            convert(nvarchar,nw.CreatedDate,120) as CreatedDate,
                                            case when (nw.CreatedBy = usr.UserID) then usr.Username else null end as Creator,
                                            convert(nvarchar,nw.UpdatedDate,120) as UpdatedDate,
                                            case when (nw.UpdatedBy = usr.UserID) then usr.Username else null end as Updater

                                            FROM News nw (NOLOCK)
                                            left join NewsCategories nc (NOLOCK)
                                            on nw.NewsCategoryID = nc.ID
                                            left join Users usr
                                            on nw.CreatedBy = usr.UserID or nw.UpdatedBy = usr.UserID
                                            ", null).OrderBy(x => x.NewsTitle).ToList();


            return View(result);
        }
    }
}