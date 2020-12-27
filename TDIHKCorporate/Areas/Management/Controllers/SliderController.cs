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
    public class SliderController : ManagementBaseController
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
                        SliderUrl = createContentViewModel.SliderURL,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 1,
                        IsActive = true
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
                ViewBag.Error = "Error:" + ex.Message;
                return View();
            }
        }

        public ActionResult EditSliderItem(int id)
        {
            DapperRepository<SliderContent> sc = new DapperRepository<SliderContent>();
            SliderContent sliderContent = sc.Get(@"select * from SliderContent (NOLOCK) where SliderContentID = @SliderContentID", new { SliderContentID = id });

            return View(sliderContent);
        }

        [HttpPost]
        public ActionResult EditSliderItem(EditContentViewModel editContentViewModel)
        {
            DapperRepository<SliderContent> scRepo = new DapperRepository<SliderContent>();
            SliderContent sc = scRepo.Get("select * from SliderContent where SliderContentID = @SliderContentID", new { SliderContentID = editContentViewModel.SliderContentID });

            try
            {
                if (ModelState.IsValid)
                {
                    

                    DapperRepository<Sliders> sliderRepo = new DapperRepository<Sliders>();
                    Sliders slider = sliderRepo.Get("select * from Sliders where SliderID = @sliderID", new { sliderID = editContentViewModel.SliderID });

                    SliderContent sliderContent = new SliderContent()
                    {
                        SliderContentID = editContentViewModel.SliderContentID,
                        SliderContentTitle = editContentViewModel.SliderContentTitle,
                        Language = editContentViewModel.SliderContentLanguage,
                        SliderPriority = editContentViewModel.SliderPriority,
                        SliderUrl = editContentViewModel.SliderURL,
                        UpdatedDate = DateTime.Now,
                        UpdatedBy = 1
                    };

                    DapperRepository<SliderContent> sliderContentRepo = new DapperRepository<SliderContent>();
                    string sqlCommand = "";

                    if (editContentViewModel.sliderFile!=null)
                    {
                        string pic = System.IO.Path.GetFileName(editContentViewModel.sliderFile.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Content/MainSite/assets/images"), pic);
                        editContentViewModel.sliderFile.SaveAs(path);
                        sliderContent.ImagePath = "/Content/MainSite/assets/images/" + editContentViewModel.sliderFile.FileName;
                        sqlCommand = @"update SliderContent set SliderContentTitle=@sliderContentTitle,[Language]=@language,ImagePath=@ImagePath,SliderID=@sliderID,SliderUrl=@sliderUrl,SliderPriority=@sliderPriority,UpdatedDate=@updatedDate,UpdatedBy =@updatedBy where SliderContentID = @SliderContentID";
                    }
                    else
                    {
                        sqlCommand = @"update SliderContent set SliderContentTitle=@sliderContentTitle,[Language]=@language,SliderID=@sliderID,SliderUrl=@sliderUrl,SliderPriority=@sliderPriority,UpdatedDate=@updatedDate,UpdatedBy =@updatedBy where SliderContentID = @SliderContentID";
                    }
                    
                    var result = sliderContentRepo.Execute(sqlCommand, sliderContent);

                    ViewBag.Success = "Success";
                    return View(sliderContent);
                }
                else
                {
                    ViewBag.Warning = "Boş bırakılan alanlar var!";
                   
                    return View(sc);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error:" + ex.Message; 
                return View(sc);
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

        public ActionResult SliderItems(int id)
        {
            DapperRepository<SliderContent> slider = new DapperRepository<SliderContent>();
            List<SliderContent> sliderContentList = slider.GetList(@"SELECT  * FROM [IHK].[dbo].[SliderContent] (NOLOCK)
                                                WHERE SliderID = @SliderID 
                                                order by SliderPriority", new { SliderID = id });

            return View(sliderContentList);
        }

        [HttpPost]
        public string UpdateSlider(List<UpdateSliderContent> updateSliderContents)
        {
            try
            {
                if (updateSliderContents != null)
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

        public string GetSliderItems(int sliderID)
        {
            DapperRepository<SliderContentBySliderId> getSliderItemsBySliderId = new DapperRepository<SliderContentBySliderId>();

            List<SliderContentBySliderId> result = getSliderItemsBySliderId.GetList(@"
                                                SELECT  * FROM [IHK].[dbo].[SliderContent] (NOLOCK)
                                                WHERE SliderID = @SliderID
                                                order by SliderPriority", new { SliderID = sliderID });

            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string PassiveSliderItem(int sliderItemId)
        {
            try
            {
                DapperRepository<SliderContent> item = new DapperRepository<SliderContent>();
                int result = item.Execute(@"delete from SliderContent where SliderContentID = @SliderContentID ", new { SliderContentID = sliderItemId });

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        [HttpPost]
        public string ActivateSliderItem(int sliderItemId)
        {
            try
            {
                DapperRepository<SliderContent> item = new DapperRepository<SliderContent>();
                int result = item.Execute(@"update SliderContent set IsActive = 1 where SliderContentID = @SliderContentID ", new { SliderContentID = sliderItemId });

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(false);
            }
        }


    }
}