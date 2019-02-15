using System.Collections.Generic;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Supplier_Management;

namespace ClinicLogist.Models
{
    public class SupplierViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierViewModel()
        {
            Supplement = new HashSet<Table_Supplement>();
        }

        public int Supplier_ID { get; set; }
        public string Supplier_Contact_Person { get; set; }
        public string Supplier_Cell { get; set; }
        public string Supplier_Fax { get; set; }
        public string Supplier_Email { get; set; }
        public string Supplier_Bank { get; set; }
        public string Supplier_Branch_Code { get; set; }
        public string Supplier_Account_Number { get; set; }
        public string Supplier_Type_Of_Account { get; set; }
        public string Supplier_Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table_Supplement> Supplement { get; set; }


        public static List<SupplierViewModel> GetSuppliers()
        {

            using (var supplierRepo = new SupplierRepository())
            {
                return supplierRepo.GetAll().ToList().Select(x => new SupplierViewModel
                {
                    Supplier_ID=x.Supplier_ID,
                    Supplier_Bank=x.Supplier_Bank,
                    Supplier_Cell=x.Supplier_Cell,
                    Supplier_Account_Number=x.Supplier_Account_Number,
                    Supplier_Branch_Code=x.Supplier_Branch_Code,
                    Supplier_Comments=x.Supplier_Comments,
                    Supplier_Contact_Person=x.Supplier_Contact_Person,
                    Supplier_Email=x.Supplier_Email,
                    Supplier_Fax=x.Supplier_Fax,
                    Supplier_Type_Of_Account=x.Supplier_Type_Of_Account


                }).ToList();

            }


        }


        public static void Insert(SupplierViewModel model)
        {
            using (var supplierRepo = new SupplierRepository())
            {
                var supplier = new Table_Supplier
                {
                    Supplier_Bank = model.Supplier_Bank,
                    Supplier_Cell = model.Supplier_Cell,
                    Supplier_Account_Number = model.Supplier_Account_Number,
                    Supplier_Branch_Code = model.Supplier_Branch_Code,
                    Supplier_Comments = model.Supplier_Comments,
                    Supplier_Contact_Person = model.Supplier_Contact_Person,
                    Supplier_Email = model.Supplier_Email,
                    Supplier_Fax = model.Supplier_Fax,
                    Supplier_Type_Of_Account = model.Supplier_Type_Of_Account



                };
                supplierRepo.Insert(supplier);
            }
        }

        public static SupplierViewModel GetSupplier(int id)
        {
            using (var supplierRepo = new SupplierRepository())
            {
                var model = new SupplierViewModel();
                Table_Supplier supplier = supplierRepo.GetById(id);
                if (supplier != null)
                {
                    model = new SupplierViewModel()
                    {
                        Supplier_ID = supplier.Supplier_ID,
                        Supplier_Bank = supplier.Supplier_Bank,
                        Supplier_Cell = supplier.Supplier_Cell,
                        Supplier_Account_Number = supplier.Supplier_Account_Number,
                        Supplier_Branch_Code = supplier.Supplier_Branch_Code,
                        Supplier_Comments = supplier.Supplier_Comments,
                        Supplier_Contact_Person = supplier.Supplier_Contact_Person,
                        Supplier_Email = supplier.Supplier_Email,
                        Supplier_Fax = supplier.Supplier_Fax,
                        Supplier_Type_Of_Account = supplier.Supplier_Type_Of_Account

                    };

                }
                return model;

            }
        }

        public static void Update(SupplierViewModel model)
        {
            using (var supplierRepo = new SupplierRepository())
            {
                int id = model.Supplier_ID;
                Table_Supplier supplier = supplierRepo.GetById(id);
                if (supplier != null)
                {

                    supplier.Supplier_Bank = model.Supplier_Bank;
                    supplier.Supplier_Cell = model.Supplier_Cell;
                    supplier.Supplier_Account_Number = model.Supplier_Account_Number;
                    supplier.Supplier_Branch_Code = model.Supplier_Branch_Code;
                    supplier.Supplier_Comments = model.Supplier_Comments;
                    supplier.Supplier_Contact_Person = model.Supplier_Contact_Person;
                    supplier.Supplier_Email = model.Supplier_Email;
                    supplier.Supplier_Fax = model.Supplier_Fax;
                    supplier.Supplier_Type_Of_Account = model.Supplier_Type_Of_Account;

                    supplierRepo.Update(supplier);
                }

            }

        }

        public static void Delete(int id)
        {
            using (var supplierRepo = new SupplierRepository())
            {
                Table_Supplier supplier = supplierRepo.GetById(id);
                if (supplier != null)
                {
                    supplierRepo.Delete(supplier);
                }
            }
        }


    }
}