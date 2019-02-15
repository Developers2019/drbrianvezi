using System;
using System.Web.Mvc;


namespace drbrianvezi_cms.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
        public static string HowLongAgo(this HtmlHelper helper, DateTime? startDate)
        {
            var result = "";
            DateTime now = DateTime.Now;
            if (startDate != null)
            {
                TimeSpan elapsed = now.Subtract((DateTime)startDate);

                double daysAgo = elapsed.TotalDays;
                var resultholder = "";
                if (daysAgo < 1)
                {
                    resultholder = "Today";
                }
                else
                {
                    resultholder = daysAgo.ToString("0") + " days ago";
                }
                 result = resultholder;
            }
            return result;
        }
    }
}
