using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    public static class TableUtility
    {
        #region Column Name from Number

        private static Dictionary<int, string> ColumNameFromNumberCache = new Dictionary<int, string>();

        public static string GetColumnNameFromNumber(int columnNumber)
        {
            string columnName = string.Empty;

            if (ColumNameFromNumberCache.ContainsKey(columnNumber))
            {
                columnName = ColumNameFromNumberCache[columnNumber];
            }
            else
            {
                int dividend = columnNumber;
                int modulo;

                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    columnName = (char)(65 + modulo) + columnName;
                    dividend = (int)Decimal.Floor((dividend - modulo) / 26);
                }

                // Thread Safety
                lock (ColumNameFromNumberCache)
                {
                    if (!ColumNameFromNumberCache.ContainsKey(columnNumber))
                    {
                        ColumNameFromNumberCache.Add(columnNumber, columnName);
                    }
                }
            }

            return columnName;
        }

        #endregion Column Name from Number
    }
}
