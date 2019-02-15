using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Invoice_Management
{
    public class InvoiceInfoRepository:IInvoiceInfoRepository,IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Invoice_Info> _invoiceinfoRepository;

        public InvoiceInfoRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _invoiceinfoRepository = new RepositoryService<Table_Invoice_Info>(_datacontext);

        }

        public Table_Invoice_Info GetById(int id)
        {
            return _invoiceinfoRepository.GetById(id);
        }

        public List<Table_Invoice_Info> GetAll()
        {
            return _invoiceinfoRepository.GetAll().ToList();
        }

        public void Insert(Table_Invoice_Info model)
        {
            _invoiceinfoRepository.Insert(model);
        }

        public void Update(Table_Invoice_Info model)
        {
            _invoiceinfoRepository.Update(model);
        }

        public void Delete(Table_Invoice_Info model)
        {
            _invoiceinfoRepository.Delete(model);
        }

        public IEnumerable<Table_Invoice_Info> Find(Func<Table_Invoice_Info, bool> predicate)
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