using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Underwriting.Service.BLL.Models
{
    public class BLL_DealSubmissions : BaseGlobalReBusinessEntity
    {
        public int Dealnum { get; set; }
        public int? Cedant { get; set; } 
        public string CedantName { get; set; } 
        public string Dealname { get; set; }
        public int? Broker { get; set; }
        public string BrokerName { get; set; }
        public int? BrokerContact { get; set; }
        public string BrokerContactName { get; set; }
        public int? Paper { get; set; }
        public string PaperName { get; set; }
        public DateTime? Submissiondate { get; set; }
        public DateTime? Targetdt { get; set; }
        public bool? Continuous { get; set; }
        public bool? ExpiryEod { get; set; }
        public DateTime? Inceptdate { get; set; }
        public DateTime? Expirydate { get; set; }
        public int? Renewal { get; set; }
        public int? Dealtype { get; set; }
        public string DealtypeName { get; set; }
        public int? Coveragetype { get; set; }
        public string CoveragetypeName { get; set; }
        public int? Policybasis { get; set; }
        public string PolicybasisName { get; set; }
        public int? Currency { get; set; }
        public string CurrencyName { get; set; }
        public int? Domicile { get; set; }
        public string DomicileName { get; set; }
        public int? Region { get; set; }
        public string RegionName { get; set; }
    }
}
