using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Appointment_Managment
{
    public interface IAppointmentRepository
    {
        Table_Appointment GetById(int id);
        List<Table_Appointment> GetAll();
        void Insert(Table_Appointment model);
        void Update(Table_Appointment model);
        void Delete(Table_Appointment model);
        IEnumerable<Table_Appointment> Find(Func<Table_Appointment, bool> predicate);
    }
}
