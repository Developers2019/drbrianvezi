using System.Web.Mvc;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class SelfServiceController : Controller
    {
        // GET: SelfService
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}