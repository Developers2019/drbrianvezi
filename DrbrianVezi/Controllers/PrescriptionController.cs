using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;
using drbrianvezi_cms.Service.RDLC_Management;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class PrescriptionController : Controller
    {
       
        // GET: Prescription
        public ActionResult Index()
        {
            var table_Prescription = PrescriptionViewModel.GetAll();
            return View(table_Prescription.ToList());
        }

        // GET: Prescription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionViewModel table_Prescription = PrescriptionViewModel.GetById((int)id);
            if (table_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(table_Prescription);
        }

        // GET: Prescription/Create
        public ActionResult Create(int?id)
        {
            var client = ClientDataViewModel.GetClient((int)id);

            var model = new PrescriptionViewModel
            {

                FirstName = client.Client_Name,
                LastName = client.Client_Surname,
                ClientID=id,
                Title=client.Title
                
            };
            return View(model);
        }

        // POST: Prescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrescriptionViewModel table_Prescription)
        {
            if (ModelState.IsValid)
            {
             int prescriptionId =   PrescriptionViewModel.Insert(table_Prescription);
                Reports.PrescritionNoteEmail(prescriptionId);
                return RedirectToAction("Index");
            }
            return View(table_Prescription);
        }

        // GET: Prescription/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionViewModel table_Prescription = PrescriptionViewModel.GetById((int)id);
            if (table_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(table_Prescription);
        }

        // POST: Prescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrescriptionViewModel table_Prescription)
        {
            if (ModelState.IsValid)
            {
                PrescriptionViewModel.Update(table_Prescription);
                return RedirectToAction("Index");
            }
            return View(table_Prescription);
        }

        // GET: Prescription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescriptionViewModel table_Prescription = PrescriptionViewModel.GetById((int)id);
            if (table_Prescription == null)
            {
                return HttpNotFound();
            }
            return View(table_Prescription);
        }

        // POST: Prescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            PrescriptionViewModel.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Email(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reports.PrescritionNoteEmail((int)id);
            return RedirectToAction("Details", "Prescription", new { id = id });
        }
        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reports.PrescriptionNoteReport((int)id);
            return RedirectToAction("Details", "Prescription", new { id = id });
        }

    }
}
