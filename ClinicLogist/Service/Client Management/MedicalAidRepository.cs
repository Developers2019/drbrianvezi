using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Client_Management
{
    public class MedicalAidRepository: IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Medical_Aid_Details> _medicalRepository;

        public MedicalAidRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _medicalRepository = new RepositoryService<Medical_Aid_Details>(_datacontext);

        }

        public Medical_Aid_Details GetById(int id)
        {
            return _medicalRepository.GetById(id);
        }

        public List<Medical_Aid_Details> GetAll()
        {

            return _medicalRepository.GetAll().ToList();

        }

        public void Insert(Medical_Aid_Details model)
        {
            _medicalRepository.Insert(model);
        }

        public void Update(Medical_Aid_Details model)
        {
            _medicalRepository.Update(model);
        }

        public void Delete(Medical_Aid_Details model)
        {
            _medicalRepository.Delete(model);
        }

        public IEnumerable<Medical_Aid_Details> Find(Func<Medical_Aid_Details, bool> predicate)
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