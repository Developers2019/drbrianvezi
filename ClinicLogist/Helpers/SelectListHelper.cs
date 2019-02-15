using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ClinicLogist.Models;
using drbrianvezi_cms.Helpers;

namespace ClinicLogist.Helpers
{
    //This class is where we create drop down lists
    public class SelectListOptions
    {
        public decimal IdDec { get; set; }
        public string FullText { get; set; }
        public int? NullableInt { get; set; }
        public bool? NullableBool { get; set; }
        public int Id { get; set; }
    }
    public static class SelectListHelper
    {
        private static List<SelectListItem> CreateSelectListItems<T>(IList<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            List<SelectListItem> items = entities
                    .Select(x => new SelectListItem
                    {
                        Value = funcToGetValue(x).ToString(),
                        Text = funcToGetText(x).ToString()
                    }).ToList();

            return items;
        }

        private static SelectList CreateSelectList<T>(IList<T> entities, Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            var items = CreateSelectListItems(entities, funcToGetValue, funcToGetText);

            return new SelectList(items, "Value", "Text");
        }

        public static SelectList CreateFromEnum<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var items = new List<SelectListItem>();

            foreach (var listItem in Enum.GetValues(typeof(TEnum)))
            {
                items.Add(new SelectListItem
                {
                    Text = listItem.ToString().Depascalise(),
                    Value = Convert.ToInt32(listItem).ToString()
                });
            }

            return CreateSelectList(items, i => i.Value, i => i.Text);
        }
        
        public static SelectList GetClients()
        {
            List<ClientDataViewModel> a = ClientDataViewModel.GetClients().ToList();
            var clients = new List<SelectListOptions> {new SelectListOptions() {IdDec = 0, FullText = "Select a client"}};
            clients.AddRange(a.Select(x => new SelectListOptions()
            {
                FullText = x.FullName,
                Id = (int)x.Client_ClientID
            }));
          
            return CreateSelectList(clients, x => x.IdDec, x => x.FullText);
        }
        public static SelectList GetSlots()
        {
            List<Appointment_SlotViewModel> a = Appointment_SlotViewModel.GetAppointmentSlots().Where(x => x.Appointment_Slot_Date != null && (x.Appointment_Slot_Date.Value.Date >= DateTime.Now.Date && x.Booked != true)).OrderBy(x => x.Appointment_Slot_Date).ToList();
            var clients = new List<SelectListOptions> {new SelectListOptions() {IdDec = 0, FullText = "Select a date"}};
            if (a!=null)
            {
                clients.AddRange(a.Select(x => new SelectListOptions()
                {
                    FullText = $"{String.Format("{0:dddd, MMM d}", x.Appointment_Slot_Date)}  - {String.Format("{0:t}", x.Appointment_Slot_Start)} to {String.Format("{0:t}", x.Appointment_Slot_End)}",
                    Id = x.Appointment_SLot_ID
                }));
            }
           
          
            return CreateSelectList(clients, x => x.Id, x => x.FullText);
        }
        public static SelectList GetArticles()
        {
            List<BlogPostViewModel> a = BlogPostViewModel.GetAll();
            var clients = new List<SelectListOptions> {new SelectListOptions() {IdDec = 0, FullText = "Select an article"}};
            if (a!=null)
            {
                clients.AddRange(a.Select(x => new SelectListOptions()
                {
                    FullText = x.SearchParam,
                    Id = x.BlogPost_ID
                }));
            }
           
          
            return CreateSelectList(clients, x => x.Id, x => x.FullText);
        }

        public static SelectList GetTimeLists()
        {
            var timeLists = new List<SelectListOptions>
            {
                new SelectListOptions() {IdDec = -999, FullText = "---Select Option---"}
            };
            DateTime today = DateTime.Now.Date;
            while (today.Date < DateTime.Now.Date.AddDays(1))
            {
                decimal minutes = (decimal)today.TimeOfDay.Minutes / 100;
                decimal timeValue = today.TimeOfDay.Hours + (today.TimeOfDay.Minutes > 0 ? minutes : 0);
                var timeList = new SelectListOptions()
                {
                    IdDec = timeValue,
                    FullText = today.ToString("HH:mm", CultureInfo.InvariantCulture),
                };
                timeLists.Add(timeList);
                today = today.AddMinutes(30);
            }

            return CreateSelectList(timeLists, x => x.IdDec, x => x.FullText);
        }
        
        public static SelectList GetHoursList()
        {
            var hoursList = new List<SelectListOptions>();
            hoursList.Add(new SelectListOptions() { IdDec = -999, FullText = "---Select Option---" });
            hoursList.AddRange(new List<SelectListOptions>()
                        {
                                new SelectListOptions() { IdDec = 0, FullText = "00" },
                                new SelectListOptions() { IdDec = 1, FullText = "01" },
                                new SelectListOptions() { IdDec = 2, FullText = "02" },
                                new SelectListOptions() { IdDec = 3, FullText = "03" },
                                new SelectListOptions() { IdDec = 4, FullText = "04" },
                                new SelectListOptions() { IdDec = 5, FullText = "05" },
                                new SelectListOptions() { IdDec = 6, FullText = "06" },
                                new SelectListOptions() { IdDec = 7, FullText = "07" },
                                new SelectListOptions() { IdDec = 8, FullText = "08" },
                                new SelectListOptions() { IdDec = 9, FullText = "09" },
                                new SelectListOptions() { IdDec = 10, FullText = "10" },
                                new SelectListOptions() { IdDec = 11, FullText = "11" },
                                new SelectListOptions() { IdDec = 12, FullText = "12" },
                                new SelectListOptions() { IdDec = 13, FullText = "13" },
                                new SelectListOptions() { IdDec = 14, FullText = "14" },
                                new SelectListOptions() { IdDec = 15, FullText = "15" },
                                new SelectListOptions() { IdDec = 16, FullText = "16" },
                                new SelectListOptions() { IdDec = 17, FullText = "17" },
                                new SelectListOptions() { IdDec = 18, FullText = "18" },
                                new SelectListOptions() { IdDec = 19, FullText = "19" },
                                new SelectListOptions() { IdDec = 20, FullText = "20" },
                                new SelectListOptions() { IdDec = 21, FullText = "21" },
                                new SelectListOptions() { IdDec = 22, FullText = "22" },
                                new SelectListOptions() { IdDec = 23, FullText = "23" },
                        });
            return CreateSelectList(hoursList, x => x.IdDec, x => x.FullText);
        }
        //For old and new
        public static SelectList GetMinutesList()
        {
            var minutesList = new List<SelectListOptions>();
            minutesList.Add(new SelectListOptions() { IdDec = -999, FullText = "---Select Option---" });
            minutesList.AddRange(new List<SelectListOptions>()
                        {
                                new SelectListOptions() { IdDec = 0, FullText = "00" },
                                new SelectListOptions() { IdDec = 5, FullText = "05" },
                                new SelectListOptions() { IdDec = 10, FullText = "10" },
                                new SelectListOptions() { IdDec = 15, FullText = "15" },
                                new SelectListOptions() { IdDec = 20, FullText = "20" },
                                new SelectListOptions() { IdDec = 25, FullText = "25" },
                                new SelectListOptions() { IdDec = 30, FullText = "30" },
                                new SelectListOptions() { IdDec = 35, FullText = "35" },
                                new SelectListOptions() { IdDec = 40, FullText = "40" },
                                new SelectListOptions() { IdDec = 45, FullText = "45" },
                                new SelectListOptions() { IdDec = 50, FullText = "50" },
                                new SelectListOptions() { IdDec = 55, FullText = "55" },
                        });
            return CreateSelectList(minutesList, x => x.IdDec, x => x.FullText);
        }
        
        
        public static SelectList GetNumberOfDays()
        {
            var numOfDays = new List<SelectListOptions>();
            //numOfDays.Add(new DecimalString() { Id = 0, FullName = "7 days"});
            numOfDays.AddRange(new List<SelectListOptions>()
                        {
                                new SelectListOptions() { IdDec = 1, FullText = "1 day"},
                                new SelectListOptions() { IdDec = 2, FullText = "2 days"},
                                new SelectListOptions() { IdDec = 3, FullText = "3 days"},
                                new SelectListOptions() { IdDec = 4, FullText = "4 days"},
                                new SelectListOptions() { IdDec = 5, FullText = "5 days"},
                                new SelectListOptions() { IdDec = 6, FullText = "6 days"},
                                new SelectListOptions() { IdDec = 7, FullText = "7 days"},
                                new SelectListOptions() { IdDec = 8, FullText = "8 days"},
                                new SelectListOptions() { IdDec = 9, FullText = "9 days"},
                                new SelectListOptions() { IdDec = 10, FullText = "10 days"},
                                new SelectListOptions() { IdDec = 11, FullText = "11 days"},
                                new SelectListOptions() { IdDec = 12, FullText = "12 days"},
                                new SelectListOptions() { IdDec = 13, FullText = "13 days"},
                                new SelectListOptions() { IdDec = 14, FullText = "14 days"},
                        });

            return CreateSelectList(numOfDays, x => x.IdDec, x => x.FullText);
        }

    }
}