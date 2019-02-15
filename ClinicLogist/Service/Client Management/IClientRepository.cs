using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Client_Management
{
    public interface IClientRepository
    {
        Table_ClientData GetById(int id);
        List<Table_ClientData> GetAll();
        void Insert(Table_ClientData model);
        void Update(Table_ClientData model);
        void Delete(Table_ClientData model);
        IEnumerable<Table_ClientData> Find(Func<Table_ClientData, bool> predicate);
    }
}
