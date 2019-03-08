using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Traits;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Traits
{
    public class DealSearchCriteria : APISearchCriteria
    {
        #region Private Variable

        private Dictionary<string, string> _propertyMappingCollection = new Dictionary<string, string>();

        #endregion

        public int Status { get; set; }
		public string StatusName { get; set; }
		public int? StatusCode { get; set; }
        public string StatusCodes { get; set; }
        public bool GlobalReData { get; set; }
        public string SubDivisions { get; set; }
        public string ProductLines { get; set; }
        public string ExposureGroups { get; set; }
        public string Exposuretypes { get; set; }
        public string PersonIds { get; set; }


        protected override void OnUpdateFilterParams(List<FilterParameter> filterParams)
        {
			filterParams.AddIf("Status", Status);
			filterParams.AddIf("StatusName", StatusName);
			filterParams.AddIf("StatusCodes", StatusCodes);
            filterParams.AddIf("GlobalReData", GlobalReData);
            filterParams.AddIf("SubDivisions", SubDivisions);
            filterParams.AddIf("ProductLines", ProductLines);
            filterParams.AddIf("ExposureGroups", ExposureGroups);
            filterParams.AddIf("Exposuretypes", Exposuretypes);
            filterParams.AddIf("PersonIds", PersonIds);
        }

        /*
        //Note : Excluded below method as we're doing client side filtering/sorting
        [ExcludeFromCodeCoverage]
        protected override Dictionary<string, string> PropertyMappingCollection
        {
            get
            {
                if (_propertyMappingCollection.Count == 0)
                {
                    _propertyMappingCollection.Add("dealNumber", "Dealnum");
                    _propertyMappingCollection.Add("dealName", "Dealname");
                    _propertyMappingCollection.Add("statusCode", "Status");
                    _propertyMappingCollection.Add("status", "StatusName");
                    _propertyMappingCollection.Add("contractNumber", "Contractnum");
                    _propertyMappingCollection.Add("inceptionDate", "Inceptdate");
                    _propertyMappingCollection.Add("targetDate", "Targetdt");
                    _propertyMappingCollection.Add("priority", "ModelPriority");
                    _propertyMappingCollection.Add("submittedDate", "Submissiondate");
                    _propertyMappingCollection.Add("primaryUnderwriterCode", "Uw1");
                    _propertyMappingCollection.Add("primaryUnderwriterName", "Uw1Name");
                    _propertyMappingCollection.Add("secondaryUnderwriterCode", "Uw2");
                    _propertyMappingCollection.Add("secondaryUnderwriterName", "Uw2Name");
                    _propertyMappingCollection.Add("technicalAssistantCode", "Ta");
                    _propertyMappingCollection.Add("technicalAssistantName", "TaName");
                    _propertyMappingCollection.Add("modellerCode", "Modeller");
                    _propertyMappingCollection.Add("modellerName", "ModellerName");
                    _propertyMappingCollection.Add("actuaryCode", "Act1");
                    _propertyMappingCollection.Add("actuaryName", "Act1Name");
                    _propertyMappingCollection.Add("expiryDate", "Expirydate");
                    _propertyMappingCollection.Add("brokerCode", "Broker");
                    _propertyMappingCollection.Add("brokerName", "BrokerName");
                    _propertyMappingCollection.Add("brokerContactCode", "BrokerContact");
                    _propertyMappingCollection.Add("brokerContactName", "BrokerContactName");
					_propertyMappingCollection.Add("cedantCode", "Cedant");
					_propertyMappingCollection.Add("cedantName", "CedantName");
					_propertyMappingCollection.Add("continuous", "Continuous");
                    _propertyMappingCollection.Add("CedentLocation", "CedentLocation");
                    _propertyMappingCollection.Add("CedentLocationname", "CedentLocationname");
                    _propertyMappingCollection.Add("brokerLocationCode", "BrokerLocation");
                    _propertyMappingCollection.Add("brokerLocationName", "BrokerLocationname");
                    _propertyMappingCollection.Add("PaperName", "PaperName");
                    _propertyMappingCollection.Add("ExpiryEod", "ExpiryEod");
                    _propertyMappingCollection.Add("Exposuretype", "Exposuretype");
                    _propertyMappingCollection.Add("renewal", "Renewal");
                    _propertyMappingCollection.Add("dealtype", "Dealtype");
                    _propertyMappingCollection.Add("dealtypeName", "DealtypeName");
                    _propertyMappingCollection.Add("coveragetype", "Coveragetype");
                    _propertyMappingCollection.Add("coveragetypeName", "CoveragetypeName");
                    _propertyMappingCollection.Add("policybasis", "Policybasis");
                    _propertyMappingCollection.Add("policybasisName", "PolicybasisName");
                    _propertyMappingCollection.Add("currency", "Currency");
                    _propertyMappingCollection.Add("currencyName", "CurrencyName");
                    _propertyMappingCollection.Add("domicile", "Domicile");
                    _propertyMappingCollection.Add("domicileName", "DomicileName");
                    _propertyMappingCollection.Add("region", "Region");
                    _propertyMappingCollection.Add("regionName", "RegionName");
                }
				return _propertyMappingCollection;
            }
        }
      */
    }
}