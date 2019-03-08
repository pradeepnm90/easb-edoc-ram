using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1
{
    [ExcludeFromCodeCoverage]
    public static class RouteHelper
    {
        #region API Endpoints

        public const string APIV1 = @"v1";

        #region Deals

        private const string RelativePathDeals = @"/deals";
        public const string DealsRoutePrefix = APIV1 + RelativePathDeals;
		private const string RelativePathDealStatusSummaries = @"/dealstatussummaries";
        public const string DealStatusSummaryRoutePrefix = APIV1 + RelativePathDealStatusSummaries;

        private const string RelativePathDealsCoverage = @"/dealscoverage";
        public const string DealsCoverageRoutePrefix = APIV1 + RelativePathDealsCoverage;

        public const string DealNumber = @"{dealNumber}";
		public const string DealLocksPrefix = DealNumber + @"/locks";

		#endregion Deals
		//private const string RelativePathCheckLists = @"/deals";
		//public const string CheckListsRoutePrefix = APIV1 + RelativePathDeals;
		// public const string CheckListTaskId = @"{taskid}";
		#region CheckList

		#endregion CheckList

		#region Master API Endpoints

		// Paper details & caching 
		private const string RelativePathWritingCompany = @"/writingcompanies";
        public const string WritingCompanyRoutePrefix = APIV1 + RelativePathWritingCompany;
        public const string GRSFlag = @"{grsflag}";

        #endregion

        #region Search

        private const string RelativePathCedants = @"/cedants";
        public const string CedantsRoutePrefix = APIV1 + RelativePathCedants;
        public const string CedantName = @"{cedantname}";

        #endregion

        //Added for GRS-473
        #region Notes
        private const string RelativePathNotes = @"/notes";
        public const string NotesRoutePrefix = APIV1 + RelativePathNotes;
        public const string DealNotesNumber = @"{DealNumber}";
        public const string NoteNumber = @"{noteNumber}";

        #endregion Notes

        #region userviews
        private const string RelativePathUserview = @"/userviews";
        public const string UserviewRoutePrefix = APIV1 + RelativePathUserview;
        public const string ViewID = @"{viewid}";


        #endregion userviews

        #region SubDivisions

        private const string RelativePathSubDivisions = @"/subdivisions";
        public const string SubDivisionsRoutePrefix = APIV1 + RelativePathSubDivisions;

        #endregion

        #region Lookups

        private const string LookupsRoutePrefix = APIV1 + @"/lookups";
        public const string LookupsDealStatusesLookupRoutePrefix = LookupsRoutePrefix + @"/dealstatuses";
        //added for 708
        public const string LookupsDeclineDealStatusesLookupRoutePrefix = LookupsRoutePrefix + @"/declinedealstatuses";
        public const string LookupsRolePersonsRoutePrefix = LookupsRoutePrefix + @"/rolepersons";
        //Added for GRS-509
        public const string LookupsCoverageBasisRoutePrefix = LookupsRoutePrefix + @"/coveragebasis";
        //Added for GRS-510
        public const string LookupsAttachmentBasisRoutePrefix = LookupsRoutePrefix + @"/attachmentbasis";
        public const string LookupsContractTypesRoutePrefix = LookupsRoutePrefix + @"/ContractTypes";
		public const string AssumedCededFlag = @"{assumedCededFlag}";


        public const string NoteTypesRoutePrefix = LookupsRoutePrefix + @"/notetypes";
        //added for GRS-596
        public const string LookupsExposureTreeRoutePrefix = LookupsRoutePrefix + @"/exposuretree";
        #endregion Lookups

        #region Persons

        private const string RelativePathPerson = @"/persons";
        public const string PersonRoutePrefix = APIV1 + RelativePathPerson;
		public const string PersonId = @"{personId}";

		#endregion

		#region Person Profile

		private const string RelativePathPersonProfile = @"/personprofile";
        public const string PersonProfileRoutePrefix = APIV1 + RelativePathPersonProfile;

        #endregion

        #region Metrics

        public const string ServiceMetricsRoutePrefix = APIV1 + @"/servicemetrics";

        #endregion Metrics

        #endregion API Endpoints

        #region Helper Methods

        private class EntityUrl
        {
            public string URL { get; private set; }
            public bool IsFullPath { get; private set; } // Relative Path or Full Path - Either use or ignore base path
            public EntityUrl(string url, bool isFullPath = false)
            {
                URL = url;
                IsFullPath = isFullPath;
            }
        }

        private static Dictionary<EntityType, EntityUrl> EntityURLMapping = new Dictionary<EntityType, EntityUrl>()
        {
            // Deals
            { EntityType.Deals, new EntityUrl(RouteHelper.DealsRoutePrefix, true) },

            // DealStatusSummaries
            { EntityType.DealStatusSummaries, new EntityUrl(RouteHelper.DealStatusSummaryRoutePrefix, true) },

            //SubDivisions
            { EntityType.SubDivisions, new EntityUrl(RouteHelper.SubDivisionsRoutePrefix, true) },

            // Lookups
            { EntityType.DealStatuses, new EntityUrl(RouteHelper.LookupsDealStatusesLookupRoutePrefix, true) },
            { EntityType.ExposureTree, new EntityUrl(RouteHelper.LookupsExposureTreeRoutePrefix, true) },

            // Persons
            { EntityType.Persons, new EntityUrl(RouteHelper.PersonRoutePrefix, true) },
             
            // PersonProfile
            { EntityType.PersonProfile, new EntityUrl(RouteHelper.PersonProfileRoutePrefix, true) },

            // DealCoverages
            { EntityType.DealCoverages, new EntityUrl("", true) },

            //Notes added by  GRS-473
            { EntityType.Notes, new EntityUrl(RouteHelper.NotesRoutePrefix, true) },

            { EntityType.UserViews, new EntityUrl(RouteHelper.UserviewRoutePrefix, true) },

             //Notes added by  GRS-688
            //{ EntityType.NoteTypes, new EntityUrl(RouteHelper.NoteTypesRoutePrefix, true) },
            
            //CoverageBasis added by  GRS-509
            { EntityType.CoverageBasis, new EntityUrl(RouteHelper.LookupsCoverageBasisRoutePrefix, true) },

            //AttachmentBasis added by GRS-510
            { EntityType.AttachmentBasis, new EntityUrl(RouteHelper.LookupsAttachmentBasisRoutePrefix, true) },

            //ContractTypes for GRS-508
            { EntityType.ContractTypes, new EntityUrl(RouteHelper.LookupsContractTypesRoutePrefix, true) },

            // WritingCompany 
            { EntityType.WritingCompany, new EntityUrl(RouteHelper.WritingCompanyRoutePrefix, true) },

            // Cedants
            { EntityType.Cedants, new EntityUrl(RouteHelper.CedantsRoutePrefix, true) },

            { EntityType.Checklists, new EntityUrl("", false) }
        };

        private static string GetUrl(EntityType businessEntityType, string parameters)
        {
            return string.Format("{0}{1}", EntityURLMapping[businessEntityType], parameters);
        }

        #endregion Helper Methods

        #region API Methods

        public static int? GetIDFromUrl(Uri uri, string relativePath)
        {
            var result = uri.LocalPath.Split('/').FindNextItem(item => item.Equals(relativePath.Replace("/", "")));
            if (!string.IsNullOrEmpty(result))
            {
                int id;
                if (int.TryParse(result, out id))
                {
                    return id;
                }
            }
            return null;
        }

        public static List<Link> BuildLinks(string baseUrl, Enum primaryEntityType, EntityAction entityActions)
        {
            List<Link> links = new List<Link>();

            EntityType entityType;
            if (!Enum.TryParse(entityActions.EntityType.ToString(), out entityType)) throw new Exception($"Unknown Entity Type: {entityActions.EntityType}");

            bool isPrimaryEntity = Enum.Equals(primaryEntityType, entityActions.EntityType);
            string relativeUrl = GetRelativeUrl(baseUrl, entityActions, EntityURLMapping[entityType], isPrimaryEntity);

            if (entityActions.EntityActions.Count > 0)
            {
                // Relative Links + Relative URL
                foreach (EntityAction entityAction in entityActions.EntityActions)
                {
                    links.AddRange(BuildLinks(relativeUrl, primaryEntityType, entityAction));
                }
            }

            string relativeEntity = isPrimaryEntity ? "Self" : entityType.ToString();
            foreach (EntityActionType entityActionType in entityActions.EntityActionTypes)
            {
                links.Add(
                    rel: relativeEntity,
                    href: relativeUrl,
                    entityAction: entityActionType
                );
            }

            return links;
        }

        private static string GetRelativeUrl(string baseUrl, EntityAction entityAction, EntityUrl entityUrl, bool isPrimaryEntity)
        {
            string relativeUrl = baseUrl;

            if (entityUrl.IsFullPath)
            {
                relativeUrl = entityUrl.URL;
            }
            else if (!isPrimaryEntity || (isPrimaryEntity && string.IsNullOrEmpty(entityAction.EntityKey)))
            {
                // Not Primary: Add Sub URL
                // Primary w/ out ID: Add Sub URL
                relativeUrl += entityUrl.URL;
            }

            if (!string.IsNullOrEmpty(entityAction.EntityKey))
                if (entityAction.EntityKey.Contains("?"))
                {
                    //Note : This is added to build link with search parameters
                    relativeUrl += entityAction.EntityKey;
                }
                else { relativeUrl += "/" + entityAction.EntityKey; }

            return relativeUrl;
        }

        #endregion API Methods
    }
}