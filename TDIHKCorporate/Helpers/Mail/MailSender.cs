using DbAccess.Dapper.Repository;
using PdfSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using TDIHKCorporate.Types;

namespace TDIHKCorporate.Helpers.Mail
{
    public class MailSender
    {
        MailOptions _mailOptions;
        public MailSender(string mailType)
        {
            _mailOptions = new MailOptions();

            DapperRepository<MailOptions> mailOptionsRepo = new DapperRepository<MailOptions>();
            _mailOptions = mailOptionsRepo.Get(@"SELECT * FROM MailOptions (NOLOCK) WHERE MailType = @MailType", new
            {
                MailType = mailType
            });
        }

        public bool SendMail(MemberShipForm memberShipForm, EmailTemplates emailTemplates, bool isConvertPDF)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
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

               

                ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress(_mailOptions.CredentialUsername);
                ePosta.To.Add(memberShipForm.Email);
                ePosta.Subject = emailTemplates.Subject;
                ePosta.Body = emailTemplateInfo.TemplateHtml;
                ePosta.IsBodyHtml = true;
                ePosta.Priority = MailPriority.High;
                ePosta.BodyEncoding = Encoding.Default;

                if (isConvertPDF)
                {
                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\memberShipFiles\\" + memberShipForm.MemberType + "_" + memberShipForm.Name + "_" + DateTime.Now.ToShortDateString()+"_"+Guid.NewGuid()+ ".pdf";
                    htmlToPdf.GeneratePdf(emailTemplates.TemplateHtml, null, path);

                    string tuzukPath = "";

                    if (lang=="tr")
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Tuzuk_2015_12_07.pdf";
                    }
                    else
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Satzung_2015_12_07.pdf";
                    }

                    ePosta.Attachments.Add(new Attachment(path));
                    ePosta.Attachments.Add(new Attachment(tuzukPath));

                }

                ePosta.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailOptions.SmtpServer;
                smtp.Port = _mailOptions.SmtpPort;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_mailOptions.CredentialUsername, _mailOptions.CredentialPassword);
                smtp.EnableSsl = _mailOptions.EnableSsl;
                smtp.Timeout = 10000;

                try
                {
                    smtp.Send(ePosta);
                    return true;

                }
                catch (SmtpException ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendMail(EmailTemplates emailTemplates, List<string> emailList)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress(_mailOptions.CredentialUsername);

                foreach (var item in emailList)
                {
                    ePosta.To.Add(item); 
                }
                ePosta.Subject = emailTemplates.Subject;
                ePosta.Body = emailTemplates.TemplateHtml;
                ePosta.IsBodyHtml = true;
                ePosta.Priority = MailPriority.High;
                ePosta.BodyEncoding = Encoding.Default;

                ePosta.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailOptions.SmtpServer;
                smtp.Port = _mailOptions.SmtpPort;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_mailOptions.CredentialUsername, _mailOptions.CredentialPassword);
                smtp.EnableSsl = _mailOptions.EnableSsl;
                smtp.Timeout = 10000;

                try
                {
                    smtp.Send(ePosta);
                    return true;

                }
                catch (SmtpException ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string SendMailTest(MemberShipForm memberShipForm, EmailTemplates emailTemplates, bool isConvertPDF)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress(_mailOptions.CredentialUsername);
                ePosta.To.Add(memberShipForm.Email);
                ePosta.Subject = emailTemplates.Subject;
                ePosta.Body = emailTemplates.TemplateHtml;
                ePosta.IsBodyHtml = true;
                ePosta.Priority = MailPriority.High;
                ePosta.BodyEncoding = Encoding.Default;

                if (isConvertPDF)
                {
                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\memberShipFiles\\" + memberShipForm.MemberType + "_" + memberShipForm.Name + "_" + DateTime.Now.ToShortDateString() + "_" + Guid.NewGuid() + ".pdf";
                    htmlToPdf.GeneratePdf(ePosta.Body, null, path);

                    string tuzukPath = "";

                    if (lang == "tr")
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Tuzuk_2015_12_07.pdf";
                    }
                    else
                    {
                        tuzukPath = AppDomain.CurrentDomain.BaseDirectory + "Content\\MainSite\\assets\\files\\Satzung_2015_12_07.pdf";
                    }

                    ePosta.Attachments.Add(new Attachment(path));
                    ePosta.Attachments.Add(new Attachment(tuzukPath));

                }

                ePosta.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailOptions.SmtpServer;
                smtp.Port = _mailOptions.SmtpPort;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_mailOptions.CredentialUsername, _mailOptions.CredentialPassword);
                smtp.EnableSsl = _mailOptions.EnableSsl;
                smtp.Timeout = 10000;

                try
                {
                    smtp.Send(ePosta);
                    return "Success";

                }
                catch (SmtpException ex)
                {
                    return ex.Message;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool SendMailStandardMembership(MemberShipForm memberShipForm, string language)
        {

            try
            {
                DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                {
                    TemplateIdentifier = "StandardMembership",
                    Language = language
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



                return SendMail(memberShipForm, emailTemplate, true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string SendMailStandardMembershipTest(MemberShipForm memberShipForm, string language)
        {

            try
            {
                DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                {
                    TemplateIdentifier = "StandardMembership",
                    Language = language
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



                return SendMailTest(memberShipForm, emailTemplate, true);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool SendMailPremiumMembership(MemberShipForm memberShipForm, string language)
        {

            try
            {
                DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();
                EmailTemplates emailTemplate = emailRepo.Get(@"select * from EmailTemplates (nolock) where [Language] = @Language and TemplateIdentifier = @templateIdentifier", new
                {
                    TemplateIdentifier = "StandardMembership",
                    Language = language
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



                return SendMail(memberShipForm, emailTemplate, true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}