using System;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// Calculates date difference between two dates in Years.
        /// </summary>
        /// <param name="start">Start Date</param>
        /// <param name="end">End Date</param>
        /// <returns>Date difference between two dates in years.</returns>
        public static int Years(this DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) + (((end.Month > start.Month) || ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
 
        public static int NumberOfLeapDays(this DateTime startDate, DateTime endDate)
        {
            int numberOfLeapYears = 0;
            DateTime sDate = startDate;
            if ((sDate <= new DateTime(sDate.Year, 2, 28) && endDate > new DateTime(sDate.Year, 2, 28) && new DateTime(sDate.Year, 2, 1).AddDays(28).Month == 2) ||
            (sDate <= new DateTime(endDate.Year, 2, 28) && endDate > new DateTime(endDate.Year, 2, 28) && new DateTime(endDate.Year, 2, 1).AddDays(28).Month == 2))
            {
                numberOfLeapYears++;
            }
            while ((sDate = sDate.AddYears(1)).Year < endDate.Year)
            {
                if (DateTime.IsLeapYear(sDate.Year))
                    numberOfLeapYears++;
            }
            return numberOfLeapYears;
        }

        public static DateTime? ToDate(this string dateString)
        {
            DateTime date;
            if(DateTime.TryParse(dateString, out date))
            {
                return date;
            }

            return null;
        }

        public static string ToDateOnly(this DateTime? date)
        {
            if (date != null)
            {
                return ((DateTime)date).ToString("yyyy-MM-dd");
            }

            return null;
        }

        public static int TotalMonths(this DateTime start, DateTime end)
        {
            return (end.Year - start.Year) * 12 + (end.Month - start.Month);
        }

        public static TimeSpan? Subtract(this DateTime? end, DateTime? start)
        {
            if (!start.HasValue) return null;
            if (!end.HasValue) return DateTime.Now.Subtract(start.Value);

            return end.Value.Subtract(start.Value);
        }
    }
}
