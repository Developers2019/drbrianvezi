using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Supplier_Management
{
    public interface ISupplierRepository
    {
        Table_Supplier GetById(int id);
        List<Table_Supplier> GetAll();
        void Insert(Table_Supplier model);
        void Update(Table_Supplier model);
        void Delete(Table_Supplier model);
        IEnumerable<Table_Supplier> Find(Func<Table_Supplier, bool> predicate);
    }
}
