using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClinicLogist.Models;
using DrbrianVezi.Models;

namespace DrbrianVezi.Controllers
{
    public class BookAnAppointmentController : Controller
    {
        // GET: BookAnAppointment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult GetSlotsByDate(Appointment_SlotViewModel viewModel)
        {
            if (viewModel.Appointment_Slot_Date == null)
            {
                viewModel.Appointment_Slot_Date = DateTime.Now;
            }
            IEnumerable<Appointment_SlotViewModel> model = Appointment_SlotViewModel.GetAppointmentSlots().ToList().Where(x => x.Booked != true && x.Appointment_Slot_Date.Value.Date == viewModel.Appointment_Slot_Date.Value.Date && x.Appointment_Slot_Date.Value.Date >= DateTime.Now.Date);
            @ViewBag.AllOptions = Appointment_SlotViewModel.GetAppointmentSlots().ToList().Where(x => x.Booked != true);
            return PartialView("_AvailableSlots", model);
        }
        public PartialViewResult All(DateTime? me)
        {
            if (me == null)
            {
                me = DateTime.Now;
            }
            IEnumerable<Appointment_SlotViewModel> model = Appointment_SlotViewModel.GetAppointmentSlots().ToList().Where(x => x.Booked != true && x.Appointment_Slot_Date.Value.Date >= DateTime.Now.Date);
            return PartialView("_AvailableSlots", model);
        }

        public ActionResult AppointmentForm(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var appointmentViewModel = new AppointmentViewModel { Appointment_Slot_ID = id };
            return View(appointmentViewModel);
        }

        [HttpPost]
        public JsonResult AjaxMethod2(AppointmentViewModel tableAppointment)
        {
            try {

                var currentPatient = ClientDataViewModel.GetClients().Find(x=>x.Client_ID_Number==tableAppointment.Id_Number || x.PassportNumber==tableAppointment.Passport_Number);

                if (currentPatient != null)
                {

                    tableAppointment.Client_ID = currentPatient.Client_ClientID;


                    var appointmentId = AppointmentViewModel.Insert(tableAppointment);
                    tableAppointment.Appointment_ID = appointmentId;

                    //AppointmentViewModel.Update(tableAppointment);

                    var slotViewModel = new Appointment_SlotViewModel();
                    if (tableAppointment.Appointment_Slot_ID != null)
                    {
                        slotViewModel.Appointment_SLot_ID = (int)tableAppointment.Appointment_Slot_ID;

                    }

                    var appointment = AppointmentViewModel.GetAppointment(appointmentId);

                    var slotdate = Appointment_SlotViewModel.GetAppointmentSlot((int)tableAppointment.Appointment_Slot_ID);
                    tableAppointment.Appointment_Slot_Date = slotdate.Appointment_Slot_Date;

                    tableAppointment.Time = slotdate.Time;

                    Appointment_SlotViewModel.UpdateBookingStatus(slotViewModel);
                    BuildEmail.BookingMail(tableAppointment);
                    BuildEmail.NewPatient(tableAppointment);

                    return Json(tableAppointment.Client_ID);
                }
                else
                {
                    var clientmodel = new ClientDataViewModel
                    {
                        Client_Name = tableAppointment.FirstName,
                        Client_Surname = tableAppointment.LastName,
                        Client_Email = tableAppointment.EmailAddress,
                        Client_Tel_Cell = tableAppointment.Contact_Number,
                        Client_ID_Number = tableAppointment.Id_Number,
                        PassportNumber = tableAppointment.Passport_Number,
                        Height = 0,
                        Weight = 0,


                    };

                    int id = ClientDataViewModel.Insert(clientmodel);

                    tableAppointment.Client_ID = id;


                    var appointmentId = AppointmentViewModel.Insert(tableAppointment);
                    tableAppointment.Appointment_ID = appointmentId;

                    //AppointmentViewModel.Update(tableAppointment);

                    var slotViewModel = new Appointment_SlotViewModel();
                    if (tableAppointment.Appointment_Slot_ID != null)
                    {
                        slotViewModel.Appointment_SLot_ID = (int)tableAppointment.Appointment_Slot_ID;

                    }

                    var appointment = AppointmentViewModel.GetAppointment(appointmentId);

                    var slotdate = Appointment_SlotViewModel.GetAppointmentSlot((int)tableAppointment.Appointment_Slot_ID);
                    tableAppointment.Appointment_Slot_Date = slotdate.Appointment_Slot_Date;

                    tableAppointment.Time = slotdate.Time;

                    Appointment_SlotViewModel.UpdateBookingStatus(slotViewModel);
                    BuildEmail.BookingMail(tableAppointment);
                    BuildEmail.NewPatient(tableAppointment);

                    return Json(id);

                }


               

            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }
            

        }

        public ActionResult ThankYouForBooking()
        {

            return View();
        }
    }
}