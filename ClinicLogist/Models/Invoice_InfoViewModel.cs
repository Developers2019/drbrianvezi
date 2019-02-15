using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Invoice_Management;
using drbrianvezi_cms.Helpers;

namespace ClinicLogist.Models
{
    public class Invoice_InfoViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice_InfoViewModel()
        {
            Invoice_Supplements = new HashSet<Table_Invoice_Supplements>();
        }

        public int Invoice_Info_ID { get; set; }

        [Display(Name ="Invoice Number")]
        public string Invoice_Number { get; set; }

        [Display(Name = "Patient Name")]
        public int? Invoice_Client_ID { get; set; }

        [Display(Name = "Consultation Fee")]
        public double? Invoice_Consultation { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? Invoice_Date { get; set; }
       
        public bool Invoice_Paid { get; set; }

        [Display(Name = "Payment Method")]
        public string Invoice_Payment_method { get; set; }

        [Display(Name = "Appointment")]
        public bool Invoice_Is_Appointment { get; set; }

        public bool? Invoice_Is_Supplements { get; set; }

        [Display(Name = "Total Payment")]
        public double? Invoice_Total { get; set; }

        [Display(Name = "Appointment")]
        public string Answer { get; set; }

        public virtual Table_ClientData ClientData { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Invoice_Supplements> Invoice_Supplements { get; set; }


        public static List<Invoice_InfoViewModel> GetInvoices()
        {
            using (var invoicerepo = new InvoiceInfoRepository())
            {

                var invoicelist = invoicerepo.GetAll().ToList().Select(x => new Invoice_InfoViewModel
                {
                    Invoice_Info_ID = x.Invoice_Info_ID,
                    Invoice_Client_ID = x.Invoice_Client_ID,
                    Invoice_Number = x.Invoice_Number,
                    //Invoice_Paid = x.Invoice_Paid,
                    //Invoice_Consultation = x.Invoice_Consultation,
                    //Invoice_Is_Appointment = x.Invoice_Is_Appointment,
                    Invoice_Is_Supplements = x.Invoice_Is_Supplements,
                    Invoice_Payment_method = x.Invoice_Payment_method,
                    Invoice_Date = x.Invoice_Date,
                    Invoice_Total = x.Invoice_Total,
                    Answer = Helper.YesorNo(x.Invoice_Is_Appointment)


                });


                return invoicelist.ToList();
            }
        }

        public static Invoice_InfoViewModel GetInvoice(int id)
        {
            using (var invoicerepo = new InvoiceInfoRepository())
            {
                var x = new Invoice_InfoViewModel();
                var invoice = invoicerepo.GetById(id);

                if(invoice != null)
                {
                    x = new Invoice_InfoViewModel()
                    {
                        Invoice_Info_ID = x.Invoice_Info_ID,
                        Invoice_Client_ID = x.Invoice_Client_ID,
                        Invoice_Number = x.Invoice_Number,
                        Invoice_Paid = x.Invoice_Paid,
                        Invoice_Consultation = x.Invoice_Consultation,
                        Invoice_Is_Appointment = x.Invoice_Is_Appointment,
                        Invoice_Is_Supplements = x.Invoice_Is_Supplements,
                        Invoice_Payment_method = x.Invoice_Payment_method,
                        Invoice_Date = x.Invoice_Date,
                        Invoice_Total = x.Invoice_Total,
                        Answer = Helper.YesorNo(x.Invoice_Is_Appointment)
                    };

                }
                return x;
            }
           
        }
        public static void Insert(Invoice_InfoViewModel x)
        {
            using (var invoicerepo = new InvoiceInfoRepository())
            {
                var invoice = new Table_Invoice_Info
                {
                    Invoice_Client_ID = x.Invoice_Client_ID,
                    Invoice_Number = x.Invoice_Number,
                    Invoice_Paid = x.Invoice_Paid,
                    Invoice_Consultation = x.Invoice_Consultation,
                    Invoice_Is_Appointment = x.Invoice_Is_Appointment,
                    Invoice_Payment_method = x.Invoice_Payment_method,
                    Invoice_Date = x.Invoice_Date,
                    Invoice_Total = x.Invoice_Total

                };
                invoicerepo.Insert(invoice);
            }
        }
        public static void Update(Invoice_InfoViewModel x)
        {
            using (var invoicerepo = new InvoiceInfoRepository())
            {
                var invoice = invoicerepo.GetById(x.Invoice_Info_ID);

                if (invoice != null)
                {

                    invoice.Invoice_Client_ID = x.Invoice_Client_ID;
                    invoice.Invoice_Number = x.Invoice_Number;
                    invoice.Invoice_Paid = x.Invoice_Paid;
                    invoice.Invoice_Consultation = x.Invoice_Consultation;
                    invoice.Invoice_Is_Appointment = x.Invoice_Is_Appointment;
                    invoice.Invoice_Is_Supplements = x.Invoice_Is_Supplements;
                    invoice.Invoice_Payment_method = x.Invoice_Payment_method;
                    invoice.Invoice_Date = x.Invoice_Date;
                    invoice.Invoice_Total = x.Invoice_Total;
                    invoicerepo.Update(invoice);
                }
            }

        }
        public static void Delete(int id)
        {
            using (var invoicerepo = new InvoiceInfoRepository())
            {
                var invoice = invoicerepo.GetById(id);

                if (invoice != null)
                {
                    invoicerepo.Delete(invoice);
                }
            }

        }



    }
}