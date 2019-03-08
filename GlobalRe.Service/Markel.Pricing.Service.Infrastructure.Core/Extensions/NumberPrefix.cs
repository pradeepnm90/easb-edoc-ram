using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class NumberPrefix
    {
        private static List<NumberInfo> _numberInfoList = new List<NumberInfo>();

        static NumberPrefix()
        {
            _numberInfoList = new List<NumberInfo>();
            LoadSIPrefix();
        }

        public static List<NumberInfo> SIPrefixInfoList
        {
            get
            {
                NumberInfo[] siPrefixInfoList = new NumberInfo[6];
                _numberInfoList.CopyTo(siPrefixInfoList);
                return siPrefixInfoList.ToList();
            }
        }

        private static void LoadSIPrefix()
        {
            _numberInfoList.AddRange(new NumberInfo[]{           
            new NumberInfo() {Symbol = "B", Prefix = "giga", Example = 1000000000M, ZeroLength = 9, ShortScaleName = "Billion", LongScaleName = "Milliard"},
            new NumberInfo() {Symbol = "M", Prefix = "mega", Example = 1000000M, ZeroLength = 6, ShortScaleName = "Million", LongScaleName = "Million"},
            new NumberInfo() {Symbol = "K", Prefix = "kilo", Example = 1000M, ZeroLength = 3, ShortScaleName = "Thousand", LongScaleName = "Thousand"},
            new NumberInfo() {Symbol = "", Prefix = "", Example = 1M, ZeroLength = 0, ShortScaleName = "One", LongScaleName = "One"}
        });
        }

        public static string FormatNumberString(this decimal amount, int decimals =0)
        {
            return FormatNumber(amount, decimals).AmountWithPrefix;
        }
        public static NumberInfo FormatNumber(this long amount, int decimals=0)
        {
            return FormatNumber(Convert.ToDecimal(amount), decimals);
        }

        public static NumberInfo FormatNumber(this decimal amount, int decimals=0)
        {
            decimal amountToTest = Math.Abs(amount);
            NumberInfo numberInfo =_numberInfoList.Where(i => amountToTest >= i.Example).OrderByDescending(n => n.ZeroLength).FirstOrDefault();
            if(numberInfo == null)
                numberInfo = _numberInfoList.Where(i => i.ZeroLength == 0).OrderByDescending(n => n.ZeroLength).FirstOrDefault();
            decimal num = Math.Round(amountToTest / Convert.ToDecimal(numberInfo.Example), decimals);
            if (decimals > 0)
            {
                decimal valAfterDecimalPt = num - Math.Floor(num);
                if (valAfterDecimalPt == 0)
                    decimals = 0;
            }
            numberInfo.AmountWithPrefix = Math.Round(amountToTest / Convert.ToDecimal(numberInfo.Example), decimals).ToString() + numberInfo.Symbol.ToLower().Trim();
            return numberInfo;            
        }
    }

    public class NumberInfo : ICloneable
    {
        public string Symbol { get; set; }
        public decimal Example { get; set; }
        public string Prefix { get; set; }
        public int ZeroLength { get; set; }
        public string ShortScaleName { get; set; }
        public string LongScaleName { get; set; }
        public string AmountWithPrefix { get; set; }

        public object Clone()
        {
            return new NumberInfo()
            {
                Example = this.Example,
                LongScaleName = this.LongScaleName,
                ShortScaleName = this.ShortScaleName,
                Symbol = this.Symbol,
                Prefix = this.Prefix,
                ZeroLength = this.ZeroLength
            };

        }
    }
}
