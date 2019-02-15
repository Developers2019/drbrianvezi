using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Client_Management
{
    public class KinRepository:IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Next_Of_Kin> _kinRepository;

        public KinRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _kinRepository = new RepositoryService<Next_Of_Kin>(_datacontext);

        }

        public Next_Of_Kin GetById(int id)
        {
            return _kinRepository.GetById(id);
        }

        public List<Next_Of_Kin> GetAll()
        {

            return _kinRepository.GetAll().ToList();

        }

        public void Insert(Next_Of_Kin model)
        {
            _kinRepository.Insert(model);
        }

        public void Update(Next_Of_Kin model)
        {
            _kinRepository.Update(model);
        }

        public void Delete(Next_Of_Kin model)
        {
            _kinRepository.Delete(model);
        }

        public IEnumerable<Next_Of_Kin> Find(Func<Next_Of_Kin, bool> predicate)
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