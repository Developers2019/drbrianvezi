using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace drbrianvezi_cms.Service.RDLC_Management
{
    public class Email
    {
        private readonly string _smtpNameServer = ConfigurationManager.AppSettings["smtp"].ToString();
        private readonly string _smtpServerPort = ConfigurationManager.AppSettings["port"].ToString();
        private readonly string _mailFrom = ConfigurationManager.AppSettings["username"].ToString();
        private readonly string _mailFromUserName = ConfigurationManager.AppSettings["username"].ToString();
        private readonly string _mailFromPassword = ConfigurationManager.AppSettings["password"].ToString();

        public class EmailAttachment
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public Stream Stream { get; set; }
        }

        public string SendEmail(string mailTo, string subject, string body, string mailToBcc = null, List<EmailAttachment> emailAttachmentList = null)
        {
            string status;
            try
            {
                MailMessage mail = new MailMessage {From = new MailAddress(_mailFrom)};


                mail.To.Add(mailTo);
                if (!string.IsNullOrEmpty(mailToBcc))
                {
                    mail.Bcc.Add(mailToBcc);
                }

                mail.Subject = subject;

                mail.IsBodyHtml = true;

                mail.Body = body;

                if (emailAttachmentList != null)
                {
                    if (emailAttachmentList.Count != 0)
                    {
                        foreach (EmailAttachment e in emailAttachmentList)
                        {
                            // Create a new attachment and put the PDF report into it.
                            e.Stream.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att = new Attachment(e.Stream, e.Name, e.Type);

                            // Add the PDF report to it.
                            mail.Attachments.Add(att);
                        }
                    }
                }
                SmtpClient smtpServer = new SmtpClient(_smtpNameServer)
                {
                    EnableSsl = false,
                    Port = Convert.ToInt32(_smtpServerPort),
                    Credentials = new System.Net.NetworkCredential(_mailFromUserName, _mailFromPassword)
                };
                smtpServer.Send(mail);

                status = "Sent";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return status;
        }


        public string SendEmailMultiAttachment(string mailTo, string subject, string body, string mailToBcc = null, List<string> attachmentNames = null, List<MemoryStream> attachmentPdfs = null)
        {
            string status;
            try
            {
                MailMessage mail = new MailMessage();


                mail.From = new MailAddress(_mailFrom);
                mail.To.Add(mailTo);
                mail.Bcc.Add(mailToBcc);
                mail.Subject = subject;

                mail.IsBodyHtml = true;

                mail.Body = body;

                if (attachmentPdfs != null)
                {
                    if (attachmentPdfs.Count > 0)
                    {
                        for (int i = 0; i < attachmentPdfs.Count; i++)
                        {
                            var attachmentPdf = attachmentPdfs[i];

                            // Create a new attachment and put the PDF report into it.
                            attachmentPdf.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att = new Attachment(attachmentPdf, attachmentNames[i], "application/pdf");

                            // Add the PDF report to it.
                            mail.Attachments.Add(att);
                        }
                    }
                }
                SmtpClient smtpServer = new SmtpClient(_smtpNameServer);
                smtpServer.EnableSsl = false;
                smtpServer.Port = Convert.ToInt32(_smtpServerPort);
                smtpServer.Credentials = new System.Net.NetworkCredential(_mailFromUserName, _mailFromPassword);
                smtpServer.Send(mail);

                status = "Sent";
            }
            catch (Exception ex)
            {
                status = ex.ToString();

            }

            return status;
        }

        public string GeneralHtmlEmail(string mailTo, string subject, string body, string bcc = null)
        {
            string status;
            try
            {
                MailMessage mail = new MailMessage();

                if (bcc != null)
                {
                    mail.Bcc.Add(bcc);
                }

                mail.From = new MailAddress(_mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = subject;

                mail.IsBodyHtml = true;

                mail.Body = body;

                SmtpClient smtpServer = new SmtpClient(_smtpNameServer);
                smtpServer.EnableSsl = false;
                smtpServer.Port = Convert.ToInt32(_smtpServerPort);
                smtpServer.Credentials = new System.Net.NetworkCredential(_mailFromUserName, _mailFromPassword);
                smtpServer.Send(mail);

                status = "Sent";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return status;
        }

        public bool EmailAddressValidator(string emailAddress)
        {


            string strPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";


            if (System.Text.RegularExpressions.Regex.IsMatch(emailAddress, strPattern))
            { return true; }
            return false;
        }

    }
}