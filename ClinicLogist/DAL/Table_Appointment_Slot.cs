//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicLogist.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_Appointment_Slot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_Appointment_Slot()
        {
            this.Table_Appointment = new HashSet<Table_Appointment>();
        }
    
        public int Appointment_SLot_ID { get; set; }
        public Nullable<System.DateTime> Appointment_Slot_Date { get; set; }
        public Nullable<System.DateTime> Appointment_Slot_Start { get; set; }
        public Nullable<System.DateTime> Appointment_Slot_End { get; set; }
        public Nullable<System.DateTime> EditedDateTime { get; set; }
        public Nullable<System.DateTime> CapturedDateTime { get; set; }
        public Nullable<double> Duration { get; set; }
        public Nullable<bool> Booked { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Appointment> Table_Appointment { get; set; }
    }
}
