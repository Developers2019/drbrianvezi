using ClinicLogist.Models;
using System;
using System.Collections.Generic;

namespace DrbrianVezi.Models
{
    public class HomePageViewModel
    {
        public string TradingHoursStatus { get; set; }
        public bool LoadGallery { get; set; }
        public List<BlogPostViewModel> BlogPostViewModels { get; set; }

        public static HomePageViewModel GetHomePageValues()
        {
            var viewModel = new HomePageViewModel();
            if (((int)DateTime.Now.DayOfWeek >= 1  && (int)DateTime.Now.DayOfWeek <= 4) && (DateTime.Now.Hour <= 16 && DateTime.Now.Hour >= 8))
            {
                viewModel.TradingHoursStatus = "Opened";
            }
            else if((int)DateTime.Now.DayOfWeek == 5 && (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 14))
            {
                viewModel.TradingHoursStatus = "Opened";
            }
            else
            {
                viewModel.TradingHoursStatus = "Closed";
            }
            
            return viewModel;
        }
    }
}