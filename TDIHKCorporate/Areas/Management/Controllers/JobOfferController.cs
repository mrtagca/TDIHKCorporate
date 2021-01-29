using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.Areas.Management.Filters;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    [AuthorizationFilter]
    public class JobOfferController : ManagementBaseController
    {
        // GET: Management/JobOffer
        public ActionResult AddJobOffer()
        {
            return View();
        }

        public string GetMembers()
        {
            DapperRepository<Members> memberRepo = new DapperRepository<Members>();
            List<Members> memberList = memberRepo.GetList(@"select * from Members (nolock) where IsActive = 1 order by MemberTitle", null);
            return JsonConvert.SerializeObject(memberList);
        }

        [HttpPost]
        public ActionResult AddJobOffer(JobOffers jobOffers)
        {
            jobOffers.CreatedDate = DateTime.Now;
            jobOffers.CreatedBy = Convert.ToInt32(Session["UserID"]);
            jobOffers.IsActive = true;

            string path = "/Content/MainSite/assets/images/joboffers/";

            if (!jobOffers.IsMember)
            {
                string filePath = Path.Combine(Server.MapPath(path), jobOffers.file.FileName);
                jobOffers.file.SaveAs(filePath);

                jobOffers.LogoPath = "/Content/MainSite/assets/images/joboffer/" + jobOffers.file.FileName;
            }
            else
            {
                DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                Members members = memberRepo.Get(@"SELECT MemberLogoPath FROM Members (NOLOCK) where MemberID = @MemberID", new { MemberID = jobOffers.MemberID });
            }



            DapperRepository<JobOffers> jobRepo = new DapperRepository<JobOffers>();

            int result = jobRepo.Execute(@"insert into JobOffers
(IsMember,MemberID,LogoPath,Position,PositionDescription,[Location],JobOfferURL,[Language],CreatedDate,CreatedBy,IsActive)
 values (@IsMember,@MemberID,@LogoPath,@Position,@PositionDescription,@Location,@JobOfferURL,@Language,@CreatedDate,@CreatedBy,@IsActive)", jobOffers);

            return View();
        }

        public ActionResult JobOfferList()
        {
            DapperRepository<JobOffers> jobList = new DapperRepository<JobOffers>();
            var result = jobList.GetList(@"select
                                                        jo.JobOfferID,
                                                        jo.IsMember,
                                                        jo.MemberID,
                                                        case when jo.IsMember = 1 then mem.MemberLogoPath else jo.LogoPath end as LogoPath,
														case when jo.IsMember = 1 then mem.MemberTitle else jo.CorporationName end as CorporationName,
                                                        jo.Position,
                                                        jo.PositionDescription,
                                                        jo.[Location],
                                                        jo.CreatedDate,
                                                        jo.CreatedBy,
                                                        jo.UpdatedDate,
                                                        jo.UpdatedBy,
                                                        jo.IsActive
                                                        from JobOffers jo (nolock)
                                                        left join Members mem (nolock)
                                                        on jo.MemberID = mem.MemberID
														order by jo.CreatedDate desc
                                            ", null).ToList();


            return View(result);
        }

        public ActionResult Edit(int id)
        {
            DapperRepository<JobOffers> jobOffer = new DapperRepository<JobOffers>();
            JobOffers jo = jobOffer.Get(@"select
                                                        jo.JobOfferID,
                                                        jo.IsMember,
                                                        jo.MemberID,
                                                        jo.[Language],
                                                        case when jo.IsMember = 1 then mem.MemberLogoPath else jo.LogoPath end as LogoPath,
														case when jo.IsMember = 1 then mem.MemberTitle else jo.CorporationName end as CorporationName,
                                                        jo.Position,
                                                        jo.PositionDescription,
                                                        jo.[Location],
                                                        jo.CreatedDate,
                                                        jo.CreatedBy,
                                                        jo.UpdatedDate,
                                                        jo.UpdatedBy,
                                                        jo.IsActive
                                                        from JobOffers jo (nolock)
                                                        left join Members mem (nolock)
                                                        on jo.MemberID = mem.MemberID
														where jo.JobOfferID = @jobOfferID", new { jobOfferID = id }
            );

            return View(jo);
        }

        public string PassiveJobOffer(int jobOfferID)
        {
            try
            {
                DapperRepository<JobOffers> jobOffer = new DapperRepository<JobOffers>();

                int result = jobOffer.Execute(@"update JobOffers set IsActive = 0,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where JobOfferID = @JobOfferID", new { JobOfferID = jobOfferID, UpdatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(Session["UserID"]) });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public string ActivateJobOffer(int jobOfferID)
        {
            try
            {
                DapperRepository<JobOffers> jobOffer = new DapperRepository<JobOffers>();

                int result = jobOffer.Execute(@"update JobOffers set IsActive = 1,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where JobOfferID = @JobOfferID", new { JobOfferID = jobOfferID, UpdatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(Session["UserID"]) });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        [HttpPost]
        public string EditJobOffer(JobOffers jobOffers)
        {
            try
            {
                jobOffers.UpdatedDate = DateTime.Now;
                jobOffers.UpdatedBy = Convert.ToInt32(Session["UserID"]);

                string path = "/Content/MainSite/assets/images/joboffers/";

                if (!jobOffers.IsMember)
                {
                    string filePath = Path.Combine(Server.MapPath(path), jobOffers.file.FileName);
                    jobOffers.file.SaveAs(filePath);

                    jobOffers.LogoPath = "/Content/MainSite/assets/images/joboffer/" + jobOffers.file.FileName;
                }
                else
                {
                    DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                    Members members = memberRepo.Get(@"SELECT MemberLogoPath FROM Members (NOLOCK) where MemberID = @MemberID", new { MemberID = jobOffers.MemberID });
                }



                DapperRepository<JobOffers> jobRepo = new DapperRepository<JobOffers>();

                int result = jobRepo.Execute(@"update JobOffers set
IsMember=@IsMember,MemberID=@MemberID,LogoPath=@LogoPath,Position=@Position,PositionDescription=@PositionDescription,[Location]=@Location,JobOfferURL=@JobOfferURL,[Language]=@Language,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where JobOfferID = @JobOfferID", jobOffers);

                if (result > 0)
                {
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