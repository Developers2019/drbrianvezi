using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Appointment_Managment;

namespace ClinicLogist.Models
{
    public class Appointment_SlotViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment_SlotViewModel()
        {
            Appointment = new HashSet<Table_Appointment>();
        }

        public int Appointment_SLot_ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Slot Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? Appointment_Slot_Date { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime? Appointment_Slot_Start { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime? Appointment_Slot_End { get; set; }
        public DateTime? EditedDateTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Captured Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CapturedDateTime { get; set; }

        public double? Duration { get; set; }
        public bool? Booked { get; set; }

        public string Time
        {

            get
            {
                return $"{Appointment_Slot_Start.Value.ToString("t")}-{Appointment_Slot_End.Value.ToString("t")}";
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Appointment> Appointment { get; set; }

        public static List<Appointment_SlotViewModel> GetAppointmentSlots()
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {
                var listofSlots = slotRepo.GetAll().ToList().Select(x => new Appointment_SlotViewModel
                {
                    Appointment_SLot_ID = x.Appointment_SLot_ID,
                    Appointment_Slot_Date = x.Appointment_Slot_Date,
                    Appointment_Slot_Start = x.Appointment_Slot_Start,
                    Appointment_Slot_End = x.Appointment_Slot_End,
                    CapturedDateTime = x.CapturedDateTime,
                    Duration = x.Duration,
                    Booked = x.Booked
                }).ToList();

                return listofSlots;
            }
        }
        public static Appointment_SlotViewModel GetAppointmentSlot(int id)
        {
            using (var appointmentRepo = new AppointmentSlotRepository())
            {
                var appointmentSlot = appointmentRepo.GetById(id);
                var model = new Appointment_SlotViewModel();

                if (appointmentSlot != null)
                {
                    model = new Appointment_SlotViewModel
                    {

                        Appointment_SLot_ID = appointmentSlot.Appointment_SLot_ID,
                        Appointment_Slot_Date = appointmentSlot.Appointment_Slot_Date,
                        Appointment_Slot_Start = appointmentSlot.Appointment_Slot_Start,
                        Appointment_Slot_End = appointmentSlot.Appointment_Slot_End,
                        Duration = appointmentSlot.Duration,
                        Booked = appointmentSlot.Booked,
                        CapturedDateTime = appointmentSlot.CapturedDateTime,

                    };
                }
                return model;

            }
        }
        public static void Insert(Appointment_SlotViewModel viewModel)
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {



                var model = new Table_Appointment_Slot
                {

                    Appointment_Slot_Date = viewModel.Appointment_Slot_Date,
                    Appointment_Slot_Start = viewModel.Appointment_Slot_Start,
                    CapturedDateTime = DateTime.Now,
                    Appointment_Slot_End = viewModel.Appointment_Slot_End,
                    Duration=CalcDuration((DateTime)viewModel.Appointment_Slot_Start, (DateTime)viewModel.Appointment_Slot_End),
                    Booked=viewModel.Booked
                    


                };
                slotRepo.Insert(model);


            }
        }
        public static void Update(Appointment_SlotViewModel viewModel)
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {

                var slot = slotRepo.GetById(viewModel.Appointment_SLot_ID);

                if (slot != null)
                {
                    slot.Appointment_Slot_Date = viewModel.Appointment_Slot_Date;
                    slot.Appointment_Slot_Start = viewModel.Appointment_Slot_Start;
                    slot.CapturedDateTime = viewModel.CapturedDateTime;
                    slot.Appointment_Slot_End = viewModel.Appointment_Slot_End;
                    slot.EditedDateTime = DateTime.Now;
                    slot.Duration = viewModel.Duration;
                    slot.Booked = viewModel.Booked;
                    slotRepo.Update(slot);
                }

            }
        }
        public static void UpdateBookingStatus(Appointment_SlotViewModel viewModel)
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {

                Table_Appointment_Slot slot = slotRepo.GetById(viewModel.Appointment_SLot_ID);

                if (slot != null)
                {
                    slot.Booked = true;
                    slotRepo.Update(slot);
                }

            }
        }
        public static void CancelSlotBooking(Appointment_SlotViewModel viewModel)
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {

                Table_Appointment_Slot slot = slotRepo.GetById(viewModel.Appointment_SLot_ID);

                if (slot != null)
                {
                    slot.Booked = false;
                    slotRepo.Update(slot);
                }

            }
        }
        public static void Delete(int id)
        {
            using (var slotRepo = new AppointmentSlotRepository())
            {

                var slot = slotRepo.GetById(id);

                if (slot != null)
                {
                    slotRepo.Delete(slot);
                }

            }
        }

        public static double CalcDuration(DateTime start, DateTime end)
        {
            TimeSpan timeSpan;
            timeSpan = end - start;

            var duration = Math.Abs(timeSpan.TotalHours);

            return duration;
        }
   
    }
}