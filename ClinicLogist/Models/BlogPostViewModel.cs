using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using ClinicLogist.DAL;
using ClinicLogist.Service.Blog_Management;

namespace ClinicLogist.Models
{
    public class BlogPostViewModel
    {
        public int BlogPost_ID { get; set; }
        [Required]

        [Display(Name ="Blog Title")]
        public string Title { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        [AllowHtml]
        public string BlogText { get; set; }
        public DateTime? CapturedDateTime { get; set; }
        public DateTime? EditedDateTime { get; set; }
        public string CapturedBy { get; set; }
        public string EditedBy { get; set; }
        public int? PostViews { get; set; }
        public string SearchParam { get; set; }
        [Display(Name = "Description")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }
        public byte[] ThumbFileData { get; set; }
        public string ThumbContentType { get; set; }

        [Display(Name = "Blog Date")]
        public string DateDisplay { get; set; }


        public static void Delete(int id)
        {
            using (var blogRepo = new BlogRepository())
            {
                Table_BlogPost clientNote = blogRepo.GetById(id);
                if (clientNote != null)
                {
                    blogRepo.Delete(clientNote);
                }
            }
        }

        public static List<BlogPostViewModel> GetAll()
        {
           
            using (var clientnoterepo = new BlogRepository())
            {
                var returnlist = clientnoterepo.GetAll().ToList().OrderByDescending(b=>b.BlogPost_ID);

                return returnlist.Select(x => new BlogPostViewModel
                {
                    BlogPost_ID=x.BlogPost_ID,
                    BlogText=x.BlogText,
                    Title=x.Title,
                    PostViews=x.PostViews,
                    ContentType = x.ContentType,
                    FileData = x.FileData,
                    SearchParam=x.StringParam,
                    Desc=x.Description,
                    DateDisplay=x.CapturedDateTime.Value.ToString("D"),
                    CapturedDateTime = x.CapturedDateTime,
                    ThumbContentType = x.ThumbContentType,
                    ThumbFileData = x.ThumbFileData


                }).ToList();

            }
        }

        public static BlogPostViewModel GetById(int id)
        {
            using (var clientnoteRepo = new BlogRepository())
            {
            

                var model = new BlogPostViewModel();
                Table_BlogPost x = clientnoteRepo.GetById(id);

                if (x != null)
                {
                    model = new BlogPostViewModel
                    {
                        BlogPost_ID = x.BlogPost_ID,
                        BlogText = x.BlogText,
                        Title = x.Title,
                        PostViews = x.PostViews,
                        ContentType=x.ContentType,
                        FileData=x.FileData,
                        SearchParam = x.StringParam,
                        Desc = x.Description,
                      
                        ThumbContentType = x.ThumbContentType,
                        ThumbFileData = x.ThumbFileData,

                        CapturedDateTime = x.CapturedDateTime,
                        DateDisplay = x.CapturedDateTime.Value.ToString("D")

                    };
                }
                return model;
            }
        }

        public static void Insert(BlogPostViewModel model)
        {


            using (var clientnoteRepo = new BlogRepository())
            {
                var clientNote = new Table_BlogPost
                {
                   BlogText=model.BlogText,
                   Title=model.Title,
                   FileData=model.FileData,
                   ContentType=model.ContentType,
                   CapturedDateTime=DateTime.Now,
                   CapturedBy=model.CapturedBy,
                   PostViews=0,
                   StringParam=model.SearchParam,
                   Description=model.Desc,
                   ThumbContentType = model.ThumbContentType,
                   ThumbFileData = model.ThumbFileData


                };
                clientnoteRepo.Insert(clientNote);
            }
        }

        public static void Update(BlogPostViewModel x)
        {
            using (var clientnoteRepo = new BlogRepository())
            {
                Table_BlogPost clientNote = clientnoteRepo.GetById(x.BlogPost_ID);
                if (clientNote != null)
                {
                    clientNote.BlogText = x.BlogText;
                    clientNote.Title = x.Title;
                    clientNote.PostViews = x.PostViews;
                    clientNote.Description = x.Desc;
                    clientNote.FileData = x.FileData;
                    clientNote.ContentType = x.ContentType;
                    clientNote.EditedDateTime = DateTime.Now;
                    clientNote.StringParam = x.SearchParam;
                    clientNote.Description = x.Desc;
                    clientNote.ThumbContentType = x.ThumbContentType;
                    clientNote.ThumbFileData = x.ThumbFileData;
                    clientNote.CapturedDateTime = x.CapturedDateTime;

                }
                clientnoteRepo.Update(clientNote);
            }
        }
        public static void UpdateBlogTextViaKeyword(BlogPostViewModel x)
        {
            using (var repo = new BlogRepository())
            {
                Table_BlogPost blogPost = repo.GetById(x.BlogPost_ID);
                if (blogPost != null)
                {
                    blogPost.BlogText = x.BlogText;
                    blogPost.EditedDateTime = DateTime.Now;
                    blogPost.EditedBy = x.EditedBy;

                }
                repo.Update(blogPost);
            }
        }

        public static int Counter(BlogPostViewModel counter)
        {

            var count = GetById(counter.BlogPost_ID);

            if (count.PostViews == 0)
            {
                count.PostViews++;
            }
            else
            {
                count.PostViews++;

            }
            Update(count);

            return (int)count.PostViews;
        }

        public static string SearchParams(BlogPostViewModel blog)
        {
            var search = blog.Title.Replace(" ", "-").Trim().ToLower();
          
            return search;



        }

    } 
}