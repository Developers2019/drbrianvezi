using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace drbrianvezi_cms.Helpers
{
    public static class Helper
    {

      

        public static decimal ConvertCM(decimal height)
        {
            decimal m = 0;
            var cm = height;
            if (cm != 0)
            {
                m = cm / 100;
            }
            return m;
        }
        public static string YesorNo(bool? answer)
        {
            if (answer == null)
            {
                answer = false;
            }

            return (bool)answer ? "Yes" : "No";
        }

        private static readonly Random Getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (Getrandom) // synchronize
            {
                return Getrandom.Next(min, max);
            }
        }
    }
}