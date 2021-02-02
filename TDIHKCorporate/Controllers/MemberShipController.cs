using DbAccess.Dapper.Repository;
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
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string lang = cultureInfo.TwoLetterISOLanguageName;

            bool mailResult = false;

            try
            {
                if (ModelState.IsValid)
                {
                    DapperRepository<MemberShipForm> memberRepo = new DapperRepository<MemberShipForm>();
                    memberShipForm.CreatedDate = DateTime.Now;
                    memberShipForm.IPAdress = Request.UserHostAddress;



                    int result = memberRepo.Execute(@"insert into MemberShipForm  ([IPAdress],[IsCorporate],[MemberType],[Name],[Street],[HomePhone],[PostalCode],[City],[Email],[PhoneNumber],[WebSite],[Fax],[ResponsiblePersonel],[PersonelPosition],[CorporationIncome],[CorporationLogoPath],[MemberSuggestion1],[MemberSuggestion2],[MemberSuggestion3],[SuggestionInfo],[SuggestionLocationAndTime],[CreatedDate]) values (@IPAdress,@IsCorporate,@MemberType,@Name,@Street,@HomePhone,@PostalCode,@City,@Email,@PhoneNumber,@WebSite,@Fax,@ResponsiblePersonel,@PersonelPosition,@CorporationIncome,@CorporationLogoPath,@MemberSuggestion1,@MemberSuggestion2,@MemberSuggestion3,@SuggestionInfo,@SuggestionLocationAndTime,@CreatedDate)", memberShipForm);


                    //Mail Gönderme methodu
                    DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                    EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                    {
                        TemplateIdentifier = "StandardMembership",
                        Language = lang
                    });

                    emailTemplate.TemplateHtml =
                        emailTemplate.TemplateHtml
                        .Replace("<##Name##>", memberShipForm.Name)
                        .Replace("<##Street##>", memberShipForm.Street)
                        .Replace("<##Home_Phone##>", memberShipForm.HomePhone)
                        .Replace("<##Postal_Code##>", memberShipForm.PostalCode.ToString())
                        .Replace("<##City##>", memberShipForm.City)
                        .Replace("<##Email##>", memberShipForm.Email)
                        .Replace("<##Telephone##>", memberShipForm.PhoneNumber)
                        .Replace("<##Website##>", memberShipForm.WebSite)
                        .Replace("<##Fax##>", memberShipForm.Fax)
                        .Replace("<##Responsible_Personel##>", memberShipForm.ResponsiblePersonel)
                        .Replace("<##Personel_Position##>", memberShipForm.PersonelPosition)
                        .Replace("<##Member_Packet_Type##>", memberShipForm.CorporationIncome)
                        .Replace("<##MEMBER_1##>", memberShipForm.MemberSuggestion1)
                        .Replace("<##MEMBER_2##>", memberShipForm.MemberSuggestion2)
                        .Replace("<##MEMBER_3##>", memberShipForm.MemberSuggestion3)
                        .Replace("<##Suggestion_Info##>", memberShipForm.SuggestionInfo)
                        .Replace("<##SuggestionLocationAndTime##>", memberShipForm.SuggestionLocationAndTime);


                    EmailTemplates emailTemplateInfo = new EmailTemplates();

                    if (lang == "tr")
                    {
                        emailTemplateInfo = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                        {
                            TemplateIdentifier = "StandardMembershipInfo",
                            Language = lang
                        });
                        emailTemplateInfo.TemplateHtml = emailTemplateInfo.TemplateHtml;
                    }
                    else
                    {
                        emailTemplateInfo = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                        {
                            TemplateIdentifier = "StandardMembershipInfo",
                            Language = lang
                        });
                        emailTemplateInfo.TemplateHtml = emailTemplateInfo.TemplateHtml;
                    }

                    MailSender mailSender = new MailSender("MemberShip");
                    List<string> list = new List<string>();
                    list.Add(memberShipForm.Email);
                    List<Attachment> attachments = new List<Attachment>();



                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\memberShipFiles\\" + memberShipForm.MemberType + "_" + memberShipForm.Name + "_" + DateTime.Now.ToShortDateString() + "_" + Guid.NewGuid() + ".pdf";
                    htmlToPdf.GeneratePdf(emailTemplate.TemplateHtml, null, path);

                    attachments.Add(new Attachment(path));

                    string tuzukPath = "";

                    if (memberShipForm.Email != "mitgliedschaft@td-ihk.de")
                    {
                        if (lang == "tr")
                        {
                            tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Tuzuk_2015_12_07.pdf";
                        }
                        else
                        {
                            tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Satzung_2015_12_07.pdf";
                        }
                        attachments.Add(new Attachment(tuzukPath));
                    }

                    List<string> internalMail = new List<string>();
                    internalMail.Add("mitgliedschaft@td-ihk.de");
                    mailSender.SendMail(emailTemplate, internalMail);

                    mailResult = mailSender.SendMail(emailTemplateInfo, list, attachments);

                }

                return JsonConvert.SerializeObject(mailResult);
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

        [HttpPost]
        public string PremiumMitgliedschaftAntragsformular(MemberShipForm memberShipForm)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string lang = cultureInfo.TwoLetterISOLanguageName;

            bool mailResult = false;

            try
            {
                if (ModelState.IsValid)
                {
                    DapperRepository<MemberShipForm> memberRepo = new DapperRepository<MemberShipForm>();
                    memberShipForm.CreatedDate = DateTime.Now;
                    memberShipForm.IPAdress = Request.UserHostAddress;

                    //Mail Gönderme methodu

                    int result = memberRepo.Execute(@"insert into MemberShipForm  ([IPAdress],[IsCorporate],[MemberType],[Name],[Street],[HomePhone],[PostalCode],[City],[Email],[PhoneNumber],[WebSite],[Fax],[ResponsiblePersonel],[PersonelPosition],[CorporationIncome],[CorporationLogoPath],[MemberSuggestion1],[MemberSuggestion2],[MemberSuggestion3],[SuggestionInfo],[SuggestionLocationAndTime],[CreatedDate]) values (@IPAdress,@IsCorporate,@MemberType,@Name,@Street,@HomePhone,@PostalCode,@City,@Email,@PhoneNumber,@WebSite,@Fax,@ResponsiblePersonel,@PersonelPosition,@CorporationIncome,@CorporationLogoPath,@MemberSuggestion1,@MemberSuggestion2,@MemberSuggestion3,@SuggestionInfo,@SuggestionLocationAndTime,@CreatedDate)", memberShipForm);

                    MailSender mailSender = new MailSender("MemberShip");

                    DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                    EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                    {
                        TemplateIdentifier = "StandardMembership",
                        Language = lang
                    });

                    emailTemplate.TemplateHtml =
                        emailTemplate.TemplateHtml
                        .Replace("<##Name##>", memberShipForm.Name)
                        .Replace("<##Street##>", memberShipForm.Street)
                        .Replace("<##Home_Phone##>", memberShipForm.HomePhone)
                        .Replace("<##Postal_Code##>", memberShipForm.PostalCode.ToString())
                        .Replace("<##City##>", memberShipForm.City)
                        .Replace("<##Email##>", memberShipForm.Email)
                        .Replace("<##Telephone##>", memberShipForm.PhoneNumber)
                        .Replace("<##Website##>", memberShipForm.WebSite)
                        .Replace("<##Fax##>", memberShipForm.Fax)
                        .Replace("<##Responsible_Personel##>", memberShipForm.ResponsiblePersonel)
                        .Replace("<##Personel_Position##>", memberShipForm.PersonelPosition)
                        .Replace("<##Member_Packet_Type##>", memberShipForm.CorporationIncome)
                        .Replace("<##MEMBER_1##>", memberShipForm.MemberSuggestion1)
                        .Replace("<##MEMBER_2##>", memberShipForm.MemberSuggestion2)
                        .Replace("<##MEMBER_3##>", memberShipForm.MemberSuggestion3)
                        .Replace("<##Suggestion_Info##>", memberShipForm.SuggestionInfo)
                        .Replace("<##SuggestionLocationAndTime##>", memberShipForm.SuggestionLocationAndTime);

                    EmailTemplates emailTemplateInfo = new EmailTemplates();
                    List<string> list = new List<string>();
                    list.Add(memberShipForm.Email);
                    List<Attachment> attachments = new List<Attachment>();

                    if (lang == "tr")
                    {
                        emailTemplateInfo = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                        {
                            TemplateIdentifier = "StandardMembershipInfo",
                            Language = lang
                        });

                        emailTemplateInfo.TemplateHtml = emailTemplateInfo.TemplateHtml;

                    }
                    else
                    {
                        emailTemplateInfo = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                        {
                            TemplateIdentifier = "StandardMembershipInfo",
                            Language = lang
                        });

                        emailTemplateInfo.TemplateHtml = emailTemplateInfo.TemplateHtml;
                    }


                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\memberShipFiles\\" + memberShipForm.MemberType + "_" + memberShipForm.Name + "_" + DateTime.Now.ToShortDateString() + "_" + Guid.NewGuid() + ".pdf";
                    htmlToPdf.GeneratePdf(emailTemplateInfo.TemplateHtml, null, path);

                    attachments.Add(new Attachment(path));

                    string tuzukPath = "";

                    if (lang == "tr")
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Tuzuk_2015_12_07.pdf";
                    }
                    else
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Satzung_2015_12_07.pdf";
                    }

                    attachments.Add(new Attachment(tuzukPath));


                    List<string> internalMail = new List<string>();
                    internalMail.Add("mitgliedschaft@td-ihk.de");
                    mailSender.SendMail(emailTemplate, internalMail);

                    mailResult = mailSender.SendMail(emailTemplateInfo, list, attachments);
                }

                return JsonConvert.SerializeObject(mailResult);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(false);
            }
        }

        public ActionResult Mitgliederliste(string search)
        {
            DapperRepository<Members> memberRepo = new DapperRepository<Members>();
            List<Members> members = new List<Members>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                members = memberRepo.GetList(@"select 
                                                case UPPER(SUBSTRING(MemberTitle,1,1))
                                                when 'Ç' then 'C'
                                                when 'İ' then 'I'
                                                when 'Ö' then 'O'
                                                when 'Ş' then 'S'
                                                when 'Ü' then 'U'
                                                else
                                                UPPER(SUBSTRING(MemberTitle,1,1)) 
                                                end as AlphabetStarter,UPPER(SUBSTRING(MemberTitle,1,1)) as RealAlphabetStarter,* from Members (nolock) 
                                                where IsActive = 1 and MemberTitle like '%'+@Search+'%'
                                                order by MemberTitle", new { Search = search });
            }
            else
            {
                members = memberRepo.GetList(@"select 
                                                case UPPER(SUBSTRING(MemberTitle,1,1))
                                                when 'Ç' then 'C'
                                                when 'İ' then 'I'
                                                when 'Ö' then 'O'
                                                when 'Ş' then 'S'
                                                when 'Ü' then 'U'
                                                else
                                                UPPER(SUBSTRING(MemberTitle,1,1))
                                                end as AlphabetStarter,UPPER(SUBSTRING(MemberTitle,1,1)) as RealAlphabetStarter,* from Members (nolock) where IsActive = 1 order by MemberTitle", null);
            }


            return View(members);
        }

    }
}