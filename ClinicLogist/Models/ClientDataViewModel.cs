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
    public class ClientDataViewModel
    {

        [ScaffoldColumn(false)]
        public int? Client_ClientID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        public string Client_Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required")]
        public string Client_Surname { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{Client_Name} {Client_Surname}";
            }
        }

        [Display(Name = "Address")]
        public string Client_Address { get; set; }

        [Display(Name = "Street Address")]
        public string Client_Street2 { get; set; }

        [Display(Name = "Suburb")]
        public string Clientt_Suburb { get; set; }

        [Display(Name = "Town")]
        public string Client_Town { get; set; }

        [Display(Name = "Counrty")]
        public string Client_Country { get; set; }

        [Display(Name = "Home Tel")]
        public string Client_Tel_Home { get; set; }

        [Display(Name = "Work Tel")]
        public string Client_Tel_Work { get; set; }

        [Display(Name = "Cellphone Number")]
        public string Client_Tel_Cell { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Client_Email { get; set; }

        [Display(Name = "Dr. Referral")]
        public string Client_Reference { get; set; }

        [Display(Name = "Identity Number")]
        [MaxLength(13)]
        [MinLength(13)]
        [StringLength(13)]
        public string Client_ID_Number { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Client_Date_Of_birth { get; set; }

        [Display(Name = "Date of Birth")]
        public string Date_Of_Birth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Capture Date")]
        public DateTime? CapturedDateTime { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }


        [Display(Name = "Modified Date")]
        public DateTime? EditedDate { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Height(cm)")]
        public decimal? Height { get; set; }


        [Display(Name = "Weight(kg)")]
        public decimal? Weight { get; set; }

        public decimal? BMI_Reading { get; set; }

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Display(Name = "Dep Code")]
        public string Dep_Code { get; set; }

        [Display(Name = "Employer Name")]
        public string Employer_Name { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public static List<ClientDataViewModel> GetClients()
        {
          
                using (var clientdata = new ClientRepository())
                {

                        var returnlist=clientdata.GetAll().ToList().Select(x => new ClientDataViewModel
                        {
                            Client_ClientID = x.Client_ClientID,
                            Client_Name = x.Client_Name,
                            Client_Surname = x.Client_Surname,
                            Client_Tel_Cell = x.Client_Tel_Cell,
                            Client_Email = x.Client_Email,
                            Clientt_Suburb = x.Clientt_Suburb,
                            Client_Address = x.Client_Address,
                            Client_Country = x.Client_Country,
                            Client_Reference = x.Client_Reference,
                            Client_Street2 = x.Client_Street2,
                            Client_Tel_Home = x.Client_Tel_Home,
                            Client_Tel_Work = x.Client_Tel_Work,
                            Client_Town = x.Client_Town,
                            Client_ID_Number = x.Client_ID_Number,
                            CapturedDateTime = x.CapturedDateTime,
                            Gender = x.Gender,
                            Height = x.Height,
                            Weight = x.Weight,
                            BMI_Reading = x.BMI_Reading,
                            Age = x.Age,
                            PassportNumber = x.PassportNumber,
                            Title = x.Title,
                            Employer_Name = x.Employer_Name,
                            Dep_Code = x.Dep_Code                   
                        


                        }).ToList();
                return returnlist;
            
                }


        }
    

        public static int Insert(ClientDataViewModel model)
        {
            using (var client = new ClientRepository())
            {
                var clientData = new Table_ClientData
                {
               
                    Client_Name = model.Client_Name,
                    Client_Surname = model.Client_Surname,
                    Client_Tel_Cell = model.Client_Tel_Cell,
                    Client_Email = model.Client_Email,
                    Clientt_Suburb = model.Clientt_Suburb,
                    Client_Address = model.Client_Address,
                    Client_Country = model.Client_Country,
                    Client_Date_Of_birth = model.Client_Date_Of_birth,
                    Client_Reference = model.Client_Reference,
                    Client_Street2 = model.Client_Street2,
                    Client_Tel_Home = model.Client_Tel_Home,
                    Client_Tel_Work = model.Client_Tel_Work,
                    Client_Town = model.Client_Town,
                    Client_ID_Number = model.Client_ID_Number,
                    CapturedDateTime = DateTime.Now,
                    Gender = model.Client_ID_Number != null && Convert.ToInt16(model.Client_ID_Number.Substring(6, 1)) < 5 ? "Female" : "Male",
                    Height = model.Height,
                    Weight = model.Weight,
                    BMI_Reading = Math.Round(CalcBMI((decimal)model.Weight,(decimal)model.Height),2),
                    Age=model.Age,
                    PassportNumber=model.PassportNumber,
                    Title=model.Title,
                    Employer_Name=model.Employer_Name,
                    Dep_Code=model.Dep_Code



                };
                client.Insert(clientData);

                return clientData.Client_ClientID;
            }
        }

        public static ClientDataViewModel GetClient(int id)
        {
            using (var client = new ClientRepository())
            {
                var model = new ClientDataViewModel();
                Table_ClientData clientData = client.GetById(id);
                if (clientData != null)
                {
                    model = new ClientDataViewModel()
                    {
                        Client_ClientID = clientData.Client_ClientID,
                        Client_Name = clientData.Client_Name,
                        Client_Surname = clientData.Client_Surname,
                        Client_Tel_Cell = clientData.Client_Tel_Cell,
                        Client_Email = clientData.Client_Email,
                        Clientt_Suburb = clientData.Clientt_Suburb,
                        Client_Address = clientData.Client_Address,
                        Client_Country = clientData.Client_Country,
                        Client_Date_Of_birth = Convertor.ConvertToDateTime(clientData.Client_Date_Of_birth),
                        Client_Reference = clientData.Client_Reference,
                        Client_Street2 = clientData.Client_Street2,
                        Client_Tel_Home = clientData.Client_Tel_Home,
                        Client_Tel_Work = clientData.Client_Tel_Work,
                        Client_Town = clientData.Client_Town,
                        Client_ID_Number = clientData.Client_ID_Number,
                        CapturedDateTime= Convertor.ConvertToDateTime(clientData.CapturedDateTime),
                        Gender=clientData.Gender,
                        Height = clientData.Height,
                        Weight = clientData.Weight,
                        BMI_Reading = clientData.BMI_Reading,
                        Age = clientData.Age,
                        PassportNumber = clientData.PassportNumber,
                        Title = clientData.Title,
                        Employer_Name = clientData.Employer_Name,
                        Dep_Code = clientData.Dep_Code,
                        Date = Convertor.ConvertToDate(clientData.CapturedDateTime),


                    };
                    
                }
                return model;

            }
        }

        public static void Update(ClientDataViewModel model)
        {
            using (var clientRepo = new ClientRepository())
            {
                Table_ClientData clientData = clientRepo.GetById((int)model.Client_ClientID);
                if (clientData != null)
                {

                    clientData.Client_ClientID = (int)model.Client_ClientID;
                    clientData.Client_Name = model.Client_Name;
                    clientData.Client_Surname = model.Client_Surname;
                    clientData.Client_Tel_Cell = model.Client_Tel_Cell;
                    clientData.Client_Email = model.Client_Email;
                    clientData.Clientt_Suburb = model.Clientt_Suburb;
                    clientData.Client_Address = model.Client_Address;
                    clientData.Client_Country = model.Client_Country;
                    clientData.Client_Date_Of_birth = model.Client_Date_Of_birth;
                    clientData.Client_Reference = model.Client_Reference;
                    clientData.Client_Street2 = model.Client_Street2;
                    clientData.Client_Tel_Home = model.Client_Tel_Home;
                    clientData.Client_Tel_Work = model.Client_Tel_Work;
                    clientData.Client_Town = model.Client_Town;
                    clientData.Client_ID_Number = model.Client_ID_Number;
                    clientData.CapturedDateTime = model.CapturedDateTime;
                    clientData.EditedDate = DateTime.Now;
                    clientData.Gender = model.Gender;
                    clientData.Height = model.Height;
                    clientData.Weight = model.Weight;

                    clientData.Age = model.Age;
                    clientData.PassportNumber = model.PassportNumber;
                    clientData.Title = model.Title;
                    clientData.Employer_Name = model.Employer_Name;
                    clientData.Dep_Code = model.Dep_Code;


                    clientRepo.Update(clientData);
                }

            }

        }

        public static void Delete(int id)
        {
            using (var clientRepo = new ClientRepository())
            {
                Table_ClientData clientData = clientRepo.GetById(id);
                if (clientData != null)
                {
                    clientRepo.Delete(clientData);
                }
            }
        }
              

        public static decimal CalcBMI(decimal weight, decimal height)
        {
            var h = Helper.ConvertCM(height);
            decimal bmiCalc = 0;
            if (h != 0)
            {
                 bmiCalc = weight / (h * h);
            }
            
            return bmiCalc;
        }


        public class PatientFullDetailsViewModel
        {
            public ClientDataViewModel Patient { get; set; }
            public MemberDetailsViewModel Member { get; set; }

            public MedicalAidViewModel Medical { get; set; }

            public NextOfKinViewModel Kin { get; set; }

        }

    }
}