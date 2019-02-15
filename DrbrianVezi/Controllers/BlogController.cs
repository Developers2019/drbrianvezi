using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicLogist.Helpers;
using ClinicLogist.Models;
using drbrianvezi_cms.Helpers;

namespace DrbrianVezi.Controllers
{
    public class BlogController : MultiController
    {
        // GET: Blog
        public ActionResult Index()
        {

            return View(BlogPostViewModel.GetAll().OrderByDescending(x=> x.BlogPost_ID).ToList());
        }
        [Authorize]
        public ActionResult AddNewBlogPost()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddNewBlogPost(BlogPostViewModel viewModel, HttpPostedFileBase image)
        {
            
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                if (image != null)
                {
                    viewModel.ContentType = image.ContentType;
                    viewModel.FileData = new byte[image.ContentLength];
                    image.InputStream.Read(viewModel.FileData, 0,image.ContentLength);

                    string path = Server.MapPath("~/BlogImages/");


                    string saveCroppedImage = Convertor.SaveCroppedImage(Image.FromStream(image.InputStream),
                        985, 657, path +Helper.GetRandomNumber(1,100)+ DateTime.Now.ToString("yy-MM-dd") + ".jpg");

                    var readAllBytes = System.IO.File.ReadAllBytes(saveCroppedImage);
                    var imageJpeg = "image/jpeg";

                    viewModel.ThumbContentType = imageJpeg;
                    viewModel.ThumbFileData = readAllBytes;

                    if (System.IO.File.Exists(saveCroppedImage))
                    {
                        System.IO.File.Delete(saveCroppedImage);
                    }
                }
                viewModel.SearchParam = BlogPostViewModel.SearchParams(viewModel);
                viewModel.Desc = Convertor.LimitText(viewModel.Desc.ToString(), 65);

                BlogPostViewModel.Insert(viewModel);
                return RedirectToAction("Index");
               
            }
            catch (Exception e)
            {
                SetViewError(e);
                return View(viewModel);
            }
        
        }

        public ActionResult Article(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            var blog = BlogPostViewModel.GetAll().ToList().Find(x => x.SearchParam != null && x.SearchParam.Contains(id));
        
            blog.DateDisplay= blog.CapturedDateTime.Value.ToString("D");

            ViewBag.PostViews = BlogPostViewModel.Counter(blog);

            if (blog == null)
            {
                return HttpNotFound();
            }
           BlogPostViewModel blogPostViewModel = BlogPostViewModel.GetAll().FindAll(x => x.BlogPost_ID < blog.BlogPost_ID).FirstOrDefault();
            if (blogPostViewModel != null)
            {
                ViewBag.NextTitle = blogPostViewModel.Title;
                ViewBag.NextSearchParam = blogPostViewModel.SearchParam;
            }
           BlogPostViewModel blogPostViewModelPrevious = BlogPostViewModel.GetAll().FindAll(x => x.BlogPost_ID > blog.BlogPost_ID).FirstOrDefault();
            if (blogPostViewModelPrevious != null)
            {
                ViewBag.PreviousTitle = blogPostViewModelPrevious.Title;
                ViewBag.PreviousSearchParam = blogPostViewModelPrevious.SearchParam;
            }

            return View(blog);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostViewModel blog = BlogPostViewModel.GetAll().Find(x=>x.SearchParam.Contains(id));
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPostViewModel blog, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }
            try {

                BlogPostViewModel blogId = BlogPostViewModel.GetAll().Find(x => x.SearchParam.Contains(blog.SearchParam));
                if (image != null)
                {
                    blog.ContentType = image.ContentType;
                    blog.FileData = new byte[image.ContentLength];
                    image.InputStream.Read(blog.FileData, 0, image.ContentLength);

                    string path = Server.MapPath("~/BlogImages/");

                    string saveCroppedImage = Convertor.SaveCroppedImage(Image.FromStream(image.InputStream),
                        985, 657, path + DateTime.Now.ToString("yy-MM-dd") + ".jpg");

                    var readAllBytes = System.IO.File.ReadAllBytes(saveCroppedImage);
                    var imageJpeg = "image/jpeg";

                    blog.ThumbContentType = imageJpeg;
                    blog.ThumbFileData = readAllBytes;

                    if (System.IO.File.Exists(saveCroppedImage))
                    {
                        System.IO.File.Delete(saveCroppedImage);
                    }

                }
                blog.BlogPost_ID = blogId.BlogPost_ID;
                blog.PostViews = blogId.PostViews;
                blog.CapturedDateTime = blogId.CapturedDateTime;
                blog.SearchParam = BlogPostViewModel.SearchParams(blog);


                BlogPostViewModel.Update(blog);
                return RedirectToAction("GetListOfBlogs");

            } catch (Exception e) {

                SetViewError(e);
                return View(blog);

            }
           
            
        }

        public ActionResult GetListOfBlogs()
        {

            return View(BlogPostViewModel.GetAll().OrderByDescending(x=> x.CapturedDateTime));


            
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostViewModel blog = BlogPostViewModel.GetAll().Find(x => x.SearchParam.Contains(id));
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        //POST: Table_Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BlogPostViewModel blog = BlogPostViewModel.GetAll().Find(x => x.SearchParam.Contains(id));

            BlogPostViewModel.Delete(blog.BlogPost_ID);
            return RedirectToAction("GetListOfBlogs");
        }



    }
}