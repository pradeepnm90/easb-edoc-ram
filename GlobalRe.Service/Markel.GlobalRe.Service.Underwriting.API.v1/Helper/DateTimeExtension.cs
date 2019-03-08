using System;
using System.Configuration;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Helper
{
    //TODO : This is just added to override default date format specified in core framework. (Remove once updated in core framework)
    public static class DateTimeExtension
    {
        private const string DEFAULT_DATE_FORMAT = "MM-dd-yyyy";
        public static string ToDateOnly(this DateTime? date)
        {
            if (date != null)
            {
                return ((DateTime)date).ToString(ConfigurationManager.AppSettings["ApplicationDateFormat"] ?? DEFAULT_DATE_FORMAT);
            }

            return null;
        }

        public static DateTime? ToDate(this string dateString)
        {
            DateTime date;
            if (DateTime.TryParse(dateString, out date))
            {
                return date;
            }

            return null;
        }

        public static DateTime? ToUniversalDate(this string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
                return DateTime.Parse(dateString).ToUniversalTime();
            return null;
        }
    }
}