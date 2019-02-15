using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClinicLogist.Models;

namespace DrbrianVezi.Controllers
{
    [Authorize]
    public class BiographicalCapturingController : MultiController
    {
        // GET: BiographicalCapturing
        #region ClientData
        [HttpGet]
        public ActionResult Index()
        {
           
            List<ClientDataViewModel> viewModels = ClientDataViewModel.GetClients().OrderByDescending(x=> x.CapturedDateTime).ToList();
            IOrderedEnumerable<ClientDataViewModel> patientList = viewModels.OrderByDescending(x => x.CapturedDateTime);
            return View(patientList);
        }

        // GET: Table_ClientData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDataViewModel tableClientData = ClientDataViewModel.GetClient((int)id);
            if (tableClientData == null)
            {
                return HttpNotFound();
            }
            return View(tableClientData);
        }

        // GET: Table_ClientData/Create
        public ActionResult Create()
        {
              
                return View();
                  
        }

        // POST: Table_ClientData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientDataViewModel tableClientData)
        {
            
            if (!ModelState.IsValid)
            {
                return View(tableClientData);
            
            }
            try
            {
                var clientId= ClientDataViewModel.Insert(tableClientData);
                ViewBag.ClientId = clientId;
                return RedirectToAction("CreateMemberDetails", new { id = clientId });
                

            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(tableClientData);
            }
           
        }
        public ActionResult ClientAddedConfirmation()
        {
            return View();
        }
        
        // GET: Table_ClientData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientDataViewModel tableClientData = ClientDataViewModel.GetClient((int)id);
            MemberDetailsViewModel member = MemberDetailsViewModel.GetAll().Find(x => x.ClientId == (int)id);
            MedicalAidViewModel med = MedicalAidViewModel.GetAll().Find(x => x.Client_Id == (int)id);
            NextOfKinViewModel kin = NextOfKinViewModel.GetAll().Find(x => x.Client_Id == (int)id);


            var model = new ClientDataViewModel.PatientFullDetailsViewModel
            {
                Patient=tableClientData,
                Member=member,
                Medical=med,
                Kin=kin
                
            };

            if (tableClientData == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Table_ClientData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientDataViewModel.PatientFullDetailsViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model.Patient);
               
            }
            try
            {
                              
                MemberDetailsViewModel.Update(model.Member);
                MedicalAidViewModel.Update(model.Medical);
                NextOfKinViewModel.Update(model.Kin);
                ClientDataViewModel.Update(model.Patient);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                SetViewError(ex);
                return View(model.Patient);
            }

        }

        // GET: Table_ClientData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDataViewModel tableClientData = ClientDataViewModel.GetClient((int)id);

            if (tableClientData == null)
            {
                return HttpNotFound();
            }
            return View(tableClientData);
        }

        // POST: Table_ClientData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientDataViewModel tableClientData = ClientDataViewModel.GetClient(id);

            AppointmentViewModel appointmentClient= AppointmentViewModel.GetAppointments().Find(x => x.Client_ID == tableClientData.Client_ClientID);
            MemberDetailsViewModel member = MemberDetailsViewModel.GetAll().Find(x => x.ClientId == id);
            MedicalAidViewModel med = MedicalAidViewModel.GetAll().Find(x => x.Client_Id == id);
            NextOfKinViewModel kin = NextOfKinViewModel.GetAll().Find(x => x.Client_Id == id);

            if (appointmentClient != null || member!=null || med!=null || kin!=null)
            {
                AppointmentViewModel.Delete(appointmentClient.Appointment_ID);
                MemberDetailsViewModel.Delete(member.MemberDetailId);
                MedicalAidViewModel.Delete(med.Med_Id);
                NextOfKinViewModel.Delete(kin.Kin_Id);

            }

            ClientDataViewModel.Delete((int)tableClientData.Client_ClientID);
            return RedirectToAction("Index");
        }
        #endregion
        #region MemberDetails


        // GET: MemberDetails/Create
        public ActionResult CreateMemberDetails(int?id)
        {
            ViewBag.ClientId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberDetailsViewModel memberDetail = new MemberDetailsViewModel
            {
                ClientId = id
            };
            return View(memberDetail);
        }

        // POST: MemberDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("CreateMemberDetails")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberDetailsViewModel memberDetail)
        {
            if (ModelState.IsValid)
            {

                MemberDetailsViewModel.Insert(memberDetail);
                return RedirectToAction("CreateMedicalAid",new { id=memberDetail.ClientId});
            }

            
            return View(memberDetail);
        }
            

        #endregion
        #region Medical Aid

        public ActionResult CreateMedicalAid(int?id)
        {
            ViewBag.ClientId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalAidViewModel medical = new MedicalAidViewModel
            {
                Client_Id = id
            };
            return View(medical);
        }

        // POST: Medical_Aid_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("CreateMedicalAid")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalAidViewModel medical_Aid_Details)
        {
            if (ModelState.IsValid)
            {
                MedicalAidViewModel.Insert(medical_Aid_Details);
                return RedirectToAction("CreateKin",new { id=medical_Aid_Details.Client_Id});
            }
            return View(medical_Aid_Details);
        }



        #endregion
        #region NextOfKin

        public ActionResult CreateKin(int?id)
        {
            ViewBag.ClientId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKinViewModel model = new NextOfKinViewModel
            {
                Client_Id = id
            };
            return View(model);
        }

        // POST: Next_Of_Kin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("CreateKin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NextOfKinViewModel next_Of_Kin)
        {
            if (ModelState.IsValid)
            {
                NextOfKinViewModel.Insert(next_Of_Kin);
                return RedirectToAction("ClientAddedConfirmation");
            }
            return View(next_Of_Kin);
        }






        #endregion

    }
}