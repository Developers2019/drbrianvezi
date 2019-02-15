using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ClinicLogist.Service.Email_Management
{
    public class EmailServiceModel
    {
        
        public static string SendMail(string destination,string subject, string body, List<Attachment> attachments)
        {
            string status = "";
            try
            {

                var username = ConfigurationManager.AppSettings["username"];
                var password = ConfigurationManager.AppSettings["password"];
                var message = new MailMessage { From = new MailAddress(username, "Dr.Brian Vezi") };

                destination = ConfigurationManager.AppSettings["DeveloperEmail"];

                var destinations = destination.Split(',').ToList();
                foreach (var dest in destinations)
                {
                    message.To.Add(new MailAddress(dest));
                }

                message.IsBodyHtml = true;

                message.Subject = subject;

                message.Body = body;

                var client = new SmtpClient(ConfigurationManager.AppSettings["smtp"],
                    Convert.ToInt32(ConfigurationManager.AppSettings["port"]))
                {
                    Credentials = new NetworkCredential(username,password)
                };
                if (attachments !=null)
                {
                    foreach (Attachment attachment in attachments)
                    {


                        message.Attachments.Add(attachment);
                    }
                }
                

             client.Send(message);



                status = "Sent";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }
        

    }
}