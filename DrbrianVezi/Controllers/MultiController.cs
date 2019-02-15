using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using ClinicLogist.Helpers;
using ClinicLogist.Models;
using DrbrianVezi.Models;

namespace DrbrianVezi.Controllers
{
    //This controller helps in order to pass on Global Controller methods
    public class MultiController : Controller
    {

        protected void SetViewError(Exception ex)
        {
            ModelState.AddModelError("", ex.DisplayMessage());
        }

        public PartialViewResult Notifications()
        {
            List<AppointmentViewModel> tableAppointment = AppointmentViewModel.GetAppointments().Where(x => x.IsApproved == null).ToList();

            List<AppointmentViewModel> top3 = tableAppointment.OrderByDescending(x => x.Appointment_ID).Take(3).ToList();

            ViewBag.Top3 = top3;

            ViewBag.Newappointment = tableAppointment.Count;

            //foreach (AppointmentViewModel item in top3)
            //{
            //    if (item.CapturedDatetime != null)
            //        ViewBag.HowLongAgo = Convertor.HowLongAgo((DateTime)item.CapturedDatetime);
            //}
            return PartialView("_Notifications");
        }
        public PartialViewResult CreatePatient()
        {
            return PartialView("_AddNewPatientDetails");
        }

        #region Patient Full Details

        public PartialViewResult PatientDetails(int? id)
        {
            var client = ClientDataViewModel.GetClient((int)id);
           
            return PartialView("_PatientDetails", client);
        }

        public PartialViewResult UpdatePatientDetails(int? id)
        {
            if (id != null)
            {
                ClientDataViewModel client = ClientDataViewModel.GetClient((int)id);
                var model = new ClientDataViewModel.PatientFullDetailsViewModel
                {
                    Patient = client
                };
                return PartialView("_EditPatientDetails", model);
            }
            return PartialView("_PartialNotFound");

        }

        public PartialViewResult MemberDetails(int? id)
        {
            if (id!=null)
            {

                MemberDetailsViewModel member = MemberDetailsViewModel.GetAll().Find(x => x.ClientId == (int)id);
                return PartialView("_MemberDetails", member);
            }
            return PartialView("_PartialNotFound");
        }


        public PartialViewResult UpdateMemberDetails(int? id)
        {
            MemberDetailsViewModel member = MemberDetailsViewModel.GetAll().Find(x => id != null && x.ClientId == (int) id);
            var model = new ClientDataViewModel.PatientFullDetailsViewModel
            {
                Member = member
            };
            return PartialView("_EditMemberDetails", model);
        }


        public PartialViewResult MedicalAidDetails(int? id)
        {
            var med = MedicalAidViewModel.GetAll().Find(x => x.Client_Id == (int)id);
            return PartialView("_MedicalAidDetails", med);
        }

        public PartialViewResult UpdateMedicalAidDetails(int? id)
        {
            var med = MedicalAidViewModel.GetAll().Find(x => x.Client_Id == (int)id);
            var model = new ClientDataViewModel.PatientFullDetailsViewModel
            {
                Medical= med
            };
            return PartialView("_UpdateMedicalAidDetails", model);
        }

        public PartialViewResult KinDetails(int? id)
        {
            var kin = NextOfKinViewModel.GetAll().Find(x => x.Client_Id == (int)id);
            return PartialView("_KinDetails", kin);
        }

        public PartialViewResult UpdateKinDetails(int? id)
        {
            var kin = NextOfKinViewModel.GetAll().Find(x => x.Client_Id == (int)id);
            var model = new ClientDataViewModel.PatientFullDetailsViewModel
            {
                Kin=kin
            };
            return PartialView("_UpdateKinDetails", model);
        }


        #endregion

        public PartialViewResult About()
        {
            return PartialView("_About");
        }
        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }
        public PartialViewResult Header(int? id)
        {
            ViewBag.PageName = id;
            return PartialView("_Header");
        }
       
        public PartialViewResult HomePageGallery()
        {
            return PartialView("_HomePageGallery");
        }
        public PartialViewResult TopBlogs()

        {
            var viewModel = new HomePageViewModel
            {
                BlogPostViewModels = BlogPostViewModel.GetAll().OrderByDescending(x => x.BlogPost_ID).Take(3).ToList()
            };
            
            return PartialView("_TopBlogs", viewModel);
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetImage
            (int id)
        {
            var blogPostViewModel = BlogPostViewModel.GetById(id);
            if (blogPostViewModel != null)
            {
                return File(blogPostViewModel.FileData,
                    blogPostViewModel.ContentType);
            }
            else
            {
                return null;
            }
        }
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetThmumbnailImage
            (int id)
        {
            BlogPostViewModel blogPostViewModel = BlogPostViewModel.GetById(id);
            if (blogPostViewModel != null)
            {
                return File(blogPostViewModel.ThumbFileData,
                    blogPostViewModel.ThumbContentType);
            }
            else
            {
                return null;
            }
        }
        public string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                if (viewResult.View == null)
                    throw new Exception("View not found with name: " + viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Converts exception into a JSON format suitable for getting an error message through for display
        /// </summary>
        public JsonResult ThrowJsonError(Exception ex)
        {
            var message = ex.Message;
            return ThrowJsonError(message);
        }
        public JsonResult ThrowJsonError(string message)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = message;
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetExistingKeywordsExceptKeyword(string keyword)
        //{
        //    return Json(!BlogPostKeywordViewModel.GetBlogPostKeywords().Any(x => x.Keyword == keyword), JsonRequestBehavior.AllowGet);
        //}
    }
}