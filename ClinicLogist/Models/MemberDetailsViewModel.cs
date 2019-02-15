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
    public class MemberDetailsViewModel
    {
        #region Properties
        public int MemberDetailId { get; set; }

        [Display(Name = "First Name")]
        public string Member_Name { get; set; }
        [Display(Name = "Surname")]
        public string Member_Surname { get; set; }
        [Display(Name = "Residential Address")]
        public string Member_Residential_Address { get; set; }
        [Display(Name = "Postal Address")]
        public string Member_Postal_Address { get; set; }
        [Display(Name = "Employer Name")]
        public string Member_Employer_Name { get; set; }
        [Display(Name = "City")]
        public string Member_City { get; set; }
        [Display(Name = "Town")]
        public string Member_Town { get; set; }
        [Display(Name = "HM Tel No.")]
        public string Member_Tel_Home { get; set; }
        [Display(Name = "WK Tel No")]
        public string Member_Tel_Work { get; set; }
        [Display(Name = "Cell No.")]
        public string Member_Tel_Cell { get; set; }
        [Display(Name = "Email Address")]
        public string Member_Email { get; set; }
        [Display(Name = "Occupation")]
        public string Member_Occupation { get; set; }
        [Display(Name = "Id No.")]
        public string Member_ID_Number { get; set; }
        public DateTime? CapturedDateTime { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        public DateTime? EditedDate { get; set; }
        public int? ClientId { get; set; }

        public string Code { get; set; } 
        #endregion


        #region Methods
        public static void Delete(int id)
        {
            using (var memeberRepo = new MemberRepository())
            {
                MemberDetail memberdetails = memeberRepo.GetById(id);

                if (memberdetails != null)
                {
                    memeberRepo.Delete(memberdetails);
                }
            }
        }

        public static List<MemberDetailsViewModel> GetAll()
        {

            using (var memeberRepo = new MemberRepository())
            {
                return memeberRepo.GetAll().ToList().Select(x => new MemberDetailsViewModel
                {
                    MemberDetailId = x.MemberDetailId,
                    Member_City = x.Member_City,
                    Member_Email = x.Member_Email,
                    Member_Employer_Name = x.Member_Employer_Name,
                    Member_ID_Number = x.Member_ID_Number,
                    Member_Name = x.Member_Name,
                    Member_Occupation = x.Member_Occupation,
                    Member_Postal_Address = x.Member_Postal_Address,
                    Member_Residential_Address = x.Member_Residential_Address,
                    Member_Surname = x.Member_Surname,
                    Member_Tel_Cell = x.Member_Tel_Cell,
                    Member_Tel_Home = x.Member_Tel_Home,
                    Member_Tel_Work = x.Member_Tel_Work,
                    Member_Town = x.Member_Town,
                    ClientId = x.ClientId,
                    Code = x.Code,
                    CapturedDateTime=Convertor.ConvertToDateTime(x.CapturedDateTime),
                    Date=Convertor.ConvertToDate(x.CapturedDateTime.Value.ToString("D"))



                }).ToList();
            }



        }

        public static MemberDetailsViewModel GetById(int id)
        {
            using (var memeberRepo = new MemberRepository())
            {
                
                var x = memeberRepo.GetById(id);
                var model = new MemberDetailsViewModel();
                if (x != null)
                {
                    model = new MemberDetailsViewModel
                    {

                        MemberDetailId = x.MemberDetailId,
                        Member_City = x.Member_City,
                        Member_Email = x.Member_Email,
                        Member_Employer_Name = x.Member_Employer_Name,
                        Member_ID_Number = x.Member_ID_Number,
                        Member_Name = x.Member_Name,
                        Member_Occupation = x.Member_Occupation,
                        Member_Postal_Address = x.Member_Postal_Address,
                        Member_Residential_Address = x.Member_Residential_Address,
                        Member_Surname = x.Member_Surname,
                        Member_Tel_Cell = x.Member_Tel_Cell,
                        Member_Tel_Home = x.Member_Tel_Home,
                        Member_Tel_Work = x.Member_Tel_Work,
                        Member_Town = x.Member_Town,
                        ClientId = x.ClientId,
                        Code = x.Code,
                        Date = Convertor.ConvertToDate(x.CapturedDateTime.Value.ToString("D"))

                    };
                }
               
                return model;
            }


        }

        public static void Insert(MemberDetailsViewModel x)
        {
            using (var memeberRepo = new MemberRepository())
            {


                MemberDetail member = new MemberDetail
                {

                    Member_City = x.Member_City,
                    Member_Email = x.Member_Email,
                    Member_Employer_Name = x.Member_Employer_Name,
                    Member_ID_Number = x.Member_ID_Number,
                    Member_Name = x.Member_Name,
                    Member_Occupation = x.Member_Occupation,
                    Member_Postal_Address = x.Member_Postal_Address,
                    Member_Residential_Address = x.Member_Residential_Address,
                    Member_Surname = x.Member_Surname,
                    Member_Tel_Cell = x.Member_Tel_Cell,
                    Member_Tel_Home = x.Member_Tel_Home,
                    Member_Tel_Work = x.Member_Tel_Work,
                    Member_Town = x.Member_Town,
                    ClientId = x.ClientId,
                    CapturedDateTime = DateTime.Now,
                    Code = x.Code


                };
                memeberRepo.Insert(member);



            }


        }

        public static void Update(MemberDetailsViewModel x)
        {
            using (var memeberRepo = new MemberRepository())
            {

                var update = memeberRepo.GetById(x.MemberDetailId);

                if (update != null)
                {
                    update.MemberDetailId = x.MemberDetailId;
                    update.Member_City = x.Member_City;
                    update.Member_Email = x.Member_Email;
                    update.Member_Employer_Name = x.Member_Employer_Name;
                    update.Member_ID_Number = x.Member_ID_Number;
                    update.Member_Name = x.Member_Name;
                    update.Member_Occupation = x.Member_Occupation;
                    update.Member_Postal_Address = x.Member_Postal_Address;
                    update.Member_Residential_Address = x.Member_Residential_Address;
                    update.Member_Surname = x.Member_Surname;
                    update.Member_Tel_Cell = x.Member_Tel_Cell;
                    update.Member_Tel_Home = x.Member_Tel_Home;
                    update.Member_Tel_Work = x.Member_Tel_Work;
                    update.Member_Town = x.Member_Town;
                    update.EditedDate = DateTime.Now;
                    update.Code = x.Code;

                    memeberRepo.Update(update);

                }

            }


        } 
        #endregion
    }
}