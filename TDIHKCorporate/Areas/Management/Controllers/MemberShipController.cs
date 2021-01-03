using DbAccess.Dapper.Repository;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.Areas.Management.Filters;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    [AuthorizationFilter]
    public class MemberShipController : ManagementBaseController
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Members members)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string member = System.IO.Path.GetFileName(members.file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/MainSite/assets/images/members/"), member);

                    members.file.SaveAs(path);

                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 300,
                        Height = 150
                    };
                    ImageBuilder.Current.Build(path, path, resizeSetting);

                    members.MemberLogoPath = "/Content/MainSite/assets/images/members/" + members.file.FileName;
                    members.CreatedDate = DateTime.Now;
                    members.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    members.IsActive = true;

                    DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                    var result = memberRepo.Execute(@"insert into  Members (MemberLogoPath,MemberTitle,MemberAddress,MemberWebSite,MemberPhone1,MemberPhone2,MemberEmailAddress,CreatedDate,CreatedBy,IsActive) 
values (@MemberLogoPath,@MemberTitle,@MemberAddress,@MemberWebSite,@MemberPhone1,@MemberPhone2,@MemberEmailAddress,@CreatedDate,@CreatedBy,@IsActive)", members);

                    ViewBag.Success = "Success";
                    return View();
                }
                else
                {
                    ViewBag.Success = "Please check the values!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Success = "Fail: " + ex.Message;
                return View();
            }
        }

        public ActionResult MemberList()
        {
            DapperRepository<Members> memberList = new DapperRepository<Members>();
            var result = memberList.GetList(@"select * from Members (nolock) order by MemberTitle", null).ToList();


            return View(result);
        }

        public ActionResult Edit(int id)
        {
            DapperRepository<Members> getMembers = new DapperRepository<Members>();

            Members member = getMembers.Get("select  * from Members (nolock) where MemberID = @MemberID", new { MemberID = id });

            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Members members)
        {

            try
            {

                string sql = "";

                members.UpdatedDate = DateTime.Now;
                members.UpdatedBy = Convert.ToInt32(Session["UserID"]);


                if (members.file != null)
                {
                    string member = System.IO.Path.GetFileName(members.file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/MainSite/assets/images/members/"), member);

                    members.file.SaveAs(path);

                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Width = 300,
                        Height = 150
                    };
                    ImageBuilder.Current.Build(path, path, resizeSetting);

                    members.MemberLogoPath = "/Content/MainSite/assets/images/members/" + members.file.FileName;

                    sql = @"update Members set MemberLogoPath=@MemberLogoPath,MemberTitle=@MemberTitle,MemberAddress=@MemberAddress,MemberWebSite=@MemberWebSite,MemberPhone1=@MemberPhone1,MemberPhone2=@MemberPhone2,MemberEmailAddress=@MemberEmailAddress,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy
where MemberID = @MemberID";
                }
                else
                {
                    sql = @"update Members set MemberTitle=@MemberTitle,MemberAddress=@MemberAddress,MemberWebSite=@MemberWebSite,MemberPhone1=@MemberPhone1,MemberPhone2=@MemberPhone2,MemberEmailAddress=@MemberEmailAddress,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy
where MemberID = @MemberID";
                }


                DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                var result = memberRepo.Execute(sql, members);

                ViewBag.Success = "Success";
                return View(members);

            }
            catch (Exception ex)
            {

                DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                Members result = memberRepo.Get("select * from Members (nolock) where MemberID = @MemberID", new { MemberID = members.MemberID });

                ViewBag.Error = "Fail: " + ex.Message;

                return View(result);
            }
        }

        public string PassiveMember(int memberID)
        {
            try
            {
                DapperRepository<Members> member = new DapperRepository<Members>();

                int result = member.Execute(@"update Members set IsActive = 0,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where MemberID = @MemberID", new { MemberID = memberID, UpdatedDate = DateTime.Now, UpdatedBy = 1 });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public string ActivateMember(int memberID)
        {
            try
            {
                DapperRepository<Members> member = new DapperRepository<Members>();

                int result = member.Execute(@"update Members set IsActive = 1,UpdatedDate=@UpdatedDate,UpdatedBy=@UpdatedBy where MemberID = @MemberID", new { MemberID = memberID, UpdatedDate = DateTime.Now, UpdatedBy = 1 });


                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }
    }
}