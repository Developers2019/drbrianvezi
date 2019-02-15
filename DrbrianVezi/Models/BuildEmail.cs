using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using ClinicLogist.Models;
using ClinicLogist.Service.Email_Management;

namespace DrbrianVezi.Models
{
    public class BuildEmail
    {
        public static string ContactInfoMail(ContactFormViewModel viewmodel)
        {
            var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/mailer/index.html"));

            var emailBody = $"Hi, You have an enquiry from the following person:<br /><br />";
            
            emailBody += $"<span>Full name: </span><strong>{viewmodel.FullName}</strong>";
            emailBody += $"<br/><span>Subject: </span><strong>{viewmodel.Subject}</strong>";
            emailBody += $"<br/><span>Phone Number: </span><strong>{viewmodel.PhoneNumber}</strong>";
            emailBody += $"<br/><span>Email Address: </span><strong>{viewmodel.EmailAddress}</strong>";
            emailBody += $"<br/><span>Message: </span><br/>{viewmodel.MessageBody}";

            html = html.Replace("<!--COPY HERE-->",emailBody);

            var status =EmailServiceModel.SendMail(viewmodel.EmailAddress,$"Website Contact form - {viewmodel.Subject}",html,new List<System.Net.Mail.Attachment>());

            EmailLogService.InsertLog(viewmodel.EmailAddress,status,"");
            
            return status;
        }
     
        public static string ApprovalMail(AppointmentViewModel viewmodel,string body)
        {
      
            var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/mailer/index.html"));

            var emailBody = $"Dear <strong>{viewmodel.FullName}</strong><br /><br />";

            emailBody += $"<strong>{body}</strong>";

            html = html.Replace("<!--COPY HERE-->", emailBody);

            var status = EmailServiceModel.SendMail(viewmodel.EmailAddress, $"Aprroval Notification- #{viewmodel.Appointment_ID}", html, new List<System.Net.Mail.Attachment>());

            EmailLogService.InsertLog(viewmodel.EmailAddress, status,"");

            return status;
        }

        public static string BookingMail(AppointmentViewModel viewmodel)
        {

            var date = viewmodel.Appointment_Slot_Date.Value.ToString("D");
            var time = viewmodel.Time;

            
            var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/mailer/index.html"));

            var emailBody = $"Dear <strong>{viewmodel.FullName}</strong><br /><br />";

            emailBody += $"Your appoinment booked for <strong> Date:{date}, Time:{time} </strong> has been recieved.<br /><br />";
                        
            emailBody += $"You will be contacted shortly for approval confirmation.";


            html = html.Replace("<!--COPY HERE-->", emailBody);

            var status = EmailServiceModel.SendMail(viewmodel.EmailAddress, $"Booking Notification: {viewmodel.FullName}", html, new List<System.Net.Mail.Attachment>());

            EmailLogService.InsertLog(viewmodel.EmailAddress, status,"");

            return status;
        }
        public static string NewPatient(AppointmentViewModel viewmodel)
        {

            var email = ConfigurationManager.AppSettings["AdminUser"];
            var html = File.ReadAllText(HttpContext.Current.Server.MapPath("~/mailer/index.html"));

            var emailBody = $"Dear <strong>Admin User</strong><br /><br />";

            emailBody += "A new appointment has been booked.<br /><br />";

            emailBody += $"To review the details please log onto the Dr.Brian Vezi CMS by clicking the button below.<br /><br />";

            emailBody += "<a class='btn btn-success' href='https://www.drbrianvezi.co.za/appointment/ApproveBook/"+ viewmodel.Appointment_ID+"'>Login</a>";

            
            html = html.Replace("<!--COPY HERE-->", emailBody);

            var status = EmailServiceModel.SendMail(email, $"New Appointment #{viewmodel.Appointment_ID}", html, new List<System.Net.Mail.Attachment>());

            EmailLogService.InsertLog(email, status,"");

            return status;
        }
    }



}
