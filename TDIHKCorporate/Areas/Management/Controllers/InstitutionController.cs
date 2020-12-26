using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Types.ViewTypes;

namespace TDIHKCorporate.Areas.Management.Controllers
{
    public class InstitutionController : ManagementBaseController
    {
        // GET: Management/Institution
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Institution institution)
        {

            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();
                institution.CreatedDate = DateTime.Now;
                institution.CreatedBy = 1;
                institution.IsActive = true;

                var result = inst.Execute(@"insert into Institutions ([Language],InstitutionTitle,InstitutionDescription,InstitutionDate,CreatedDate,CreatedBy,IsActive) values (@language,@InstitutionTitle,@InstitutionDescription,@InstitutionDate,@createdDate,@createdBy,@IsActive)", institution);

                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
                else
                {
                    ViewBag.Error = "Fail";

                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail: " + ex.Message;
                return View();
            }


        }

        public ActionResult InstitutionList()
        {
            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();

                List<Institution> institutions = inst.GetList(@"select * from Institutions (NOLOCK) order by InstitutionDate desc", null);

                return View(institutions);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();

                Institution institutions = inst.Get(@"select * from Institutions (NOLOCK) where ID=@id", new { id = id });

                return View(institutions);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public string Edit(Institution institution)
        {
            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();
                institution.UpdatedDate = DateTime.Now;
                institution.UpdatedBy = 1; 

                var result = inst.Execute(@"update Institutions set Language = @Language,InstitutionTitle=@InstitutionTitle,InstitutionDescription=@InstitutionDescription,InstitutionDate=@InstitutionDate,UpdatedDate=@updatedDate,UpdatedBy=@updatedBy where ID = @ID", institution);
                 
                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail: " + ex.Message;
                return JsonConvert.SerializeObject(false);
            }


        }

        [HttpPost]
        public string Delete(int id)
        {
            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();

                var result = inst.Execute(@"update Institutions set IsActive = 0 
                where ID = @ID", new { ID = id });

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail: " + ex.Message;
                return JsonConvert.SerializeObject(false);
            }


        }

        [HttpPost]
        public string Activate(int id)
        {
            try
            {
                DapperRepository<Institution> inst = new DapperRepository<Institution>();

                var result = inst.Execute(@"update Institutions set IsActive = 1 
                where ID = @ID", new { ID = id });

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Fail: " + ex.Message;
                return JsonConvert.SerializeObject(false);
            }


        }
    }
}