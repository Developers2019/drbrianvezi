using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Helpers;
using ClinicLogist.Models;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class KeywordsController : MultiController
    {
        // GET: Keywords
        public ActionResult Index()
        {

            List<BlogPostKeywordViewModel> viewModels = BlogPostKeywordViewModel.GetBlogPostKeywords().OrderByDescending(x => x.CapturedDateTime).ToList();
            return View(viewModels);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPostKeywordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                if (viewModel.BlogPost_ID == 0)
                    throw new Exception("Please select an Article");
                var blogPostKeywordViewModel = new BlogPostKeywordViewModel
                {
                    Keyword = viewModel.Keyword,
                    BlogPost_ID = viewModel.BlogPost_ID,
                };

                BlogPostKeywordViewModel.Insert(blogPostKeywordViewModel);

                List<BlogPostViewModel> blogPostViewModels = BlogPostViewModel.GetAll().Where(b =>
                    Convertor.RemoveHTMLElements(b.BlogText, b.BlogText.Length, 0).Contains(viewModel.Keyword) &&
                    b.BlogPost_ID != viewModel.BlogPost_ID).ToList();

                if (blogPostViewModels.Any() || blogPostViewModels.Count > 0)
                {
                    var searchParam = "";
                    var articleKeywordViewModel = new ArticleKeywordViewModel();

                    if (viewModel.BlogPost_ID != null)
                    {
                      searchParam = BlogPostViewModel.GetById((int) viewModel.BlogPost_ID).SearchParam;
                    }
                    foreach (BlogPostViewModel blogPostViewModel in blogPostViewModels)
                    {
                        //Convertor.IgnoreHTMLElements(blogPostViewModel,viewModel.Keyword,searchParam);
                        blogPostViewModel.BlogText = BlogPostKeywordViewModel.IgnoreHTMLElements(blogPostViewModel, viewModel.Keyword, searchParam).BlogText;
                        //blogPostViewModel.BlogText = blogPostViewModel.BlogText.Replace(viewModel.Keyword, $"<a href='/blog/article/{searchParam}'>{viewModel.Keyword}</a>");
                        BlogPostViewModel.UpdateBlogTextViaKeyword(blogPostViewModel);

                        articleKeywordViewModel.Keyword = viewModel.Keyword;
                        articleKeywordViewModel.OriginalArticle = searchParam;
                        articleKeywordViewModel.LinkedArticle = blogPostViewModel.SearchParam;
                        ArticleKeywordViewModel.Insert(articleKeywordViewModel);
                    }
                }

                return RedirectToAction("KeywordConfirmation","Keywords", new {id = viewModel.Keyword});

            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(viewModel);
            }
        }

        public ActionResult KeywordConfirmation(string id)
        {
            ViewBag.ResultFound = false;
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            List<ArticleKeywordViewModel> blog = ArticleKeywordViewModel.GetAll()
                .Where(x => x.Keyword != null && x.Keyword.Contains(id)).ToList();
            if (!blog.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (blog.Any())
                {
                    ViewBag.ResultFound = true;
                }
                return View(blog);
            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(blog);
            }
        }
    }
}