using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;
using DrbrianVezi.Models;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class AppointmentController : MultiController
    {

        // GET: Appointment
        //[OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Index()
        {
           
            List<AppointmentViewModel> tableAppointment = AppointmentViewModel.GetAppointments();
         
            return View(tableAppointment.ToList());
        }
        //GET: Table_Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var table_Appointment = AppointmentViewModel.GetAppointment((int)id);
            if (table_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(table_Appointment);
        }

        // GET: Table_Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Table_Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel tableAppointment)
        { 
            if (!ModelState.IsValid)
            {
                return View(tableAppointment);
            }
            try
            {
                if (tableAppointment.Appointment_Slot_ID == 0)
                 throw new Exception("Please select a Slot Date");
                var clientmodel = new ClientDataViewModel
                {
                    Client_Name = tableAppointment.FirstName,
                    Client_Surname = tableAppointment.LastName,
                    Client_Email = tableAppointment.EmailAddress,
                    Client_Tel_Cell = tableAppointment.Contact_Number,
                    Client_ID_Number = tableAppointment.Id_Number,
                    Height = 0,
                    Weight = 0,


                };

                int id = ClientDataViewModel.Insert(clientmodel);

                tableAppointment.Client_ID = id;


                var appointmentId = AppointmentViewModel.Insert(tableAppointment);
                tableAppointment.Appointment_ID = appointmentId;

                var slotViewModel = new Appointment_SlotViewModel();
                if (tableAppointment.Appointment_Slot_ID != null)
                {
                    slotViewModel.Appointment_SLot_ID = (int)tableAppointment.Appointment_Slot_ID;
                    
                }

                Appointment_SlotViewModel.UpdateBookingStatus(slotViewModel);

                return RedirectToAction("AppointmentBookingSuccess","Appointment", new {id= appointmentId} );

            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(tableAppointment);
            }
        }

        public ActionResult AppointmentBookingSuccess(int id)
        {
            return View();
        }

        public ActionResult NewAppointments()
        {
            List<AppointmentViewModel> tableAppointment = AppointmentViewModel.GetAppointments().Where(x=> x.IsApproved == null).OrderByDescending(x=> x.Appointment_ID).ToList();
            return View(tableAppointment);
        }
        public ActionResult ApprovedAppointments()
        {
            List<AppointmentViewModel> tableAppointment = AppointmentViewModel.GetAppointments().Where(x=> x.IsApproved != null).OrderByDescending(x=> x.Appointment_ID).ToList();
            return View(tableAppointment);
        }

        public ActionResult CreateAppointment()
        {            
            return View();
        }
        [HttpPost, ActionName("CreateAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApp(AppointmentViewModel tableAppointment)
        {
            var clientmodel = new ClientDataViewModel
            {
                Client_Name = tableAppointment.FirstName,
                Client_Surname = tableAppointment.LastName,
                Client_Email = tableAppointment.EmailAddress,
                Client_Tel_Cell = tableAppointment.Contact_Number,
                Client_ID_Number = tableAppointment.Id_Number,
                Height = 0,
                Weight = 0,
                
            };

           var id= ClientDataViewModel.Insert(clientmodel);

            tableAppointment.Client_ID = id;
            AppointmentViewModel.Insert(tableAppointment);

          
            return RedirectToAction("ApprovedAppointments");

            
        }
        //// GET: Table_Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViewModel table_Appointment = AppointmentViewModel.GetAppointment((int)id);
            if (table_Appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Appointment_Slot_ID = new SelectList(Appointment_SlotViewModel.GetAppointmentSlots(), "Appointment_SLot_ID", "Appointment_SLot_ID");
            ViewBag.Client_ID = new SelectList(ClientDataViewModel.GetClients(), "Client_ClientID", "Client_Name");
            return View(table_Appointment);
        }

        //POST: Table_Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentViewModel table_Appointment)
        {
                            

            if (!ModelState.IsValid)
            {
                return View(table_Appointment);

            }
            try
            {
                AppointmentViewModel.Update(table_Appointment);
                ViewBag.Appointment_Slot_ID = new SelectList(Appointment_SlotViewModel.GetAppointmentSlots(), "Appointment_SLot_ID", "Appointment_SLot_ID");
                ViewBag.Client_ID = new SelectList(ClientDataViewModel.GetClients(), "Client_ClientID", "Client_Name");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(table_Appointment);
            }
           

        }

        //GET: Table_Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViewModel table_Appointment = AppointmentViewModel.GetAppointment((int)id);
            if (table_Appointment == null)
            {
                return HttpNotFound();
            }
            return View(table_Appointment);
        }

        //POST: Table_Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppointmentViewModel table_Appointment = AppointmentViewModel.GetAppointment(id);

            var slot = Appointment_SlotViewModel.GetAppointmentSlots().Find(x => x.Appointment_SLot_ID == table_Appointment.Appointment_Slot_ID);

            if (slot != null)
            {
                Appointment_SlotViewModel.Update(new Appointment_SlotViewModel { Appointment_SLot_ID = slot.Appointment_SLot_ID, Booked = false });
            }

            AppointmentViewModel.Delete(id);
            return RedirectToAction("ApprovedAppointments");
        }

        public ActionResult ApproveBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViewModel table_Appointment = AppointmentViewModel.GetAppointment((int)id);
            if (table_Appointment == null)
            {
                return HttpNotFound();
            }
         
            return View(table_Appointment);
        }

        //POST: Table_Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("ApproveBook")]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooking(AppointmentViewModel table_Appointment)
        {
            

            if (ModelState.IsValid)
            {
                if (!(bool) table_Appointment.IsApproved)
                {
                    table_Appointment.IsAppointCancelled = true;

                    var slotViewModel = new Appointment_SlotViewModel();
                    if (table_Appointment.Appointment_Slot_ID != null)
                    {
                        slotViewModel.Appointment_SLot_ID = (int)table_Appointment.Appointment_Slot_ID;


                    }

                    Appointment_SlotViewModel.CancelSlotBooking(slotViewModel);
                }
                else
                {
                    table_Appointment.IsAppointCancelled = false;
                    table_Appointment.IsRescheduled = false;
                }


                AppointmentViewModel.UpdateIsAppointmentBooked(new AppointmentViewModel
                {
                    Appointment_ID = table_Appointment.Appointment_ID,
                    IsRescheduled = table_Appointment.IsRescheduled,
                    IsAppointCancelled = table_Appointment.IsAppointCancelled,
                    ReasonForCancelling = table_Appointment.ReasonForCancelling,
                    IsApproved = table_Appointment.IsApproved
                });

                var date = AppointmentViewModel.GetAppointment(table_Appointment.Appointment_ID);
                var slot = Appointment_SlotViewModel.GetAppointmentSlot((int)date.Appointment_Slot_ID);

                date.Appointment_Slot_Date = slot.Appointment_Slot_Date;

                if ((bool)table_Appointment.IsApproved)
                {
                    var body = $"The appointment booked for {date.Appointment_Slot_Date.Value.ToString("D")} has been approved by Dr. Brian Vezi";
                    BuildEmail.ApprovalMail(table_Appointment, body);
                }
                else
                {
                    var body = $"Unfortunately the appointment booked for {date.Appointment_Slot_Date.Value.ToString("D")} has not been approved due to the following reasons: {table_Appointment.ReasonForCancelling}";
                    BuildEmail.ApprovalMail(table_Appointment, body);
                }
                
                return RedirectToAction("NewAppointments");
            }

            return View(table_Appointment);
        }

    }
}