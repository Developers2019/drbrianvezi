using System.Configuration;

namespace ClinicLogist.Helpers
{
    public static class CmsConfig
    {
        public static string Connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //public static string ExtConnection = ConfigurationManager.ConnectionStrings["RegConnection2"].ConnectionString;
        //public static string EmailTempleteFolder = ConfigurationManager.AppSettings["EmailTempleteFolder"].ToString();
        //public static string SiteUrl = ConfigurationManager.AppSettings["SiteUrl"].ToString();
    }
}