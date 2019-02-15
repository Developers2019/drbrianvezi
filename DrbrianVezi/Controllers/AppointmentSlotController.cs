using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class AppointmentSlotController : MultiController
    {
        // GET: AppointmentSlot
        public ActionResult Index()
        {
            return View(Appointment_SlotViewModel.GetAppointmentSlots().ToList());
        }

        public ActionResult BookedSlots()
        {
            return View(Appointment_SlotViewModel.GetAppointmentSlots().Where(x=> x.Booked == true).ToList());
        }
        public ActionResult FreeSlots()
        {
            return View(Appointment_SlotViewModel.GetAppointmentSlots().Where(x=> x.Booked != true).ToList());
        }

        // GET: Table_Appointment_Slot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment_SlotViewModel table_Appointment_Slot = Appointment_SlotViewModel.GetAppointmentSlot((int)id);
            if (table_Appointment_Slot == null)
            {
                return HttpNotFound();
            }
            return View(table_Appointment_Slot);
        }

        // GET: Table_Appointment_Slot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Table_Appointment_Slot/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment_SlotViewModel table_Appointment_Slot)
        {
            if (!ModelState.IsValid)
            {
                return View(table_Appointment_Slot);
            }
            try
            {
                Appointment_SlotViewModel.Insert(table_Appointment_Slot);
                return RedirectToAction("SlotConfirmation");
            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(table_Appointment_Slot);
            }

            
        }
        public ActionResult SlotConfirmation()
        {
            return View();

        }

        // GET: Table_Appointment_Slot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment_SlotViewModel table_Appointment_Slot = Appointment_SlotViewModel.GetAppointmentSlot((int)id);
            if (table_Appointment_Slot == null)
            {
                return HttpNotFound();
            }
            return View(table_Appointment_Slot);
        }

        // POST: Table_Appointment_Slot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment_SlotViewModel table_Appointment_Slot)
        {
            if (ModelState.IsValid)
            {
                Appointment_SlotViewModel.Update(table_Appointment_Slot);
                return RedirectToAction("Index");
            }
            return View(table_Appointment_Slot);
        }

        // GET: Table_Appointment_Slot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment_SlotViewModel table_Appointment_Slot = Appointment_SlotViewModel.GetAppointmentSlot((int)id);
            if (table_Appointment_Slot == null)
            {
                return HttpNotFound();
            }
            return View(table_Appointment_Slot);
        }

        // POST: Table_Appointment_Slot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment_SlotViewModel.Delete(id);

            return RedirectToAction("Index");
        }
    }
}