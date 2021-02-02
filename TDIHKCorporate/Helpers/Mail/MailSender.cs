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

        public bool SendMail(EmailTemplates emailTemplate, List<string> emailList, List<Attachment> attachments = null)
        {
            try
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                string lang = cultureInfo.TwoLetterISOLanguageName;

                DapperRepository<EmailTemplates> emailRepo = new DapperRepository<EmailTemplates>();


                ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress(_mailOptions.CredentialUsername);
                ePosta.Subject = emailTemplate.Subject;
                ePosta.Body = emailTemplate.TemplateHtml;
                ePosta.IsBodyHtml = true;
                ePosta.Priority = MailPriority.High;
                ePosta.BodyEncoding = Encoding.Default;
                ePosta.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                foreach (var email in emailList)
                {
                    ePosta.To.Add(email);
                }

                if (attachments!=null)
                {
                    foreach (var attach in attachments)
                    {
                        ePosta.Attachments.Add(attach);
                    }
                }

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

    }

}