﻿using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Models.Language;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class HomeController : SiteBaseController
    {
        public ActionResult ChangeLanguage(string language)
        {

            new SiteLanguage().SetLanguage(language);

            string url = Request.UrlReferrer.ToString();

            if (url.Contains("seiten") || url.Contains("sayfalar"))
            {
                if (language == "tr") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                    Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/sayfalar/" + page.PageSeoLink + "");
                }

                if (language == "de") //change tr
                {
                    string seo = url.Substring(url.LastIndexOf("/") + 1);

                    DapperRepository<Pages> pageRepo = new DapperRepository<Pages>();
                    Pages page = pageRepo.Get(@"SELECT * FROM [IHK].[dbo].[Pages] (NOLOCK)
where [Language] = @lang and PageIdentifier in (SELECT top 1 PageIdentifier FROM Pages (NOLOCK) where PageSeoLink = @seoUrl) ", new { lang = language, seoUrl = seo });



                    return Redirect("https://" + Request.UrlReferrer.Authority + "/" + language + "/seiten/" + page.PageSeoLink + "");
                }
            }

            return Redirect("");
        }

        //public ActionResult ChangeLanguage(string language)
        //{

        //    new SiteLanguage().SetLanguage(language);
        //    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

        //    string url = Request.UrlReferrer.ToString();


        //    return RedirectToAction("Index", "Home");

        //}

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult MenuList()
        {
            DapperRepository<MenuItems> getPageCategories = new DapperRepository<MenuItems>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<MenuItems> menuItems = getPageCategories.GetList(@"select mi.*,p.PageSeoLink from MenuItems mi (NOLOCK)
                                        inner join Menus m (NOLOCK)
                                        on mi.MenuID = m.ID
                                        left join Pages p (NOLOCK)
                                        on mi.PageID = p.PageID

                                        where m.ID = 1 and mi.[Language] = @lang", new { lang = name }).OrderBy(x => x.MenuItemPriority).ToList();


            return PartialView("_PartialInvestData", menuItems);
        }

        public ActionResult ShowHomePageSlider()
        {
            DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string name = cultureInfo.TwoLetterISOLanguageName;

            List<SliderContent> sliderItems = sliderContent.GetList(@"SELECT sc.* FROM SliderContent sc (NOLOCK) 
                                                                        inner join Sliders sl (NOLOCK)
                                                                        on sc.SliderID = sl.SliderID

                                                                        where sl.SliderName ='Home Slider' and sl.[Language] = @lang and sc.[Language] = @lang
                                                                        order by SliderPriority", new { lang = name });


            return PartialView("_PartialHomePageSlider", sliderItems);
        }
    }
}