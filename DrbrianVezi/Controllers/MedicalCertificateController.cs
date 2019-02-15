using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;
using drbrianvezi_cms.Service.RDLC_Management;

namespace DrbrianVezi.Controllers
{
    public class MedicalCertificateController : Controller
    {
        

        // GET: MedicalCertificate
        public ActionResult Index()
        {
            var tableMedicalCertificate = MedicalCertificateViewModel.GetAll();
            return View(tableMedicalCertificate.ToList());
        }

        // GET: MedicalCertificate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCertificateViewModel table_MedicalCertificate = MedicalCertificateViewModel.GetById((int)id);
            if (table_MedicalCertificate == null)
            {
                return HttpNotFound();
            }
            return View(table_MedicalCertificate);
        }

        public ActionResult Email(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reports.MedicalCertificateEmail((int)id);
            return RedirectToAction("Details","MedicalCertificate", new {id = id});
        }
        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reports.MedicalCertificateReport((int)id);
            return RedirectToAction("Details","MedicalCertificate", new {id = id});
        }

        // GET: MedicalCertificate/Create
        public ActionResult Create(int?id)
        {
            var client = ClientDataViewModel.GetClient((int)id);

            var model = new MedicalCertificateViewModel
            {

                FirstName=client.Client_Name,
                LastName=client.Client_Surname,
                Client_ID=(int)id
            };

            return View(model);
        }

        // POST: MedicalCertificate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalCertificateViewModel table_MedicalCertificate)
        {
            if (ModelState.IsValid)
            {
               int medicalCertificateId = MedicalCertificateViewModel.Insert(table_MedicalCertificate);
            
              Reports.MedicalCertificateEmail(medicalCertificateId);
                return RedirectToAction("Index");
            }
            return View(table_MedicalCertificate);
        }

        // GET: MedicalCertificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCertificateViewModel table_MedicalCertificate = MedicalCertificateViewModel.GetById((int)id);
            if (table_MedicalCertificate == null)
            {
                return HttpNotFound();
            }
            return View(table_MedicalCertificate);
        }

        // POST: MedicalCertificate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalCertificateViewModel table_MedicalCertificate)
        {
            if (ModelState.IsValid)
            {
                MedicalCertificateViewModel.Update(table_MedicalCertificate);
                return RedirectToAction("Index");
            }
            return View(table_MedicalCertificate);
        }

        // GET: MedicalCertificate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalCertificateViewModel table_MedicalCertificate = MedicalCertificateViewModel.GetById((int)id);
            if (table_MedicalCertificate == null)
            {
                return HttpNotFound();
            }
            return View(table_MedicalCertificate);
        }

        // POST: MedicalCertificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalCertificateViewModel.Delete(id);
            return RedirectToAction("Index");
        }
              
    }
}
