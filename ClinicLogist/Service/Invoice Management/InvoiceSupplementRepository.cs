using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Invoice_Management
{
    public class InvoiceSupplementRepository:IInvoiceSupplementRepository,IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Invoice_Supplements> _invoicesupplementsRepository;

        public InvoiceSupplementRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _invoicesupplementsRepository = new RepositoryService<Table_Invoice_Supplements>(_datacontext);

        }

        public Table_Invoice_Supplements GetById(int id)
        {
            return _invoicesupplementsRepository.GetById(id);
        }

        public List<Table_Invoice_Supplements> GetAll()
        {
            return _invoicesupplementsRepository.GetAll().ToList();
        }

        public void Insert(Table_Invoice_Supplements model)
        {
            _invoicesupplementsRepository.Insert(model);
        }

        public void Update(Table_Invoice_Supplements model)
        {
            _invoicesupplementsRepository.Update(model);
        }

        public void Delete(Table_Invoice_Supplements model)
        {
            _invoicesupplementsRepository.Delete(model);
        }

        public IEnumerable<Table_Invoice_Supplements> Find(Func<Table_Invoice_Supplements, bool> predicate)
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