using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Blog_Keyword_Management;

namespace ClinicLogist.Models
{
    public class ArticleKeywordViewModel
    {
        public int ArticleKeywordID { get; set; }
        public string Keyword { get; set; }
        public string OriginalArticle { get; set; }
        public string LinkedArticle { get; set; }
        public DateTime? CapturedDateTime { get; set; }
        public DateTime? EditedDateTime { get; set; }

        public static void Delete(int id)
        {
            using (var repo = new ArticleKeywordRepository())
            {
                Table_ArticleKeyword articleKeyword = repo.GetById(id);
                if (articleKeyword != null)
                {
                    repo.Delete(articleKeyword);
                }
            }
        }

        public static List<ArticleKeywordViewModel> GetAll()
        {

            using (var repository = new ArticleKeywordRepository())
            {
                IOrderedEnumerable<Table_ArticleKeyword> returnlist = repository.GetAll().ToList().OrderByDescending(b => b.ArticleKeywordID);

                return returnlist.Select(x => new ArticleKeywordViewModel
                {
                    ArticleKeywordID = x.ArticleKeywordID,
                    Keyword = x.Keyword,
                    OriginalArticle = x.OriginalArticle,
                    LinkedArticle = x.LinkedArticle,
                    CapturedDateTime = x.CapturedDateTime,
                    EditedDateTime = x.EditedDateTime
                }).ToList();

            }
        }

        public static ArticleKeywordViewModel GetById(int id)
        {
            using (var repo = new ArticleKeywordRepository())
            {


                var model = new ArticleKeywordViewModel();
                Table_ArticleKeyword x = repo.GetById(id);

                if (x != null)
                {
                    model = new ArticleKeywordViewModel
                    {
                        ArticleKeywordID = x.ArticleKeywordID,
                        Keyword = x.Keyword,
                        OriginalArticle = x.OriginalArticle,
                        LinkedArticle = x.LinkedArticle,
                        CapturedDateTime = x.CapturedDateTime,
                        EditedDateTime = x.EditedDateTime

                    };
                }
                return model;
            }
        }

        public static void Insert(ArticleKeywordViewModel model)
        {


            using (var repo = new ArticleKeywordRepository())
            {
                var articleKeyword = new Table_ArticleKeyword
                {
                    ArticleKeywordID = model.ArticleKeywordID,
                    Keyword = model.Keyword,
                    OriginalArticle = model.OriginalArticle,
                    LinkedArticle = model.LinkedArticle,
                    CapturedDateTime = DateTime.Now,

                };
                repo.Insert(articleKeyword);
            }
        }

        public static void Update(ArticleKeywordViewModel x)
        {
            using (var repo = new ArticleKeywordRepository())
            {
                Table_ArticleKeyword articleKeyword = repo.GetById(x.ArticleKeywordID);
                if (articleKeyword != null)
                {
                    articleKeyword.ArticleKeywordID = x.ArticleKeywordID;
                    articleKeyword.Keyword = x.Keyword;
                    articleKeyword.OriginalArticle = x.OriginalArticle;
                    articleKeyword.LinkedArticle = x.LinkedArticle;
                    articleKeyword.EditedDateTime = DateTime.Now;

                }
                repo.Update(articleKeyword);
            }
        }
    }
}