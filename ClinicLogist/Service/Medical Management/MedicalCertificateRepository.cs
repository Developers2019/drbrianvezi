using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Medical_Management
{
    public class MedicalCertificateRepository:IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_MedicalCertificate> _medicalcertificateRepository;

        public MedicalCertificateRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _medicalcertificateRepository = new RepositoryService<Table_MedicalCertificate>(_datacontext);

        }

        public void Delete(Table_MedicalCertificate model)
        {
            _medicalcertificateRepository.Delete(model);
        }

        public IEnumerable<Table_MedicalCertificate> Find(Func<Table_MedicalCertificate, bool> predicate)
        {
            return _medicalcertificateRepository.Find(predicate).ToList();

        }

        public List<Table_MedicalCertificate> GetAll()
        {
            return _medicalcertificateRepository.GetAll().ToList();
        }

        public Table_MedicalCertificate GetById(int id)
        {
            return _medicalcertificateRepository.GetById(id);
        }

        public void Insert(Table_MedicalCertificate model)
        {
            _medicalcertificateRepository.Insert(model);
        }

        public void Update(Table_MedicalCertificate model)
        {
            _medicalcertificateRepository.Update(model);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}