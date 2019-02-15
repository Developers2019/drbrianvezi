using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Medical_Management
{
    public class PrescriptionRepository:IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Prescription> _prescriptionRepository;

        public PrescriptionRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _prescriptionRepository = new RepositoryService<Table_Prescription>(_datacontext);

        }

        public void Delete(Table_Prescription model)
        {
            _prescriptionRepository.Delete(model);
        }

        public IEnumerable<Table_Prescription> Find(Func<Table_Prescription, bool> predicate)
        {
            return _prescriptionRepository.Find(predicate).ToList();

        }

        public List<Table_Prescription> GetAll()
        {
            return _prescriptionRepository.GetAll().ToList();
        }

        public Table_Prescription GetById(int id)
        {
            return _prescriptionRepository.GetById(id);
        }

        public void Insert(Table_Prescription model)
        {
            _prescriptionRepository.Insert(model);
        }

        public void Update(Table_Prescription model)
        {
            _prescriptionRepository.Update(model);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}