using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClinicLogist.Models;

namespace drbrianvezi_cms.Helpers
{
    public class UniquePhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string number = value as string;
            BlogPostKeywordViewModel contact = validationContext.ObjectInstance as BlogPostKeywordViewModel;

            if (number != null)
            {
                
                    var contactFound = BlogPostKeywordViewModel.GetBlogPostKeywords().FirstOrDefault(item => item.Keyword.Trim().Equals(number.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (contactFound != null && contactFound.BlogPostKeywordID != contact.BlogPostKeywordID)
                    {
                        return new ValidationResult("Keyword has been taken by " + contactFound.Article);
                    }
                
            }

            return ValidationResult.Success;
        }
    }
}