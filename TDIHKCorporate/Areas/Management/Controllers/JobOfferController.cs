using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
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
            jobOffers.CreatedBy = 1;
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
                jobOffers.LogoPath = members.MemberLogoPath;
            }

            

            DapperRepository<JobOffers> jobRepo = new DapperRepository<JobOffers>();

            int result = jobRepo.Execute(@"insert into JobOffers
(IsMember,MemberID,LogoPath,Position,PositionDescription,[Location],CreatedDate,CreatedBy,IsActive)
 values (@IsMember,@MemberID,@LogoPath,@Position,@PositionDescription,@Location,@CreatedDate,@CreatedBy,@IsActive)", jobOffers);

            return View();
        }
    }
}