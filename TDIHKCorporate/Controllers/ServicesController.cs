using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class ServicesController : SiteBaseController
    {
        public ActionResult Jobangebote()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where [Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "JobOffers" });


            return View(pageItem);
        }

        public ActionResult GetJobOffers()
        {
            DapperRepository<JobOffers> job = new DapperRepository<JobOffers>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            List<JobOffers> jobOfferList = job.GetList(@"select
                                                        jo.JobOfferID,
                                                        jo.IsMember,
                                                        jo.MemberID,
                                                        case when jo.IsMember = 1 then mem.MemberLogoPath else jo.LogoPath end as LogoPath,
														case when jo.IsMember = 1 then mem.MemberTitle else jo.CorporationName end as CorporationName,
                                                        jo.Position,
                                                        jo.PositionDescription,
                                                        jo.[Location],
                                                        jo.JobOfferURL,
                                                        jo.CreatedDate,
                                                        jo.CreatedBy,
                                                        jo.UpdatedDate,
                                                        jo.UpdatedBy,
                                                        jo.IsActive
                                                        from JobOffers jo (nolock)
                                                        left join Members mem (nolock)
                                                        on jo.MemberID = mem.MemberID
														where jo.IsActive = 1 and [Language] = @Language
														order by jo.CreatedDate desc", new { Language= name });
            return PartialView("_JobOffers", jobOfferList);
        }
    }
}