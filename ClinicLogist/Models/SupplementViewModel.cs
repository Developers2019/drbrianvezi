using System.Collections.Generic;
using ClinicLogist.DAL;

namespace ClinicLogist.Models
{
    public class SupplementViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplementViewModel()
        {
            Invoice_Supplements = new HashSet<Table_Invoice_Supplements>();
        }

        public int Supplement_ID { get; set; }
        public string Supplement_Description { get; set; }
        public double? Supplement_Cost_Excl { get; set; }
        public double? Supplement_Cost_Incl { get; set; }
        public int? Supplement_Supplier_ID { get; set; }
        public int? Supplement_Min_Levels { get; set; }
        public int? Supplement_Stock_Levels { get; set; }
        public string Supplement_Nappi_Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Invoice_Supplements> Invoice_Supplements { get; set; }
        public virtual Table_Supplier Supplier { get; set; }
    }
}