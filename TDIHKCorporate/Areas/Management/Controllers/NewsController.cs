using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class NewsController : ManagementBaseController
    {
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
    }
}