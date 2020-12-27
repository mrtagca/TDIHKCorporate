﻿using DbAccess.Dapper.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TDIHKCorporate.BaseControllers.MultiLanguage;
using TDIHKCorporate.Helpers.Mail;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Controllers
{
    public class MemberShipController : SiteBaseController
    {
        public ActionResult Index()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "MemberShip" });

            return View(pageItem);
        }


        public ActionResult StandardMitgliedschaft()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "StandartMemberShip" });


            return View(pageItem);
        }

        public ActionResult PremiumMitgliedschaft()
        {
            DapperRepository<Pages> page = new DapperRepository<Pages>();

            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

            string name = cultureInfo.TwoLetterISOLanguageName;

            Pages pageItem = page.Get(@"SELECT * FROM Pages (NOLOCK)
                                            where[Language] = @language and PageIdentifier = @pageIdentifier", new { language = name, pageIdentifier = "PremiumMemberShip" });


            return View(pageItem);
        }

        public ActionResult StandardMitgliedschaftAntragsformular()
        {
            return View();
        }

        [HttpPost]
        public string StandardMitgliedschaftAntragsformular(MemberShipForm memberShipForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DapperRepository<MemberShipForm> memberRepo = new DapperRepository<MemberShipForm>();
                    memberShipForm.CreatedDate = DateTime.Now;
                    memberShipForm.IPAdress = Request.UserHostAddress;

                    //Mail Gönderme methodu

                    int result = memberRepo.Execute(@"insert into MemberShipForm  ([IPAdress],[IsCorporate],[MemberType],[Name],[Street],[HomePhone],[PostalCode],[City],[Email],[PhoneNumber],[WebSite],[Fax],[ResponsiblePersonel],[PersonelPosition],[CorporationIncome],[CorporationLogoPath],[MemberSuggestion1],[MemberSuggestion2],[MemberSuggestion3],[SuggestionInfo],[SuggestionLocationAndTime],[CreatedDate]) values (@IPAdress,@IsCorporate,@MemberType,@Name,@Street,@HomePhone,@PostalCode,@City,@Email,@PhoneNumber,@WebSite,@Fax,@ResponsiblePersonel,@PersonelPosition,@CorporationIncome,@CorporationLogoPath,@MemberSuggestion1,@MemberSuggestion2,@MemberSuggestion3,@SuggestionInfo,@SuggestionLocationAndTime,@CreatedDate)", memberShipForm);


                    CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                    string lang = cultureInfo.TwoLetterISOLanguageName;

                    MailSender mailSender = new MailSender("MemberShip");
                    mailSender.SendMailStandardMembership(memberShipForm, lang);

                }

                return JsonConvert.SerializeObject(true);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }


        public ActionResult PremiumMitgliedschaftAntragsformular()
        {
            return View();
        }

        //public ActionResult Mitgliederliste()
        //{
        //    DapperRepository<Members> memberRepo = new DapperRepository<Members>();
        //    List<Members> members = memberRepo.GetList(@"select 
        //                                                case SUBSTRING(MemberTitle,1,1) 
        //                                                when 'Ç' then 'C'
        //                                                when 'İ' then 'I'
        //                                                when 'Ö' then 'O'
        //                                                when 'Ş' then 'S'
        //                                                when 'Ü' then 'U'
        //                                                else
        //                                                SUBSTRING(MemberTitle,1,1) 
        //                                                end as AlphabetStarter,SUBSTRING(MemberTitle,1,1) as RealAlphabetStarter,* from Members (nolock) where IsActive = 1 order by AlphabetStarter", null);
        //    return View(members);
        //}

        public ActionResult Mitgliederliste(string search)
        {
            DapperRepository<Members> memberRepo = new DapperRepository<Members>();
            List<Members> members = new List<Members>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                memberRepo.GetList(@"select 
                                    case SUBSTRING(MemberTitle,1,1) 
                                    when 'Ç' then 'C'
                                    when 'İ' then 'I'
                                    when 'Ö' then 'O'
                                    when 'Ş' then 'S'
                                    when 'Ü' then 'U'
                                    else
                                    SUBSTRING(MemberTitle,1,1) 
                                    end as AlphabetStarter,SUBSTRING(MemberTitle,1,1) as RealAlphabetStarter,* from Members (nolock) 
                                    where IsActive = 1 and MemberTitle like '%'+@Search+'%'
                                    order by AlphabetStarter", new { Search = search });
            }
            else
            {
                memberRepo.GetList(@"select 
                                                        case SUBSTRING(MemberTitle,1,1) 
                                                        when 'Ç' then 'C'
                                                        when 'İ' then 'I'
                                                        when 'Ö' then 'O'
                                                        when 'Ş' then 'S'
                                                        when 'Ü' then 'U'
                                                        else
                                                        SUBSTRING(MemberTitle,1,1) 
                                                        end as AlphabetStarter,SUBSTRING(MemberTitle,1,1) as RealAlphabetStarter,* from Members (nolock) where IsActive = 1 order by AlphabetStarter", null);
            }


            return View(members);
        }

    }
}