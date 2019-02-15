using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Invoice_Management
{
    public interface IInvoiceInfoRepository
    {
        Table_Invoice_Info GetById(int id);
        List<Table_Invoice_Info> GetAll();
        void Insert(Table_Invoice_Info model);
        void Update(Table_Invoice_Info model);
        void Delete(Table_Invoice_Info model);
        IEnumerable<Table_Invoice_Info> Find(Func<Table_Invoice_Info, bool> predicate);
    }
}
