using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Appointment_Managment;
using ClinicLogist.Service.Client_Management;

namespace ClinicLogist.Models
{
    public class AppointmentViewModel
    {
      
        [Display(Name = "Appointment Id")]
        public int Appointment_ID { get; set; }

        [Display(Name = "Patient's Full Name")]
        public int? Client_ID { get; set; }
      
        [Display(Name = "Appointment Duration")]
        public double? Appointment_Duration { get; set; }
        [Display(Name = "Slot ID")]
        public int? Appointment_Slot_ID { get; set; }

        [Display(Name = "Cancelled")]
        public bool? IsAppointCancelled { get; set; }

        [Display(Name = "Patient Name")]
        public string ClientName { get; set; }
        [Display(Name = "Rescheduled")]
        public bool? IsRescheduled { get; set; }

        [Display(Name = "Reason for Cancellation")]
        [DataType(DataType.MultilineText)]
        public string ReasonForCancelling { get; set; }

        [Display(Name = "Approved")]
        public bool? IsApproved { get; set; }


        [Display(Name = "Current Patient")]
        public bool? CurrentPatient { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Display(Name = "Contact Number")]
        [Required]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Phone Number should contain only numbers")]
        public string Contact_Number { get; set; }
        [Display(Name = "Passport Number")]
        //[Required]
        public string Passport_Number { get; set; }

        [Display(Name = "ID Number")]
        //[Required]
        public string Id_Number { get; set; }
    
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public DateTime? CapturedDatetime { get; set; }
        public DateTime? EditedDateTime { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string Time { get; set; }
  

    

        [DataType(DataType.Date)]
        [Display(Name = "Slot Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      
        public DateTime? Appointment_Slot_Date { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime? Appointment_Slot_Start { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime? Appointment_Slot_End { get; set; }
       

        public static List<AppointmentViewModel> GetAppointments()
        {
            var client = new ClientRepository();
            var slot = new AppointmentSlotRepository();
     
            using (var appointmentRepo = new AppointmentRepository())
            {
                var listofAppointments = appointmentRepo.GetAll().ToList().Select(x => new AppointmentViewModel
                {
                    Appointment_ID = x.Appointment_ID,
                    ClientName= client.GetAll().ToList().Find(s=>s.Client_ClientID==x.Client_ID).Client_Name,
                    Appointment_Duration = x.Appointment_Duration,
                    Appointment_Slot_ID = x.Appointment_Slot_ID,
                    IsAppointCancelled = x.IsAppointCancelled,
                    IsRescheduled = x.IsRescheduled,
                    ReasonForCancelling = x.ReasonForCancelling,
                    EmailAddress= client.GetAll().ToList().Find(s => s.Client_ClientID == x.Client_ID).Client_Email,
                    Contact_Number= client.GetAll().ToList().Find(s => s.Client_ClientID == x.Client_ID).Client_Tel_Cell,
                    Id_Number= client.GetAll().ToList().Find(s => s.Client_ClientID == x.Client_ID).Client_ID_Number,
                    CurrentPatient=x.CurrentPatient,
                    Client_ID=x.Client_ID,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    CapturedDatetime=x.CapturedDatetime,
                    IsApproved=x.IsApproved,
                    Appointment_Slot_Date = (slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID)!= null)? slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID).Appointment_Slot_Date:DateTime.MaxValue,
                    Appointment_Slot_Start = (slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID) != null) ? slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID).Appointment_Slot_Start : DateTime.MaxValue,
                    Appointment_Slot_End = (slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID) != null) ? slot.GetAll().ToList().Find(c => c.Appointment_SLot_ID == x.Appointment_Slot_ID).Appointment_Slot_End : DateTime.MaxValue


                }).ToList();

                return listofAppointments;
            }
        }
        public static AppointmentViewModel GetAppointment(int id)
        {
            using (var appointmentRepo = new AppointmentRepository())
            {
                var clientAppointment = appointmentRepo.GetById(id);
                var model = new AppointmentViewModel();
                var client = new ClientRepository();

                if (clientAppointment != null)
                {
                     model = new AppointmentViewModel
                     {

                        Appointment_ID = clientAppointment.Appointment_ID,
                        Client_ID = clientAppointment.Client_ID,
                        ClientName= client.GetAll().ToList().Find(s => s.Client_ClientID == clientAppointment.Client_ID).Client_Name,
                        Appointment_Duration = clientAppointment.Appointment_Duration,
                        Appointment_Slot_ID = clientAppointment.Appointment_Slot_ID,
                        IsAppointCancelled = clientAppointment.IsAppointCancelled,
                        IsRescheduled = clientAppointment.IsRescheduled,
                        ReasonForCancelling = clientAppointment.ReasonForCancelling,
                        EmailAddress = client.GetAll().ToList().Find(s => s.Client_ClientID == clientAppointment.Client_ID).Client_Email,
                        Contact_Number = client.GetAll().ToList().Find(s => s.Client_ClientID == clientAppointment.Client_ID).Client_Tel_Cell,
                        Id_Number = client.GetAll().ToList().Find(s => s.Client_ClientID == clientAppointment.Client_ID).Client_ID_Number,
                        CurrentPatient=clientAppointment.CurrentPatient,
                        FirstName = clientAppointment.FirstName,
                        LastName = clientAppointment.LastName,
                        CapturedDatetime = clientAppointment.CapturedDatetime,
                        IsApproved = clientAppointment.IsApproved
                  
                     };
                }
                return model;
            
            }
        }
        public static int Insert(AppointmentViewModel viewModel)
        {
            using (var appoint = new AppointmentRepository())
            {

                
                var model = new Table_Appointment
                {
                                       
                    Client_ID = viewModel.Client_ID,
                    Appointment_Duration = viewModel.Appointment_Duration,
                    Appointment_Slot_ID = viewModel.Appointment_Slot_ID,
                    CurrentPatient= null,
                    IsAppointCancelled=false,
                    IsApproved=null,
                    IsRescheduled=false,
                    Id_Number=viewModel.Id_Number,
                    Contact_Number=viewModel.Contact_Number,
                    LastName=viewModel.LastName,
                    FirstName=viewModel.FirstName,
                    EmailAddress=viewModel.EmailAddress,
                    CapturedDatetime=DateTime.Now,
                    Passport_Number=viewModel.Passport_Number
                    
                  

                };
                appoint.Insert(model);
                return model.Appointment_ID;

            }
        }
        public static void Update(AppointmentViewModel viewModel)
        {
            using (var appointmentRepo = new AppointmentRepository())
            {

                var currentAppointment = appointmentRepo.GetById(viewModel.Appointment_ID);

                if (currentAppointment != null)
                {
                    currentAppointment.Appointment_ID = viewModel.Appointment_ID;
                    currentAppointment.Client_ID = viewModel.Client_ID;
                    currentAppointment.FirstName = viewModel.FirstName;
                    currentAppointment.LastName = viewModel.LastName;
                    currentAppointment.Appointment_Duration = viewModel.Appointment_Duration;
                    currentAppointment.Appointment_Slot_ID = viewModel.Appointment_Slot_ID;
                    currentAppointment.IsAppointCancelled = viewModel.IsAppointCancelled;
                    currentAppointment.IsRescheduled = viewModel.IsRescheduled;
                    currentAppointment.ReasonForCancelling = viewModel.ReasonForCancelling;
                    currentAppointment.CurrentPatient = viewModel.CurrentPatient;
                    currentAppointment.Contact_Number = viewModel.Contact_Number;
                    currentAppointment.Id_Number = viewModel.Id_Number;
                    currentAppointment.Passport_Number = viewModel.Passport_Number;
                    currentAppointment.EmailAddress = viewModel.EmailAddress;
                    currentAppointment.CapturedDatetime = viewModel.CapturedDatetime;  
                    currentAppointment.EditedDateTime = DateTime.Now;

                    appointmentRepo.Update(currentAppointment);
                }

            }
        }
        public static void UpdateIsAppointmentBooked(AppointmentViewModel viewModel)
        {
            using (var appointmentRepo = new AppointmentRepository())
            {

                Table_Appointment slot = appointmentRepo.GetById(viewModel.Appointment_ID);

                if (slot != null)
                {
                    slot.IsApproved = viewModel.IsApproved;
                    slot.IsAppointCancelled = viewModel.IsAppointCancelled;
                    slot.IsRescheduled = viewModel.IsRescheduled;
                    appointmentRepo.Update(slot);
                }

            }
        }
        public static void Delete(int id)
        {
            using (var appointmentRepo = new AppointmentRepository())
            {

                var currentAppointment = appointmentRepo.GetById(id);

                if (currentAppointment != null)
                {
                    appointmentRepo.Delete(currentAppointment);
                }

            }
        }

     

    }
}