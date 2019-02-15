using System.Collections.Generic;
using System.Net.Mail;

namespace DrbrianVezi.Models
{
    public class EmailViewModel
    {
        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}