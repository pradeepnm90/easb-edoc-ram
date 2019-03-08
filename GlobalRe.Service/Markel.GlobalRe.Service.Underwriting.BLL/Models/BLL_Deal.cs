using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_Deal : BaseGlobalReBusinessEntity
    {
        public int Dealnum { get; set; }
        public string Dealname { get; set; } 
        public int? Contractnum { get; set; } 
        public DateTime? Inceptdate { get; set; } 
        public DateTime? Expirydate { get; set; } 
        public bool? ExpiryEod { get; set; } 
        public DateTime? Targetdt { get; set; } 
        public int? ModelPriority { get; set; } 
        public DateTime? Submissiondate { get; set; } 
        public int? Status { get; set; } 
        public string StatusName { get; set; } 
        public int? Uw1 { get; set; } 
        public string Uw1Name { get; set; } 
        public int? Uw2 { get; set; } 
        public string Uw2Name { get; set; } 
        public int? Ta { get; set; } 
        public string TaName { get; set; } 
        public int? Modeller { get; set; } 
        public string ModellerName { get; set; } 
        public int? Act1 { get; set; } 
        public string Act1Name { get; set; } 
        public int? Broker { get; set; } 
        public string BrokerName { get; set; } 
        public int? BrokerContact { get; set; } 
        public string BrokerContactName { get; set; } 
        public string Division { get; set; } 
        public int? Paper { get; set; } 
        public string PaperName { get; set; } 
        public int? Team { get; set; } 
        public string TeamName { get; set; }
		public int? Cedant { get; set; }
		public string CedantName { get; set; }
		public bool? Continuous { get; set; }
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
        public int? BrokerLocation { get; set; } 
        public string BrokerLocationname { get; set; } 
        public int? CedentLocation { get; set; } 
        public string CedentLocationname { get; set; } 
        public int? Exposuretype { get; set; }
        public int? CedantGroupCode { get; set; }
        public string CedantGroupName { get; set; }
        public int? BrokerGroupCode { get; set; }
        public string BrokerGroupName { get; set; }
        public string RenewalName { get; set; }
        public string ChkPreBindProcessing { get; set; }
        public string ChkUWCompliance { get; set; }
        public string ChkModelling { get; set; }

        //Add for GRS-708
        public string decreason { get; set; }

    }
}
