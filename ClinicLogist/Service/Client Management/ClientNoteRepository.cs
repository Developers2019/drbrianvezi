using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Client_Management
{
    public class ClientNoteRepository : IClientNoteRepository, IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Client_Note> _clientnoteRepository;

        public ClientNoteRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _clientnoteRepository = new RepositoryService<Table_Client_Note>(_datacontext);

        }

        public void Delete(Table_Client_Note model)
        {
            _clientnoteRepository.Delete(model);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public IEnumerable<Table_Client_Note> Find(Func<Table_Client_Note, bool> predicate)
        {
           return _clientnoteRepository.Find(predicate).ToList();
        }

        public List<Table_Client_Note> GetAll()
        {
            return _clientnoteRepository.GetAll().ToList();
        }

        public Table_Client_Note GetById(int id)
        {
            return _clientnoteRepository.GetById(id);
        }

        public void Insert(Table_Client_Note model)
        {
            _clientnoteRepository.Insert(model);
        }

        public void Update(Table_Client_Note model)
        {
            _clientnoteRepository.Update(model);
        }
    }
}