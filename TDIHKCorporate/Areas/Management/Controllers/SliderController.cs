using DbAccess.Dapper.Repository;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class SliderController : SiteBaseController
    {
        public ActionResult SliderList()
        {
            DapperRepository<Sliders> slider = new DapperRepository<Sliders>();
            List<Sliders> sliderList = slider.GetList(@"SELECT * FROM Sliders (NOLOCK) order by SliderName", null);

            return View(sliderList);
        }

        public string GetSliderList()
        {
            DapperRepository<Sliders> slider = new DapperRepository<Sliders>();
            List<Sliders> sliderList = slider.GetList(@"SELECT * FROM Sliders (NOLOCK) order by SliderName", null);

            return JsonConvert.SerializeObject(sliderList);
        }

        public ActionResult CreateContent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContent(CreateContentViewModel createContentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string pic = System.IO.Path.GetFileName(createContentViewModel.sliderFile.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/MainSite/assets/images"), pic);

                    DapperRepository<Sliders> sliderRepo = new DapperRepository<Sliders>();
                    Sliders slider = sliderRepo.Get("select * from Sliders where SliderID = @sliderID", new { sliderID = createContentViewModel.SliderID });

                    SliderContent sliderContent = new SliderContent()
                    {
                        SliderContentTitle = createContentViewModel.SliderContentTitle,
                        Language = createContentViewModel.SliderContentLanguage,
                        ImagePath = "/Content/MainSite/assets/images/" + createContentViewModel.sliderFile.FileName,
                        SliderID = slider.SliderID,
                        SliderPriority = createContentViewModel.SliderPriority,
                        SliderUrl=createContentViewModel.SliderURL,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 1
                    };

                    createContentViewModel.sliderFile.SaveAs(path);
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = slider.ImageWidth,
                        Height = slider.ImageHeight
                    };
                    ImageBuilder.Current.Build(path, path, resizeSetting);

                    DapperRepository<SliderContent> sliderContentRepo = new DapperRepository<SliderContent>();
                    var result = sliderContentRepo.Execute(@"insert into SliderContent (SliderContentTitle,[Language],ImagePath,SliderID,SliderUrl,SliderPriority,CreatedDate,CreatedBy)
  values (@sliderContentTitle,@language,@ImagePath,@sliderID,@sliderUrl,@sliderPriority,@createdDate,@createdBy)", sliderContent);

                    ViewBag.Success = "Success";
                    return View(); 
                }
                else
                {
                    ViewBag.Warning = "Boş bırakılan alanlar var!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error:"+ex.Message;
                return View();
            }
        }

        public ActionResult EditSlider(int id)
        {

            DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();
            List<SliderContent> sliderContentList = sliderContent.GetList(@"SELECT sc.* FROM SliderContent sc (NOLOCK)
                                                        inner join Sliders sl (NOLOCK)
                                                        on sc.SliderID = sl.SliderID
                                                        where sl.SliderID = @sliderID
														order by SliderPriority", new { sliderID = id });



            return View(sliderContentList);
        }

        [HttpPost]
        public string UpdateSlider(List<UpdateSliderContent> updateSliderContents)
        {
            try
            {
                if (updateSliderContents!=null)
                {
                    DapperRepository<SliderContent> sliderContent = new DapperRepository<SliderContent>();

                    foreach (var item in updateSliderContents)
                    {
                        int result = sliderContent.Execute(@"update SliderContent set SliderPriority = @sliderPriority where SliderContentID = @sliderContentID", new { sliderContentID = item.SliderContentID, sliderPriority = item.SliderPriority });
                    }

                    return JsonConvert.SerializeObject(true); 
                }
                else
                {
                    return JsonConvert.SerializeObject(false);
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}