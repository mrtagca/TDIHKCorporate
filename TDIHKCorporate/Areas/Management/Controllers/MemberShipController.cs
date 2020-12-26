﻿using DbAccess.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Areas.Management.Controllers
{
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

                    members.MemberLogoPath = path;
                    members.CreatedDate = DateTime.Now;
                    members.CreatedBy = 1;
                    members.IsActive = true;

                    DapperRepository<Members> memberRepo = new DapperRepository<Members>();
                    var result = memberRepo.Execute(@"insert into  Members (MemberLogoPath,MemberTitle,MemberAddress,MemberWebSite,MemberPhone1,MemberPhone2,MemberEmailAddress,CreatedDate,CreatedBy) 
values (@MemberLogoPath,@MemberTitle,@MemberAddress,@MemberWebSite,@MemberPhone1,@MemberPhone2,@MemberEmailAddress,@CreatedDate,@CreatedBy)", members);

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
    }
}