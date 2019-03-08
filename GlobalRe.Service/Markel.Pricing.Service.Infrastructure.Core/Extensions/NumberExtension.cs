using System;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class NumberExtension
    {
        public static decimal RoundedValue(this decimal value, int decimals = 6)
        {
            return decimal.Round(value, decimals);
        }

        public static decimal? RoundedValue(this decimal? value, int decimals = 6)
        {
            if (value == null) return null;

            //TODO : Need to check if Rounding needs to be specified
            //MidpointRounding.ToEven : When a number is halfway between two others, it is rounded toward the nearest even number.
            // return decimal.Round((decimal)value, decimals, MidpointRounding.ToEven);
            return value.Value.RoundedValue(decimals);
        }

        public static double RoundedValue(this double value, int digits = 6)
        {
            return Math.Round(value, digits);
        }

        public static double? RoundedValue(this double? value, int digits = 6)
        {
            if (value == null) return null;

            return value.Value.RoundedValue(digits);
        }

        public static decimal? Sum(params decimal?[] values) { return values.Sum(); }

        public static double? Sum(params double?[] values) { return values.Sum(); }

        public static float? Sum(params float?[] values) { return values.Sum(); }

        public static long? Sum(params long?[] values) { return values.Sum(); }

        public static int? Sum(params int?[] values) { return values.Sum(); }
    }
}
