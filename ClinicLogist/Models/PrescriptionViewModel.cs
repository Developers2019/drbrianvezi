using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Helpers;
using ClinicLogist.Service.Medical_Management;
using drbrianvezi_cms.Helpers;

namespace ClinicLogist.Models
{
    public class PrescriptionViewModel
    {
        public int PrescriptionID { get; set; }

        [Display(Name ="Title")]
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Identification Code")]
        public string IdentificationCode { get; set; }
        [Display(Name = "Prescription Note")]
        [DataType(DataType.MultilineText)]
        public string PrescriptionNote { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Signature Date")]
        public DateTime? SignatureDateTime { get; set; }

        [Display(Name = "Signature Date")]
        public string SignatureDate { get; set; }

        public DateTime? CapturedDateTime { get; set; }

        [Display(Name = "Prescription Date")]
        public string PrescriptionDate { get; set; }
        public DateTime? EditedDateTime { get; set; }
        public int? ClientID { get; set; }

      

        public static List<PrescriptionViewModel> GetAll()
        {
            using (var prescriptionRepo = new PrescriptionRepository())
            {
                return prescriptionRepo.GetAll().ToList().Select(x => new PrescriptionViewModel {

                    PrescriptionID=x.PrescriptionID,
                    Title=x.Title,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    IdentificationCode=x.IdentificationCode,
                    PrescriptionNote=x.PrescriptionNote,
                    SignatureDateTime= Convertor.ConvertToDateTime(x.SignatureDateTime),
                    SignatureDate= Convertor.ConvertToDate(x.SignatureDateTime.Value.ToString("D")),
                    CapturedDateTime= Convertor.ConvertToDateTime(x.CapturedDateTime),
                    PrescriptionDate= Convertor.ConvertToDate(x.CapturedDateTime.Value.ToString("D")),
                    ClientID=x.ClientID




                }).ToList();
            }
        }

        public static PrescriptionViewModel GetById(int id)
        {
            using (var prescriptionRepo = new PrescriptionRepository())
            {
                Table_Prescription tablePrescription = prescriptionRepo.GetById(id);

                var model = new PrescriptionViewModel();

                if (tablePrescription != null)
                {
                    model = new PrescriptionViewModel
                    {
                        PrescriptionID = tablePrescription.PrescriptionID,
                        Title = tablePrescription.Title,
                        FirstName = tablePrescription.FirstName,
                        LastName = tablePrescription.LastName,
                        IdentificationCode = tablePrescription.IdentificationCode,
                        PrescriptionNote = tablePrescription.PrescriptionNote,
                        SignatureDateTime = Convertor.ConvertToDateTime(tablePrescription.SignatureDateTime),
                        SignatureDate = Convertor.ConvertToDate(tablePrescription.SignatureDateTime.Value.ToString("D")),
                        CapturedDateTime = Convertor.ConvertToDateTime(tablePrescription.CapturedDateTime),
                        PrescriptionDate = Convertor.ConvertToDate(tablePrescription.CapturedDateTime.Value.ToString("D")),
                        ClientID = tablePrescription.ClientID


                    };
                }
                return model;
            }


        }

        public static int Insert(PrescriptionViewModel model)
        {
            using (var prescriptionRepo = new PrescriptionRepository())
            {
                Table_Prescription tablePrescription = new Table_Prescription
                {

                    Title = model.Title,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IdentificationCode = model.IdentificationCode,
                    PrescriptionNote = model.PrescriptionNote,
                    SignatureDateTime = Convertor.ConvertToDateTime(model.SignatureDateTime),
                    CapturedDateTime = DateTime.Now,
                    ClientID = model.ClientID


                };
                prescriptionRepo.Insert(tablePrescription);
                return tablePrescription.PrescriptionID;
            }
        }

        public static void Update(PrescriptionViewModel model)
        {
            using (var prescriptionRepo = new PrescriptionRepository())
            {
                Table_Prescription tablePrescription = prescriptionRepo.GetById(model.PrescriptionID);

                if (tablePrescription != null)
                {
                    tablePrescription.PrescriptionID = model.PrescriptionID;
                    tablePrescription.Title = model.Title;
                    tablePrescription.FirstName = model.FirstName;
                    tablePrescription.LastName = model.LastName;
                    tablePrescription.IdentificationCode = model.IdentificationCode;
                    tablePrescription.PrescriptionNote = model.PrescriptionNote;
                    tablePrescription.SignatureDateTime = Convertor.ConvertToDateTime(model.SignatureDateTime);
                    tablePrescription.CapturedDateTime = Convertor.ConvertToDateTime(model.CapturedDateTime);
                    tablePrescription.ClientID = model.ClientID;

                    prescriptionRepo.Update(tablePrescription);
                }

            }

        }

        public static void Delete(int id)
        {
            using (var prescriptionRepo = new PrescriptionRepository())
            {
                Table_Prescription tablePrescription = prescriptionRepo.GetById(id);

                if (tablePrescription != null)
                {
                    prescriptionRepo.Delete(tablePrescription);
                }

            }

        }
    }
}