using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.User_Management
{
    public class UserProfileRepository:IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<UserProfile> _userProfileRepository;

        public UserProfileRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _userProfileRepository = new RepositoryService<UserProfile>(_datacontext);

        }

        public void Delete(UserProfile model)
        {
            _userProfileRepository.Delete(model);
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _userProfileRepository.Find(predicate).ToList();

        }

        public List<UserProfile> GetAll()
        {
            return _userProfileRepository.GetAll().ToList();
        }

        public UserProfile GetById(int id)
        {
            return _userProfileRepository.GetById(id);
        }

        public void Insert(UserProfile model)
        {
            _userProfileRepository.Insert(model);
        }

        public void Update(UserProfile model)
        {
            _userProfileRepository.Update(model);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}