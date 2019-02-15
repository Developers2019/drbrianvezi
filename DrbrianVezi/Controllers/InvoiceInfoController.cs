using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;

namespace DrbrianVezi.Controllers
{
    public class InvoiceInfoController : Controller
    {
        // GET: InvoiceInfo
        public ActionResult Index()
        {
            var table_Invoice_Info = Invoice_InfoViewModel.GetInvoices();
            return View(table_Invoice_Info.ToList());
        }

        // GET: Table_Invoice_Info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_InfoViewModel table_Invoice_Info = Invoice_InfoViewModel.GetInvoice((int)id);
            if (table_Invoice_Info == null)
            {
                return HttpNotFound();
            }
            return View(table_Invoice_Info);
        }

        // GET: Table_Invoice_Info/Create
        public ActionResult Create(int id)
        {
            //ViewBag.Invoice_Client_ID = new SelectList(ClientDataViewModel.GetClients(), "Client_ClientID", "Client_Name");
            var client=ClientDataViewModel.GetClient(id);
            return View();
        }

        // POST: Table_Invoice_Info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoice_InfoViewModel table_Invoice_Info)
        {
            if (ModelState.IsValid)
            {
                Invoice_InfoViewModel.Insert(table_Invoice_Info);
                return RedirectToAction("Index");
            }

            ViewBag.Invoice_Client_ID = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name");
            return View(table_Invoice_Info);
        }

        // GET: Table_Invoice_Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_InfoViewModel table_Invoice_Info = Invoice_InfoViewModel.GetInvoice((int)id);
            if (table_Invoice_Info == null)
            {
                return HttpNotFound();
            }
            ViewBag.Invoice_Client_ID = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name");
            return View(table_Invoice_Info);
        }

        // POST: Table_Invoice_Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice_InfoViewModel table_Invoice_Info)
        {
            if (ModelState.IsValid)
            {
                Invoice_InfoViewModel.Update(table_Invoice_Info);
                return RedirectToAction("Index");
            }
            ViewBag.Invoice_Client_ID = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name");
            return View(table_Invoice_Info);
        }

        // GET: Table_Invoice_Info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_InfoViewModel table_Invoice_Info = Invoice_InfoViewModel.GetInvoice((int)id);
            if (table_Invoice_Info == null)
            {
                return HttpNotFound();
            }
            return View(table_Invoice_Info);
        }

        // POST: Table_Invoice_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Invoice_InfoViewModel.Delete(id);
            return RedirectToAction("Index");
        }
    }
}