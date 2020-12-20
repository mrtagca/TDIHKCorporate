using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class PodcastController : ManagementBaseController
    {
        public ActionResult PodcastList()
        {
            DapperRepository<Podcasts> podcastList = new DapperRepository<Podcasts>();
            var result = podcastList.GetList(@"select * from Podcasts (NOLOCK) order by CreatedDate desc ", null).ToList();


            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            DapperRepository<Podcasts> podcastRepo = new DapperRepository<Podcasts>();
            Podcasts podcasts = podcastRepo.Get(@"select * from Podcasts (NOLOCK) where ID = @ID", new { ID = id });

            return View(podcasts);
        }

        [HttpPost]
        public ActionResult Create(PodcastCreateViewModel podcastCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string sound = System.IO.Path.GetFileName(podcastCreateViewModel.file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/MainSite/assets/files/podcasts/"), sound);

                    podcastCreateViewModel.file.SaveAs(path);

                    Podcasts podcast = new Podcasts()
                    {
                        Language = podcastCreateViewModel.Language,
                        PodcastFilePath = "/Content/MainSite/assets/files/podcasts/" + podcastCreateViewModel.file.FileName,
                        PodcastTitle = podcastCreateViewModel.PodcastTitle,
                        PodcastDescription = podcastCreateViewModel.PodcastDescription,
                        CreatedDate = DateTime.Now,
                        CreatedBy = 1,
                        IsActive = true

                    };

                    DapperRepository<Podcasts> podcastRepo = new DapperRepository<Podcasts>();
                    var result = podcastRepo.Execute(@" insert into Podcasts ([Language],PodcastFilePath,PodcastTitle,PodcastDescription,CreatedDate,CreatedBy,IsActive) values (@Language,@PodcastFilePath,@PodcastTitle,@PodcastDescription,@CreatedDate,@CreatedBy,@IsActive)", podcast);

                    ViewBag.Success = "Success";
                    return View();
                }
                else
                {
                    ViewBag.Success = "Fail";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = "Fail: " + ex.Message;
                return View();
            }
        }

        public string PassivePodcast(int podcastID)
        {
            try
            {
                DapperRepository<Podcasts> podcast = new DapperRepository<Podcasts>();

                int result = podcast.Execute(@"update Podcasts set IsActive = 0 where ID = @ID", new { ID = podcastID });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public string ActivatePodcast(int podcastID)
        {
            try
            {
                DapperRepository<Podcasts> podcast = new DapperRepository<Podcasts>();

                int result = podcast.Execute(@"update Podcasts set IsActive = 1 where ID = @ID", new { ID = podcastID });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        [HttpPost]
        public string EditPodcast(Podcasts podcasts)
        {

            try
            {
                DapperRepository<Podcasts> editPodcast = new DapperRepository<Podcasts>();


                int result = editPodcast.Execute(@"update Podcasts set [Language] = @Language,PodcastFilePath = @PodcastFilePath,PodcastTitle=@PodcastTitle,PodcastDescription=@PodcastDescription
                    where ID = @ID", new
                {
                    ID = podcasts.ID,
                    Language = podcasts.Language,
                    PodcastFilePath = podcasts.PodcastFilePath,
                    PodcastTitle = podcasts.PodcastTitle,
                    PodcastDescription = podcasts.PodcastDescription,
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
    }
}