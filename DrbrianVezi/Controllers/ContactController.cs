using System.Web.Mvc;
using DrbrianVezi.Models;

namespace DrbrianVezi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxMethod(ContactFormViewModel viewmodel)
        {
            string me = BuildEmail.ContactInfoMail(viewmodel);

            return Json(me);
        }
    }
}