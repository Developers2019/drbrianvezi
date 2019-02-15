using System;
using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Repository;

namespace ClinicLogist.Service.Appointment_Managment
{
    public class AppointmentSlotRepository : IAppointmentSlotRepository, IDisposable
    {

        private stagingclinicEntities1 _datacontext;
        private readonly IRepositoryService<Table_Appointment_Slot> _appointmentslotRepository;

        public AppointmentSlotRepository()
        {
            _datacontext = new stagingclinicEntities1();
            _appointmentslotRepository = new RepositoryService<Table_Appointment_Slot>(_datacontext);

        }
        public void Delete(Table_Appointment_Slot model)
        {
            _appointmentslotRepository.Delete(model);
        }
        public IEnumerable<Table_Appointment_Slot> Find(Func<Table_Appointment_Slot, bool> predicate)
        {
            return _appointmentslotRepository.Find(predicate).ToList();
        }

        public List<Table_Appointment_Slot> GetAll()
        {
            return _appointmentslotRepository.GetAll().ToList();
        }

        public Table_Appointment_Slot GetById(int id)
        {
            return _appointmentslotRepository.GetById(id);
        }

        public void Insert(Table_Appointment_Slot model)
        {
            _appointmentslotRepository.Insert(model);
        }

        public void Update(Table_Appointment_Slot model)
        {
            _appointmentslotRepository.Update(model);
        }
        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}