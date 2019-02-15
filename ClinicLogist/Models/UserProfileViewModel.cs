using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Helpers;
using ClinicLogist.Service.User_Management;
using drbrianvezi_cms.Helpers;

namespace ClinicLogist.Models
{
    public class UserProfileViewModel
    {
       
        public int UserProfileId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Createdby { get; set; }

        public string UserId { get; set; }

        public static List<UserProfileViewModel> GetAll()
        {
            using (var userRepo = new UserProfileRepository())
            {
                return userRepo.GetAll().Select(x => new UserProfileViewModel {

                    UserProfileId = x.UserProfileId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ContactNo = x.ContactNo,
                    CreatedDate = Convertor.ConvertToDateTime(x.CreatedDate),
                    Createdby=x.Createdby,
                    UserId=x.UserId
                    

                }).ToList();
            }
        }

        public static UserProfileViewModel GetById(int id)
        {
            using (var userRepo = new UserProfileRepository())
            {
                var user = userRepo.GetById(id);
                var model =new UserProfileViewModel();

                if (user != null)
                {
                    model = new UserProfileViewModel {

                        UserProfileId=user.UserProfileId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        ContactNo = user.ContactNo,
                        CreatedDate = Convertor.ConvertToDateTime(user.CreatedDate),
                        Createdby = user.Createdby,
                        UserId = user.UserId

                    };
                }
                return model;
            }
        }

        public static int Insert(UserProfileViewModel model)
        {
            using (var userRepo = new UserProfileRepository()) {

                var user = new UserProfile {

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ContactNo = model.ContactNo,
                    CreatedDate = DateTime.Now,
                    Createdby = model.Createdby,
                    UserId = model.UserId

                };
                userRepo.Insert(user);
                return user.UserProfileId;

            }
        }

        public static void Update(UserProfileViewModel model)
        {
            using (var userRepo = new UserProfileRepository()) { }
        }
        public void Delete(int id)
        {
            using (var userRepo = new UserProfileRepository()) { }
        }
    }
}