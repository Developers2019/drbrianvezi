using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;

namespace DrbrianVezi.Controllers
{
    public class MemberDetailsController : Controller
    {
        //private clinicEntities db = new clinicEntities();

        // GET: MemberDetails
        public ActionResult Index()
        {
            var memberDetails = MemberDetailsViewModel.GetAll();
            return View(memberDetails.ToList());
        }

        // GET: MemberDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetailsViewModel memberDetail = MemberDetailsViewModel.GetById((int)id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View(memberDetail);
        }

        // GET: MemberDetails/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name");
            return View();
        }

        // POST: MemberDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberDetailsViewModel memberDetail)
        {
            if (ModelState.IsValid)
            {
                MemberDetailsViewModel.Insert(memberDetail);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name", memberDetail.ClientId);
            return View(memberDetail);
        }

        // GET: MemberDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetailsViewModel memberDetail = MemberDetailsViewModel.GetById((int)id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name", memberDetail.ClientId);
            return View(memberDetail);
        }

        // POST: MemberDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberDetailsViewModel memberDetail)
        {
            if (ModelState.IsValid)
            {
                MemberDetailsViewModel.Update(memberDetail);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name", memberDetail.ClientId);
            return View(memberDetail);
        }

        // GET: MemberDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetailsViewModel memberDetail = MemberDetailsViewModel.GetById((int)id);
            if (memberDetail == null)
            {
                return HttpNotFound();
            }
            return View(memberDetail);
        }

        // POST: MemberDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberDetailsViewModel.Delete(id);
            return RedirectToAction("Index");
        }

      
    }
}
