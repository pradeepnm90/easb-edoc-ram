using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class Deal : BaseApiModel<BLL_Deal>
    {
        public Deal() { }
        public Deal(BLL_Deal model) : base(model) { }

        public override BLL_Deal ToBLLModel()
        {
            BLL_Deal bLL_Deal = new BLL_Deal()
            {
                Dealnum = DealNumber,
                Dealname = DealName,
                Status = StatusCode,
                StatusName = Status,
                Contractnum = ContractNumber,
                Inceptdate = InceptionDate.ToDate(),
                Targetdt = TargetDate.ToUniversalDate(),
                ModelPriority = Priority,
                Submissiondate = SubmittedDate.ToDate(),
                Uw1 = PrimaryUnderwriterCode,
                Uw1Name = PrimaryUnderwriterName,
                Uw2 = SecondaryUnderwriterCode,
                Uw2Name = SecondaryUnderwriterName,
                Ta = TechnicalAssistantCode,
                TaName = TechnicalAssistantName,
                Modeller = ModellerCode,
                ModellerName = ModellerName,
                Act1 = ActuaryCode,
                Act1Name = ActuaryName,
                Expirydate = ExpiryDate.ToDate(),
                Broker = BrokerCode,
                BrokerName = BrokerName,
                BrokerContact = BrokerContactCode,
                BrokerContactName = BrokerContactName,
				Cedant = CedantCode,
				CedantName = CedantName,
				Continuous = Continuous,
                Renewal = Renewal,
                Dealtype = Dealtype,
                DealtypeName = DealtypeName,
                Coveragetype = Coveragetype,
                CoveragetypeName = CoveragetypeName,
                Policybasis= Policybasis,
                PolicybasisName = PolicybasisName,
                Currency = Currency,
                CurrencyName = CurrencyName,
                Domicile = Domicile,
                DomicileName = DomicileName,
                Region = Region,
                RegionName = RegionName,
                CedentLocation = CedentLocation,
                CedentLocationname = CedentLocationname,
                BrokerLocation = BrokerLocation,
                BrokerLocationname = BrokerLocationname,
                PaperName = PaperName,
                ExpiryEod = ExpiryEod,
                Exposuretype = Exposuretype,
                CedantGroupCode = CedantGroupCode,
                CedantGroupName = CedantGroupName,
                BrokerGroupCode = BrokerGroupCode,
                BrokerGroupName = BrokerGroupName,
                RenewalName = RenewalName,
                ChkPreBindProcessing = ChkPreBindProcessing,
                ChkUWCompliance = ChkUWCompliance,
                ChkModelling = ChkModelling,
                decreason = decreason
            };

            return bLL_Deal;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_Deal model)
        {
            DealNumber = model.Dealnum;
            DealName = model.Dealname;
            StatusCode = model.Status;
            Status = model.StatusName;
            ContractNumber = model.Contractnum;
            InceptionDate = model.Inceptdate.ToDateOnly();
            TargetDate = model.Targetdt.ToDateOnly();
            Priority = model.ModelPriority;
            SubmittedDate = model.Submissiondate.ToDateOnly();
            PrimaryUnderwriterCode = model.Uw1;
            PrimaryUnderwriterName = model.Uw1Name;
            SecondaryUnderwriterCode = model.Uw2;
            SecondaryUnderwriterName = model.Uw2Name;
            TechnicalAssistantCode = model.Ta;
            TechnicalAssistantName = model.TaName;
            ModellerCode = model.Modeller;
            ModellerName = model.ModellerName;
            ActuaryCode = model.Act1;
            ActuaryName = model.Act1Name;
            ExpiryDate = model.Expirydate.ToDateOnly();
            BrokerCode = model.Broker;
            BrokerName = model.BrokerName;
            BrokerContactCode = model.BrokerContact;
            BrokerContactName = model.BrokerContactName;
			CedantCode = model.Cedant;
			CedantName = model.CedantName;
			Continuous = model.Continuous;
            
            CedentLocation = model.CedentLocation;
            CedentLocationname = model.CedentLocationname;
            BrokerLocation = model.BrokerLocation;
            BrokerLocationname = model.BrokerLocationname;
            PaperName = model.PaperName;
            Renewal = model.Renewal;
            ExpiryEod = model.ExpiryEod;
            Exposuretype = model.Exposuretype;
            
            Dealtype = model.Dealtype;
            DealtypeName = model.DealtypeName;
            Coveragetype = model.Coveragetype;
            CoveragetypeName = model.CoveragetypeName;
            Policybasis = model.Policybasis;
            PolicybasisName = model.PolicybasisName;
            Currency = model.Currency;
            CurrencyName = model.CurrencyName;
            Domicile = model.Domicile;
            DomicileName = model.DomicileName;
            Region = model.Region;
            RegionName = model.RegionName;

            CedantGroupCode = model.CedantGroupCode;
            CedantGroupName = model.CedantGroupName;
            BrokerGroupCode = model.BrokerGroupCode;
            BrokerGroupName = model.BrokerGroupName;
            RenewalName = model.RenewalName;
            ChkUWCompliance = model.ChkUWCompliance;
            ChkPreBindProcessing = model.ChkPreBindProcessing;
            ChkModelling = model.ChkModelling;
            decreason = model.decreason;
        }
    }
}