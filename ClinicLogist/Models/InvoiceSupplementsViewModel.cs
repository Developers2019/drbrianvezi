using System;
using ClinicLogist.DAL;

namespace ClinicLogist.Models
{
    public class InvoiceSupplementsViewModel
    {
        public int Invoice_Supplements_ID { get; set; }
        public int? Invoice_Info_ID { get; set; }
        public int? Supplement_ID { get; set; }
        public double? Invoice_Supplements_Price { get; set; }
        public int? Invoice_Supplements_Quantity { get; set; }
        public string Invoice_Supplements_TotalPrice { get; set; }
        public DateTime? CapturedDateTime { get; set; }
        public DateTime? EditedDateTime { get; set; }

        public virtual Table_Invoice_Info Invoice_Info { get; set; }
        public virtual Table_Supplement Supplement { get; set; }
    }
}