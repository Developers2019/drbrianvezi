using System.ComponentModel.DataAnnotations;

namespace DrbrianVezi.Models
{
    public class ContactFormViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Phone Number should contain only numbers")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter a valid e-mail address")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Message")]
        [MaxLength(200)]
        public string MessageBody { get; set; }

     
    }
}