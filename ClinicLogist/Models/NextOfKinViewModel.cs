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
    public  class NextOfKinViewModel
    {
        public int Kin_Id { get; set; }

        [Display(Name = "Name")]
        public string Kin_Name { get; set; }
        [Display(Name = "Relationship")]
        public string Kin_Relationship { get; set; }
        [Display(Name = "HM Tel No.")]
        public string Kin_HM_Tel_No { get; set; }
        [Display(Name = "WK Tel No.")]
        public string Kin_WK_Tel_No { get; set; }

        [Display(Name = "Cell No.")]
        public string Kin_Cell_Tel_No { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? CaptureDate { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        public int? Client_Id { get; set; }

        public static void Delete(int id)
        {
            using (var kinRepo = new KinRepository())
            {
                var kin = kinRepo.GetById(id);
                if (kin != null)
                {
                    kinRepo.Delete(kin);
                }
            }
        }

        public static List<NextOfKinViewModel> GetAll()
        {
            using (var kinRepo = new KinRepository())
            {
                return kinRepo.GetAll().ToList().Select(x=> new NextOfKinViewModel {
                    Kin_Id=x.Kin_Id,
                    Kin_Name = x.Kin_Name,
                    Kin_Cell_Tel_No = x.Kin_Cell_Tel_No,
                    Kin_HM_Tel_No = x.Kin_HM_Tel_No,
                    Kin_Relationship = x.Kin_Relationship,
                    Kin_WK_Tel_No = x.Kin_WK_Tel_No,
                    Client_Id = x.Client_Id,
                    CaptureDate = Convertor.ConvertToDateTime(x.CaptureDate),
                    Date= Convertor.ConvertToDate(x.CaptureDate.Value.ToString("D"))

                }).ToList();
            }

        }

        public static NextOfKinViewModel GetById(int id)
        {
            using (var kinRepo = new KinRepository())
            {
                Next_Of_Kin nextOfKin = kinRepo.GetById(id);

                var model = new NextOfKinViewModel {

                    Kin_Name = nextOfKin.Kin_Name,
                    Kin_Cell_Tel_No = nextOfKin.Kin_Cell_Tel_No,
                    Kin_HM_Tel_No = nextOfKin.Kin_HM_Tel_No,
                    Kin_Relationship = nextOfKin.Kin_Relationship,
                    Kin_WK_Tel_No = nextOfKin.Kin_WK_Tel_No,
                    Client_Id = nextOfKin.Client_Id,
                    CaptureDate = Convertor.ConvertToDateTime(nextOfKin.CaptureDate),
                    Date = Convertor.ConvertToDate(nextOfKin.CaptureDate.Value.ToString("D"))

                };
                return model;
            }
        }

        public static void Insert(NextOfKinViewModel model)
        {
            using (var kinRepo = new KinRepository())
            {
                var nextOfKin = new Next_Of_Kin
                {
                    Kin_Name=model.Kin_Name,
                    Kin_Cell_Tel_No=model.Kin_Cell_Tel_No,
                    Kin_HM_Tel_No=model.Kin_HM_Tel_No,
                    Kin_Relationship=model.Kin_Relationship,
                    Kin_WK_Tel_No=model.Kin_WK_Tel_No,
                    Client_Id=model.Client_Id,
                    CaptureDate=DateTime.Now

                };
                kinRepo.Insert(nextOfKin);
            }
        }

        public static void Update(NextOfKinViewModel model)
        {
            using (var kinRepo = new KinRepository())
            {
                Next_Of_Kin x = kinRepo.GetById(model.Kin_Id);

                if (x != null)
                {
                    x.Kin_Name = model.Kin_Name;
                    x.Kin_Relationship = model.Kin_Relationship;
                    x.Kin_WK_Tel_No = model.Kin_WK_Tel_No;
                    x.Kin_HM_Tel_No = model.Kin_HM_Tel_No;
                    x.Kin_Cell_Tel_No = model.Kin_Cell_Tel_No;
                    x.EditDate = DateTime.Now;
                    x.CaptureDate = Convertor.ConvertToDateTime(model.CaptureDate);

                    kinRepo.Update(x);
                }

            }
        }
    }
}