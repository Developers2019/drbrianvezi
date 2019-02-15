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
    public class MedicalCertificateViewModel
    {

        public int MedicalCertificateID { get; set; }
        public int Client_ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Consultation Date")]
        public DateTime? ConsultedDateTime { get; set; }

       
        public string ConsultationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Admitted From")]
        public DateTime? AdmittedFrom { get; set; }

        [Display(Name = "Admitted From")]
        public string AdmittedFromDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Admitted To")]
        public DateTime? AdmittedTo { get; set; }

        [Display(Name = "Admitted To")]
        public string AdmittedToDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Unfit Since")]
        public DateTime? UnfitSince { get; set; }

        [Display(Name = "Unfit Since")]
        public string UnfitSinceDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Nature of Illness")]
        public string NatureOfIllness { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Resume Date")]
        public DateTime? ResumeDate { get; set; }

        [Display(Name = "Resume Date")]
        public string Resume { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Signature Date")]
        public DateTime? SignatureDate { get; set; }


        [Display(Name = "Signature Date")]
        public string Signature { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CapturedDateTime { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        public DateTime? EditedDateTime { get; set; }

        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }

      

        public static List<MedicalCertificateViewModel> GetAll()
        {
            using (var medicalRepo = new MedicalCertificateRepository())
            {

                return medicalRepo.GetAll().ToList().Select(x => new MedicalCertificateViewModel
                {

                    MedicalCertificateID=x.MedicalCertificateID,
                    Client_ID=x.Client_ID,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    ConsultedDateTime=Convertor.ConvertToDateTime(x.ConsultedDateTime),
                    ConsultationDate=Convertor.ConvertToDate(x.ConsultedDateTime.Value.ToString("D")),
                    AdmittedFrom= Convertor.ConvertToDateTime(x.AdmittedFrom),
                    AdmittedFromDate = Convertor.ConvertToDate(x.AdmittedFrom.Value.ToString("D")),
                    AdmittedTo= Convertor.ConvertToDateTime(x.AdmittedTo),
                    AdmittedToDate = Convertor.ConvertToDate(x.AdmittedTo.Value.ToString("D")),
                    UnfitSince= Convertor.ConvertToDateTime(x.UnfitSince),
                    UnfitSinceDate = Convertor.ConvertToDate(x.UnfitSince.Value.ToString("D")),
                    NatureOfIllness=x.NatureOfIllness,
                    ResumeDate= Convertor.ConvertToDateTime(x.ResumeDate),
                    Resume = Convertor.ConvertToDate(x.ResumeDate.Value.ToString("D")),
                    SignatureDate= Convertor.ConvertToDateTime(x.SignatureDate),
                    Signature = Convertor.ConvertToDate(x.SignatureDate.Value.ToString("D")),
                    CapturedDateTime= Convertor.ConvertToDateTime(x.CapturedDateTime),
                    CreatedBy=x.CreatedBy,
                    Date = Convertor.ConvertToDate(x.CapturedDateTime.Value.ToString("D"))

                }).ToList();
            }
        }

        public static MedicalCertificateViewModel GetById(int id)
        {
            using (var medicalRepo = new MedicalCertificateRepository())
            {
                Table_MedicalCertificate x = medicalRepo.GetById(id);

                var model = new MedicalCertificateViewModel();

                if (x != null)
                {
                    model = new MedicalCertificateViewModel
                    {

                        MedicalCertificateID = x.MedicalCertificateID,
                        Client_ID = x.Client_ID,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        ConsultedDateTime = Convertor.ConvertToDateTime(x.ConsultedDateTime),
                        ConsultationDate = x.ConsultedDateTime.Value.ToString("D"),
                        AdmittedFrom = Convertor.ConvertToDateTime(x.AdmittedFrom),
                        AdmittedTo = Convertor.ConvertToDateTime(x.AdmittedTo),
                        UnfitSince = Convertor.ConvertToDateTime(x.UnfitSince),
                        NatureOfIllness = x.NatureOfIllness,
                        ResumeDate = Convertor.ConvertToDateTime(x.ResumeDate),
                        SignatureDate = Convertor.ConvertToDateTime(x.SignatureDate),
                        CapturedDateTime = Convertor.ConvertToDateTime(x.CapturedDateTime),
                        CreatedBy = x.CreatedBy,
                        AdmittedFromDate = Convertor.ConvertToDate(x.AdmittedFrom.Value.ToString("D")),
                        AdmittedToDate = Convertor.ConvertToDate(x.AdmittedTo.Value.ToString("D")),
                        UnfitSinceDate = Convertor.ConvertToDate(x.UnfitSince.Value.ToString("D")),
                        Resume = Convertor.ConvertToDate(x.ResumeDate.Value.ToString("D")),
                        Signature = Convertor.ConvertToDate(x.SignatureDate.Value.ToString("D")),
                        Date = Convertor.ConvertToDate(x.CapturedDateTime.Value.ToString("D"))
                     

                    };
                }
                return model;
            }


        }

        public static int Insert(MedicalCertificateViewModel x)
        {
            using (var medicalRepo = new MedicalCertificateRepository())
            {

                Table_MedicalCertificate medicalCertificate = new Table_MedicalCertificate
                {

                    
                    Client_ID = x.Client_ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ConsultedDateTime = Convertor.ConvertToDateTime(x.ConsultedDateTime),
                    AdmittedFrom = Convertor.ConvertToDateTime(x.AdmittedFrom),
                    AdmittedTo = Convertor.ConvertToDateTime(x.AdmittedTo),
                    UnfitSince = Convertor.ConvertToDateTime(x.UnfitSince),
                    NatureOfIllness = x.NatureOfIllness,
                    ResumeDate = Convertor.ConvertToDateTime(x.ResumeDate),
                    SignatureDate = Convertor.ConvertToDateTime(x.SignatureDate),
                    CapturedDateTime = DateTime.Now,
                    CreatedBy = x.CreatedBy


                };
                medicalRepo.Insert(medicalCertificate);
                return medicalCertificate.MedicalCertificateID;
            }
        }

        public static void Update(MedicalCertificateViewModel x)
        {

            using (var medicalRepo = new MedicalCertificateRepository())
            {
                Table_MedicalCertificate certificate = medicalRepo.GetById(x.MedicalCertificateID);

                if (certificate != null)
                {

                    certificate.Client_ID = x.Client_ID;
                    certificate.FirstName = x.FirstName;
                    certificate.LastName = x.LastName;
                    certificate.ConsultedDateTime = Convertor.ConvertToDateTime(x.ConsultedDateTime);
                    certificate.AdmittedFrom = Convertor.ConvertToDateTime(x.AdmittedFrom);
                    certificate.AdmittedTo = Convertor.ConvertToDateTime(x.AdmittedTo);
                    certificate.UnfitSince = Convertor.ConvertToDateTime(x.UnfitSince);
                    certificate.NatureOfIllness = x.NatureOfIllness;
                    certificate.ResumeDate = Convertor.ConvertToDateTime(x.ResumeDate);
                    certificate.SignatureDate = Convertor.ConvertToDateTime(x.SignatureDate);
                    certificate.CapturedDateTime = Convertor.ConvertToDateTime(x.CapturedDateTime);
                    certificate.CreatedBy = x.CreatedBy;
                    certificate.EditedDateTime = DateTime.Now;
                    certificate.EditedBy = x.EditedBy;

                    medicalRepo.Update(certificate);
                }

            }


        }
        public static void Delete(int id)
        {
            using (var medicalRepo = new MedicalCertificateRepository())
            {
                Table_MedicalCertificate certificate = medicalRepo.GetById(id);

                if (certificate != null)
                {
                    medicalRepo.Delete(certificate);
                }


            }


        }

    }
}