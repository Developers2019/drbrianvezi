using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClinicLogist.DAL;
using ClinicLogist.Service.Appointment_Managment;
using ClinicLogist.Service.Client_Management;

namespace ClinicLogist.Models
{
    public class Client_NoteViewModel
    {

        [Display(Name = "Note Id")]
        public int Client_Note_ID { get; set; }

        [Display(Name = "Patient Id")]
        public int? Client_ID { get; set; }

        [Display(Name = "Patient Fullname")]
        public string FullName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Patient Note")]

        public string Note { get; set; }
        [Display(Name = "Appointment Id")]

        public int? Appointment_ID { get; set; }
        [Display(Name = "Reason for cancelling")]

        public string ReasonForCancelling { get; set; }
        [Display(Name = "Appointment Descreption")]

        public string AppointmentDesc { get; set; }

        #region Methods
        public static void Delete(int id)
        {
            using (var clientnoterepo = new ClientNoteRepository())
            {
                Table_Client_Note clientNote = clientnoterepo.GetById(id);
                if (clientNote != null)
                {
                    clientnoterepo.Delete(clientNote);
                }
            }
        }

        public static List<Client_NoteViewModel> GetAll()
        {
            var clientdatarepo = new ClientRepository();
            var appoinmentrepo = new AppointmentRepository();

            using (var clientnoterepo = new ClientNoteRepository())
            {
                var returnlist = clientnoterepo.GetAll().ToList();
                return returnlist.Select(x => new Client_NoteViewModel
                {

                    Client_Note_ID = x.Client_Note_ID,
                    FullName = clientdatarepo.GetAll().Find(s => s.Client_ClientID == x.Client_ID).Client_Name,
                    Note = x.Note


                }).ToList();

            }
        }

        public static Client_NoteViewModel GetById(int id)
        {
            using (var clientnoteRepo = new ClientNoteRepository())
            {
                var clientdatarepo = new ClientRepository();
                var appoinmentrepo = new AppointmentRepository();

                var model = new Client_NoteViewModel();
                Table_Client_Note clientNote = clientnoteRepo.GetById(id);

                if (clientNote != null)
                {
                    model = new Client_NoteViewModel
                    {
                        Client_Note_ID = clientNote.Client_Note_ID,
                        FullName = clientdatarepo.GetAll().Find(s => s.Client_ClientID == clientNote.Client_ID).Client_Name,
                        Note = clientNote.Note
                    };
                }
                return model;
            }
        }

        public static void Insert(Client_NoteViewModel model)
        {


            using (var clientnoteRepo = new ClientNoteRepository())
            {

                var clientNote = new Table_Client_Note
                {
                    Note = model.Note,
                    Client_ID = model.Client_ID


                };
                clientnoteRepo.Insert(clientNote);
            }
        }

        public static void Update(Client_NoteViewModel model)
        {
            using (var clientnoteRepo = new ClientNoteRepository())
            {
                Table_Client_Note clientNote = clientnoteRepo.GetById(model.Client_Note_ID);
                if (clientNote != null)
                {
                    clientNote.Note = model.Note;

                }
                clientnoteRepo.Update(clientNote);
            }
        } 
        #endregion

    }
}