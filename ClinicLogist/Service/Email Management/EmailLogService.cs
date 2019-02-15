using System;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Email_Management
{
    public class EmailLogService
    {
        public class EmailLogModel
        {
            public string EmailStatus { get; set; }
            public string EmailAddressTo { get; set; }
            public string Url { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CapturedDate { get; set; }

        }

        private static void Insert(EmailLogModel model)
        {
            
            using (var emaillogRepo = new EmailLogRepository())
            {

                var emailLog = new EmailLog
                {
                   EmailStatus=model.EmailStatus,
                   EmailAddressTo=model.EmailAddressTo,
                   Url=model.Url,
                   CapturedDate=DateTime.Now
                   
                };
                emaillogRepo.Insert(emailLog);
            }
        }
        public static void InsertLog(string email,string status,string url)
        {

            var emailLog = new EmailLogModel
            {
                EmailAddressTo = email,
                EmailStatus = status,
                CreatedBy = 0,
                CapturedDate = DateTime.Now,
                Url = url
            };

            Insert(emailLog);
        }



    }
}