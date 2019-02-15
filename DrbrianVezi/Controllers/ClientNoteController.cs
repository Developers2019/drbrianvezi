using System.Linq;
using ClinicLogist.Models;
using System.Net;
using System.Web.Mvc;

namespace DrbrianVezi.Controllers
{
    public class ClientNoteController : Controller
    {
        // GET: ClientNote
        public ActionResult Index()
        {
            
            var table_Client_Note = Client_NoteViewModel.GetAll().ToList();
            //foreach (var note in table_Client_Note)
            //{
            //    if (string.IsNullOrEmpty(note.ReasonForCancelling))
            //    {

            //        return View(table_Client_Note);
            //    }
            //}

            return View(table_Client_Note);
        }

        // GET: Table_Client_Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_NoteViewModel table_Client_Note = Client_NoteViewModel.GetById((int)id);
            if (table_Client_Note == null)
            {
                return HttpNotFound();
            }
            return View(table_Client_Note);
        }

        // GET: Table_Client_Note/Create
        public ActionResult Create()
        {
            ViewBag.Appointment_ID = new SelectList(AppointmentViewModel.GetAppointments().ToList(), "Appointment_ID", "ReasonForCancelling");
            ViewBag.Client_ID = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name");
            return View();
        }

        // POST: Table_Client_Note/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client_NoteViewModel table_Client_Note)
        {
            if (ModelState.IsValid)
            {
                Client_NoteViewModel.Insert(table_Client_Note);
                return RedirectToAction("Index");
            }

            ViewBag.Appointment_ID = new SelectList(AppointmentViewModel.GetAppointments().ToList(), "Appointment_ID", "ReasonForCancelling", table_Client_Note.Appointment_ID);
            ViewBag.Client_ID = new SelectList(ClientDataViewModel.GetClients().ToList(), "Client_ClientID", "Client_Name", table_Client_Note.Client_ID);
            return View(table_Client_Note);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_NoteViewModel table_Client_Note = Client_NoteViewModel.GetById((int)id);
            if (table_Client_Note == null)
            {
                return HttpNotFound();
            }
            return View(table_Client_Note);
        }

        // POST: Table_Client_Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client_NoteViewModel table_Client_Note = Client_NoteViewModel.GetById(id);
            Client_NoteViewModel.Delete(table_Client_Note.Client_Note_ID);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client_NoteViewModel table_Client_Note = Client_NoteViewModel.GetById((int)id);

            if (table_Client_Note == null)
            {
                return HttpNotFound();
            }
            var list = Client_NoteViewModel.GetAll().ToList();
            ViewBag.Appointment_ID = new SelectList(AppointmentViewModel.GetAppointments().ToList(), "Appointment_ID", "ReasonForCancelling", table_Client_Note.Appointment_ID);
            ViewBag.Client_ID = new SelectList(list, "Client_ClientID", "Client_Name", table_Client_Note.Client_ID);
            return View(table_Client_Note);
        }

        // POST: Table_Client_Note/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Client_Note_ID,Client_ID,Note,Appointment_ID")] Client_NoteViewModel table_Client_Note)
        {
            var list = Client_NoteViewModel.GetAll().ToList();

            if (ModelState.IsValid)
            {

                Client_NoteViewModel.Update(table_Client_Note);
                return RedirectToAction("Index");
            }
            ViewBag.Appointment_ID = new SelectList(AppointmentViewModel.GetAppointments().ToList(), "Appointment_ID", "ReasonForCancelling", table_Client_Note.Appointment_ID);
            ViewBag.Client_ID = new SelectList(list, "Client_ClientID", "Client_Name", table_Client_Note.Client_ID);
            return View(table_Client_Note);
        }





    }
}