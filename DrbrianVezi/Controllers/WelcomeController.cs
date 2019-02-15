using System.Web.Mvc;
using DrbrianVezi.Models;

namespace DrbrianVezi.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
            HomePageViewModel viewModel = HomePageViewModel.GetHomePageValues();
            
            return View(viewModel);
        }


    }
}