using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Client_Management
{
    public class MemberRepository:IDisposable
    {

        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<MemberDetail> _memeberRepository;

        public MemberRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _memeberRepository = new RepositoryService<MemberDetail>(_datacontext);

        }

        public MemberDetail GetById(int id)
        {
            return _memeberRepository.GetById(id);
        }

        public List<MemberDetail> GetAll()
        {

            return _memeberRepository.GetAll().ToList();

        }

        public void Insert(MemberDetail model)
        {
            _memeberRepository.Insert(model);
        }

        public void Update(MemberDetail model)
        {
            _memeberRepository.Update(model);
        }

        public void Delete(MemberDetail model)
        {
            _memeberRepository.Delete(model);
        }

        public IEnumerable<MemberDetail> Find(Func<MemberDetail, bool> predicate)
        {
            return Find(predicate).ToList();
        }
        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}