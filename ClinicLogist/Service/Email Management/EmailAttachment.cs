using System.IO;

namespace ClinicLogist.Service.Email_Management
{
    public class EmailAttachment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Stream Stream { get; set; }
    }
}