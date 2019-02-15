using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Appointment_Managment
{
    public class AppointmentRepository : IAppointmentRepository, IDisposable
    {
        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Appointment> _appointmentRepository;

        public AppointmentRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _appointmentRepository = new RepositoryService<Table_Appointment>(_datacontext);

        }
        
        public void Delete(Table_Appointment model)
        {
            _appointmentRepository.Delete(model);
        }

        public IEnumerable<Table_Appointment> Find(Func<Table_Appointment, bool> predicate)
        {
            return _appointmentRepository.Find(predicate).ToList();

        }

        public List<Table_Appointment> GetAll()
        {
           return _appointmentRepository.GetAll().ToList();
        }

        public Table_Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public void Insert(Table_Appointment model)
        {
            _appointmentRepository.Insert(model);
        }

        public void Update(Table_Appointment model)
        {
            _appointmentRepository.Update(model);

        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}