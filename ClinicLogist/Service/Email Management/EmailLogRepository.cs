using System;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Email_Management
{
    public class EmailLogRepository:IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<EmailLog> _emailLogRepository;

        public EmailLogRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _emailLogRepository = new RepositoryService<EmailLog>(_datacontext);

        }
        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }

        public void Insert(EmailLog model)
        {
            _emailLogRepository.Insert(model);
        }

    
    }
}