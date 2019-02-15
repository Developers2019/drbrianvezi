using System;
using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Service.Appointment_Managment
{
    public interface IAppointmentSlotRepository
    {
        Table_Appointment_Slot GetById(int id);
        List<Table_Appointment_Slot> GetAll();
        void Insert(Table_Appointment_Slot model);
        void Update(Table_Appointment_Slot model);
        void Delete(Table_Appointment_Slot model);
        IEnumerable<Table_Appointment_Slot> Find(Func<Table_Appointment_Slot, bool> predicate);

    }
}
