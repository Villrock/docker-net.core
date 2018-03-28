using System;
using static System.Globalization.CultureInfo;

namespace QFlow.Extensions
{
    public static class DateTimeExtentions
    {
        public static string GetPrettyDate(this DateTime dateTime)
        {
            var s = DateTime.UtcNow.Subtract(dateTime);

            var dayDiff = ( int )s.TotalDays;
            var secDiff = ( int )s.TotalSeconds;

            if(dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            if (dayDiff == 0)
            {
                if (secDiff < 60)
                {
                    return "just now";
                }
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago", Math.Floor((double) secDiff / 60));
                }
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago", Math.Floor((double) secDiff / 3600));
                }
            }
            else if (dayDiff == 1)
            {
                return string.Format("yesterday at {0}", dateTime.ToString("hh:mm tt", InvariantCulture));
            }
            return dayDiff < 7 
                ? string.Format("{0} days ago at {1}", 
                                dayDiff, 
                                dateTime.ToString("MMM dd hh:mm tt", InvariantCulture)) 
                : string.Format("{0} at {1}", 
                                dateTime.ToString("MMM dd", InvariantCulture), 
                                dateTime.ToString("hh:mm tt", InvariantCulture));
        }
    }
}
