using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Client_Management
{
    public interface IClientNoteRepository
    {
        Table_Client_Note GetById(int id);
        List<Table_Client_Note> GetAll();
        void Insert(Table_Client_Note model);
        void Update(Table_Client_Note model);
        void Delete(Table_Client_Note model);
        IEnumerable<Table_Client_Note> Find(Func<Table_Client_Note, bool> predicate);
    }
}
