using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Helpers;
using ClinicLogist.Service.Client_Management;
using drbrianvezi_cms.Helpers;

namespace ClinicLogist.Models
{
    public class MedicalAidViewModel
    {
        public int Med_Id { get; set; }

        [Display(Name = "Med Aid Name")]
        public string Med_Aid_Name { get; set; }

        [Display(Name = "Med Aid No.")]
        public string Med_Aid_No { get; set; }

        [Display(Name = "Option")]
        public string Med_Option { get; set; }

       
        public int? Client_Id { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }
       
        public DateTime? CaptureDate { get; set; }
 
        public DateTime? EditDate { get; set; }
        public virtual Table_ClientData Table_ClientData { get; set; }



        public static List<MedicalAidViewModel> GetAll()
        {
            using (var medicalRepo = new MedicalAidRepository())
            {
                return medicalRepo.GetAll().ToList().Select(x => new MedicalAidViewModel
                {
                    Med_Id = x.Med_Id,
                    Med_Aid_Name = x.Med_Aid_Name,
                    Med_Aid_No=x.Med_Aid_No,
                    Med_Option = x.Med_Option,
                    Client_Id = (int)x.Client_Id,
                    CaptureDate = Convertor.ConvertToDateTime(x.CaptureDate),
                    Date= Convertor.ConvertToDate(x.CaptureDate.Value.ToString("D"))

                }).ToList();
            }
        }

        public static MedicalAidViewModel GetById(int id)
        {
            using (var medicalRepo = new MedicalAidRepository())
            {

                Medical_Aid_Details x = medicalRepo.GetById(id);

                var model = new MedicalAidViewModel
                {
                    Med_Id = x.Med_Id,
                    Med_Aid_Name = x.Med_Aid_Name,
                    Med_Aid_No=x.Med_Aid_No,
                    Med_Option = x.Med_Option,
                    Client_Id = (int)x.Client_Id,
                    CaptureDate = Convertor.ConvertToDateTime(x.CaptureDate),
                    Date = Convertor.ConvertToDate(x.CaptureDate.Value.ToString("D"))
                };
                return model;

            }
        }

        public static void Insert(MedicalAidViewModel x)
        {
            using (var medicalRepo = new MedicalAidRepository())
            {

                var medical = new Medical_Aid_Details
                {
                    Med_Aid_Name = x.Med_Aid_Name,
                    Med_Aid_No=x.Med_Aid_No,
                    Med_Option = x.Med_Option,
                    Client_Id = (int)x.Client_Id,
                    CaptureDate = DateTime.Now
                };
                medicalRepo.Insert(medical);
            }

        }

        public static void Update(MedicalAidViewModel model)
        {
            using (var medicalRepo = new MedicalAidRepository())
            {


                Medical_Aid_Details medical = medicalRepo.GetById(model.Med_Id);
                if (medical != null)
                {
                    medical.Med_Aid_Name = model.Med_Aid_Name;
                    medical.Med_Aid_No = model.Med_Aid_No;
                    medical.Med_Option = model.Med_Option;
                    medical.Client_Id = (int)model.Client_Id;
                    medical.CaptureDate = Convertor.ConvertToDateTime(model.CaptureDate);
                    medical.EditDate = DateTime.Now;

                    medicalRepo.Update(medical);

                }


            }


        }

        public static void Delete(int id)
        {
            using (var medicalRepo = new MedicalAidRepository())
            {
                Medical_Aid_Details medical = medicalRepo.GetById(id);

                if (medical != null)
                {
                    medicalRepo.Delete(medical);
                }

            }
        }
    }
}