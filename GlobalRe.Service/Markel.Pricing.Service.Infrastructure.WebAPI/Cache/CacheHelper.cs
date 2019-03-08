using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Cache
{
    public class CacheHelper
    {

        #region Cache Key Lookup

        public static class KeyName
        {
            public static readonly string BreezeMetaData = "BreezeMetaData";
            public static readonly string BusinessUnits = "BusinessUnits";
            public static readonly string CalculationMethodTypes = "CalculationMethodTypes";
            public static readonly string Carriers = "Carriers";
            public static readonly string CatalogItems = "CatalogItems";
            public static readonly string ConfigSettingsByPricingModel = "ConfigSettingsByPricingModel";
            public static readonly string ConfigSettingViewByPricingModel = "ConfigSettingViewByPricingModel";
            public static readonly string CoverageBasis = "CoverageBasis";
            public static readonly string Currencies = "Currencies";
            public static readonly string DataSource = "DataSource";
            public static readonly string DefaultGridFilter = "DefaultGridFilter";
            public static readonly string DefaultUnderlyingCoverageDistribution = "DefaultUnderlyingCoverageDistribution";
            public static readonly string DevelopmentPatterns = "DevelopmentPatterns";
            public static readonly string GeoRegions = "GeoRegions";
            public static readonly string GeoRegionsByType = "GeoRegionsByType";
            public static readonly string HazardGroups = "HazardGroups";
            public static readonly string ImportTypes = "ImportTypes";
            public static readonly string IntegrationActivityMappings = "IntegrationActivityMappings";
            public static readonly string IntegrationSectionMappingsByPricingModel = "IntegrationSectionMappingsByPricingModel";
            public static readonly string JobLastTaskIDs = "JobLastTaskId";
            public static readonly string WritingCompanies = "WritingCompanies";
            public static readonly string PricingAnalysis = "PricingAnalysis";
            public static readonly string PricingModels = "PricingModels";
            public static readonly string QuestionnaireAssignment = "QuestionnaireAssignment";
            public static readonly string QuestionnaireTemplate = "QuestionnaireTemplate";
            public static readonly string RatePlanAssignment = "RatePlanAssignment";
            public static readonly string RatePlanAssignmentDetails = "RatePlanAssignmentDetails";
            public static readonly string RatePlanAssignmentTable = "RatePlanAssignmentTable";
            public static readonly string RatePlanRatingFactors = "RatePlanRatingFactors";
            public static readonly string RatePlans = "RatePlans";
            public static readonly string RatingClasses = "RatingClasses";
            public static readonly string RatingClassesByRatePlan = "RatingClassesByRatePlan";
            public static readonly string RatingFactorDetails = "RatingFactorDetails";
            public static readonly string RatingFactorDetailsByRatePlan = "RatingFactorDetailsByRatePlan";
            public static readonly string RatingFactors = "RatingFactors";
            public static readonly string RatingSubClasses = "RatingSubClasses";
            public static readonly string RatingSubClassesByRatePlan = "RatingSubClassesByRatePlan";
            public static readonly string RelativeRatingFactorDetail = "RelativeRatingFactorDetail";
            public static readonly string RelativeRatingFactorDetailsByRatePlan = "RelativeRatingFactorDetailsByRatePlan";
            public static readonly string RelativeRatingFactors = "RelativeRatingFactors";
            public static readonly string SimulationResult = "SimulationResult";
            public static readonly string StagingDataFields = "StagingdDataFields";
            public static readonly string SupportedFileTypes = "SupportedFileTypes";
            public static readonly string Trends = "Trends";
            public static readonly string UIConfigurationByPricingModel = "UIConfigurationByPricingModel";
            public static readonly string UnderlyingCoverageTypes = "UnderlyingCoverageTypes";
            public static readonly string UnderwritingTeams = "UnderwritingTeams";
            public static readonly string UnitBasisTypes = "UnitBasisTypes";
            public static readonly string UserIdentity = "UserIdentity";

        }

        #endregion Cache Key Lookup
    }
}
