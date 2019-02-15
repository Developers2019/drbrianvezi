using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Client_Management
{
    public class ClientRepository:IClientRepository,IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_ClientData> _clientRepository;

        public ClientRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _clientRepository = new RepositoryService<Table_ClientData>(_datacontext);

        }

        public Table_ClientData GetById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public List<Table_ClientData> GetAll()
        {
            
            return _clientRepository.GetAll().ToList();
          
        }

        public void Insert(Table_ClientData model)
        {
            _clientRepository.Insert(model);
        }

        public void Update(Table_ClientData model)
        {
            _clientRepository.Update(model);
        }

        public void Delete(Table_ClientData model)
        {
            _clientRepository.Delete(model);
        }

        public IEnumerable<Table_ClientData> Find(Func<Table_ClientData, bool> predicate)
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