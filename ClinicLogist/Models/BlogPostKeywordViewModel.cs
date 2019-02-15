using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ClinicLogist.DAL;
using ClinicLogist.Service.Blog_Keyword_Management;
using ClinicLogist.Service.Blog_Management;

namespace ClinicLogist.Models
{
    public class BlogPostKeywordViewModel
    {

        public int BlogPostKeywordID { get; set; }
        [Required]
        //[UniquePhoneNumber]  // OUR CUSTOM ATTRIBUTE
        public string Keyword { get; set; }
        [Required]
        public int? BlogPost_ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Capture Date")]
        public DateTime? CapturedDateTime { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? EditedDateTime { get; set; }
        public string Createdby { get; set; }
        public string EditedBy { get; set; }
        public string Article { get; set; }

        public virtual Table_BlogPost Table_BlogPost { get; set; }

        public static List<BlogPostKeywordViewModel> GetBlogPostKeywords()
        {
            var blogRepository = new BlogRepository();
            using (var repo = new BlogPostKeywordRepository())
            {
                List<BlogPostKeywordViewModel> list = repo.GetAll().ToList().Select(x => new BlogPostKeywordViewModel
                {
                    BlogPostKeywordID = x.BlogPostKeywordID,
                    BlogPost_ID = x.BlogPost_ID,
                    Keyword = x.Keyword,
                    CapturedDateTime = x.CapturedDateTime,
                    EditedDateTime = x.EditedDateTime,
                    Createdby = x.Createdby,
                    EditedBy = x.EditedBy,
                    Article = blogRepository.GetAll().ToList().Find(s => s.BlogPost_ID == x.BlogPost_ID).StringParam
                }).ToList();

                return list;
            }
        }

        public static BlogPostKeywordViewModel GetById(int id)
        {
            using (var repo = new BlogPostKeywordRepository())
            {
                var model = new BlogPostKeywordViewModel();
                Table_BlogPostKeyword blogPostKeyword = repo.GetById(id);
                var blogRepository = new BlogRepository();

                if (blogPostKeyword != null)
                {
                    model = new BlogPostKeywordViewModel
                    {
                        BlogPost_ID = blogPostKeyword.BlogPost_ID,
                        BlogPostKeywordID = blogPostKeyword.BlogPostKeywordID,
                        Keyword = blogPostKeyword.Keyword,
                        CapturedDateTime = blogPostKeyword.CapturedDateTime,
                        EditedDateTime = blogPostKeyword.EditedDateTime,
                        Createdby = blogPostKeyword.Createdby,
                        EditedBy = blogPostKeyword.EditedBy,
                        Article = blogRepository.GetAll().ToList().Find(s => s.BlogPost_ID == blogPostKeyword.BlogPost_ID).StringParam
                    };
                }
                return model;
            }
        }

        public static void Insert(BlogPostKeywordViewModel model)
        {


            using (var repo = new BlogPostKeywordRepository())
            {
                var blogPostKeyword = new Table_BlogPostKeyword
                {
                    BlogPost_ID = model.BlogPost_ID,
                    Keyword = model.Keyword,
                    CapturedDateTime = DateTime.Now,
                    Createdby = model.Createdby
                };
                repo.Insert(blogPostKeyword);
            }
        }

        public static void Update(BlogPostKeywordViewModel x)
        {
            using (var repo = new BlogPostKeywordRepository())
            {
                Table_BlogPostKeyword blogPostKeyword = repo.GetById(x.BlogPostKeywordID);
                if (blogPostKeyword != null)
                {
                    blogPostKeyword.BlogPost_ID = x.BlogPost_ID;
                    blogPostKeyword.Keyword = x.Keyword;
                    blogPostKeyword.EditedDateTime = DateTime.Now;
                    blogPostKeyword.EditedBy = x.EditedBy;
                }
                repo.Update(blogPostKeyword);
            }
        }
        public static void Delete(int id)
        {
            using (var repo = new BlogPostKeywordRepository())
            {
                Table_BlogPostKeyword blogPostKeyword = repo.GetById(id);
                if (blogPostKeyword != null)
                {
                    repo.Delete(blogPostKeyword);
                }
            }
        }

        public static BlogPostViewModel IgnoreHTMLElements(BlogPostViewModel blogPostViewModel, string keyword, string searchParam)
        {
            string i = @"\b"+keyword+@"\b";
            string regex = $"({i})(?![^<a]*>|[^<>]*</a|[^<img]*>)";
            
            Match m = Regex.Match(blogPostViewModel.BlogText, regex, RegexOptions.IgnoreCase);

            while (m.Success)
            {
                blogPostViewModel.BlogText = blogPostViewModel.BlogText.Remove(m.Index, m.Value.Length).Insert(m.Index, $"<a data-targeturl href='/blog/article/{searchParam}'>{m.Value}</a>");
                m = m.NextMatch();
                if (m.Success)
                {
                    m = Regex.Match(blogPostViewModel.BlogText, regex, RegexOptions.IgnoreCase);
                }
            }
            return blogPostViewModel;

        }

    }
}