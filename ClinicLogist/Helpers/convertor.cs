using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClinicLogist.Helpers
{
    public static class Convertor
    {
       
        public static double ConvertToDouble(object number)
        {
            double result = Convert.ToDouble("0");
            if (number != null)
            {
                Double.TryParse(number.ToString(), out result);

            }

            return result;
        }
        public static int ConvertToInt(object number)
        {
            int result = 0;
            if (number != null)
            {
                int.TryParse(number.ToString(), out result);
            }

            return result;
        }
        public static DateTime ConvertToDateTime(object datetimeval)
        {

            DateTime result = DateTime.MaxValue;

            if (datetimeval != null)
            {
                DateTime.TryParse(datetimeval.ToString(), out result);
            }

            return result;
        }
        public static string ConvertToDate(object dateval)
        {

            DateTime defaultresult = DateTime.MaxValue;
            string result = defaultresult.ToString("yyyy/MM/dd");

            if (dateval != null)
            {
                DateTime.TryParse(dateval.ToString(), out defaultresult);
                result = defaultresult.ToString("D");
            }

            return result;
        }

        public static bool ConvertToBool(object value)
        {
            bool result = false;
            if (value != null)
            {
                bool.TryParse(value.ToString(), out result);
                if(value.ToString() =="1")
                {
                    result = true;
                }

            }

            return result;
        }

        public static string ConvertToBoolStringDropDown(object value)
        {
            string result = "0";
            if (value != null)
            {
                bool result2 = false;
                bool.TryParse(value.ToString(), out result2);
                if(result2 == true)
                {
                    result = "1";
                }

            }

            return result;
        }

        public static string ConvertToWSDate(string dateval)
        {

            DateTime defaultresult = DateTime.MaxValue;
            string result = defaultresult.ToString("yyyy/MM/dd");

            if (dateval != null)
            {
                defaultresult = DateTime.ParseExact(dateval.Trim(), "yyyyMMdd", null);
                result = defaultresult.ToString("yyyy/MM/dd");
            }

            return result;
        }

        public static string StringLimit(string text ,int length)
        {
            if (text == null || text.Length < length)
            {
                return text;
            }
            else
            {
               //Nearest WOrd
                //int NextSpace = Text.LastIndexOf(" ", Length);
                //return string.Format("{0}...", Text.Substring(0, (NextSpace > 0) ? NextSpace : Length).Trim());

                string result = string.Format("{0}...", text.Substring(0, length));
                return result;
            }

          
        }

		public static Guid ConvertToGuid(string stringGuid)
		{
			Guid result = new Guid("00000000-0000-0000-0000-000000000000");
			if (stringGuid != null)
			{
				Guid.TryParse(stringGuid, out result);
			}

			return result;
		}

        //public static string HowLongAgo(DateTime theTime)
        //{
        //    var ts = new TimeSpan(DateTime.UtcNow.Ticks - theTime.Ticks);
        //    double delta = Math.Abs(ts.TotalSeconds);

        //    if (delta < 60)
        //    {
        //        return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
        //    }
        //    if (delta < 120)
        //    {
        //        return "a minute ago";
        //    }
        //    if (delta < 2700) // 45 * 60
        //    {
        //        return ts.Minutes + " minutes ago";
        //    }
        //    if (delta < 5400) // 90 * 60
        //    {
        //        return "an hour ago";
        //    }
        //    if (delta < 86400) // 24 * 60 * 60
        //    {
        //        return ts.Hours + " hours ago";
        //    }
        //    if (delta < 172800) // 48 * 60 * 60
        //    {
        //        return "yesterday";
        //    }
        //    if (delta < 2592000) // 30 * 24 * 60 * 60
        //    {
        //        return ts.Days + " days ago";
        //    }
        //    if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
        //    {
        //        int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
        //        return months <= 1 ? "one month ago" : months + " months ago";
        //    }
        //    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
        //    return years <= 1 ? "one year ago" : years + " years ago";
        //}
        public static string SaveCroppedImage(Image image, int maxWidth, int maxHeight, string filePath)
        {
            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders()
                .Where(codecInfo =>
                    codecInfo.MimeType == "image/jpeg").First();
            Image finalImage = image;
            Bitmap bitmap = null;
            try
            {
                int left = 0;
                int top = 0;
                int srcWidth = maxWidth;
                int srcHeight = maxHeight;
                bitmap = new System.Drawing.Bitmap(maxWidth, maxHeight);
                double croppedHeightToWidth = (double)maxHeight / maxWidth;
                double croppedWidthToHeight = (double)maxWidth / maxHeight;

                if (image.Width > image.Height)
                {
                    srcWidth = (int)(Math.Round(image.Height * croppedWidthToHeight));
                    if (srcWidth < image.Width)
                    {
                        srcHeight = image.Height;
                        left = (image.Width - srcWidth) / 2;
                    }
                    else
                    {
                        srcHeight = (int)Math.Round(image.Height * ((double)image.Width / srcWidth));
                        srcWidth = image.Width;
                        top = (image.Height - srcHeight) / 2;
                    }
                }
                else
                {
                    srcHeight = (int)(Math.Round(image.Width * croppedHeightToWidth));
                    if (srcHeight < image.Height)
                    {
                        srcWidth = image.Width;
                        top = (image.Height - srcHeight) / 2;
                    }
                    else
                    {
                        srcWidth = (int)Math.Round(image.Width * ((double)image.Height / srcHeight));
                        srcHeight = image.Height;
                        left = (image.Width - srcWidth) / 2;
                    }
                }
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    new Rectangle(left, top, srcWidth, srcHeight), GraphicsUnit.Pixel);
                }
                finalImage = bitmap;
            }
            catch { }
            try
            {
                using (EncoderParameters encParams = new EncoderParameters(1))
                {
                    encParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)100);
                    //quality should be in the range 
                    //[0..100] .. 100 for max, 0 for min (0 best compression)
                    finalImage.Save(filePath, jpgInfo, encParams);

                    return filePath;
                }
            }
            catch { }
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            return "";
        }
        public static string LimitText(string text, int length)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > length)
            {
                string limit = text.Substring(0, length);
                return limit + "...";
            }

            return text;
        }
        public static string RemoveHTMLElements(string text, int length, int length2)
        {
            if (length2 > length)
            {
                throw new Exception("Start length cannot be higher than end lenght");
            }
            if (!string.IsNullOrEmpty(text) && text.Length > length)
            {
                string replace = Regex.Replace(text, "<.*?>", String.Empty);

                string limit = replace.Substring(length2, length);

                if (replace.Length < length)
                {
                    return replace.Substring(length2, replace.Length);
                }
                if (replace.Length < length2)
                {
                    return replace.Substring(replace.Length, replace.Length);
                }
                return limit;
            }

            return text;
        }

        public static string HowLongAgo(DateTime startDate)
        {
            var result = "";
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now.Subtract(startDate);

            double daysAgo = elapsed.TotalDays;

             result = daysAgo.ToString("0") + " days ago";
            return result;
        }
        
        
    }
}
