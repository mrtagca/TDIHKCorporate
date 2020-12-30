using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            bool isMember = Convert.ToBoolean(Request.Form["IsMember"].ToString());
            jobOffers.CreatedDate = DateTime.Now;
            jobOffers.CreatedBy = 1;
            jobOffers.IsActive = true;

            return View();
        }
    }
}