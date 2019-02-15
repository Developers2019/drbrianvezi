using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Invoice_Management
{
    public interface IInvoiceSupplementRepository
    {
        Table_Invoice_Supplements GetById(int id);
        List<Table_Invoice_Supplements> GetAll();
        void Insert(Table_Invoice_Supplements model);
        void Update(Table_Invoice_Supplements model);
        void Delete(Table_Invoice_Supplements model);
        IEnumerable<Table_Invoice_Supplements> Find(Func<Table_Invoice_Supplements, bool> predicate);
    }
}
