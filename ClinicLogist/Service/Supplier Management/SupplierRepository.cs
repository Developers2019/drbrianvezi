using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Supplier_Management
{
    public class SupplierRepository:ISupplierRepository,IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Supplier> _supplierRepository;

        public SupplierRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _supplierRepository = new RepositoryService<Table_Supplier>(_datacontext);

        }

        public Table_Supplier GetById(int id)
        {
            return _supplierRepository.GetById(id);
        }

        public List<Table_Supplier> GetAll()
        {
            return _supplierRepository.GetAll().ToList();
        }

        public void Insert(Table_Supplier model)
        {
            _supplierRepository.Insert(model);
        }

        public void Update(Table_Supplier model)
        {
            _supplierRepository.Update(model);
        }

        public void Delete(Table_Supplier model)
        {
            _supplierRepository.Delete(model);
        }

        public IEnumerable<Table_Supplier> Find(Func<Table_Supplier, bool> predicate)
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