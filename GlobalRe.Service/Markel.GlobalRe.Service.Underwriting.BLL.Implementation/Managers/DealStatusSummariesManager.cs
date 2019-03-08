using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
    public class DealStatusSummariesManager : BaseGlobalReManager<BLL_DealStatusSummary>, IDealStatusSummariesManager
    {
        #region Constants

        protected const string STATUS_CODE_SEPARATOR = ",";

        #endregion

        #region Private Variable 

        private IDealStatusSummariesRepository _dealStatusSummariesRepository;
        private IDealStatusSummariesTransformationManager _dealStatusSummariesTransformationManager;

        private enum FilterParameters
        {
            SubDivisions,
            Exposuretypes,
            ProductLines,
            ExposureGroups,
            PersonIds
        }

        protected const char COMMA_SEPARATOR = ',';
        private const string INVALID_SUBDIVISION = "SubDivision '{0}' is not valid.";
        private const string INVALID_SUBDIVISIONS = "SubDivisions '{0}' are not valid.";
        private const string INVALID_EXPOSURETYPE = "Exposuretype '{0}' is not valid or does not have valid higher level parameters.";
        private const string INVALID_EXPOSURETYPES = "Exposuretypes '{0}' are not valid.";
        private const string INVALID_EXPOSUREGROUPS = "ExposureGroups '{0}' are not valid.";
        private const string INVALID_EXPOSUREGROUP = "ExposureGroup '{0}' is not valid or does not have valid higher level parameters.";
        private const string INVALID_PRODUCTLINE = "Productline '{0}' is not valid or does not have valid higher level parameters.";
        private const string INVALID_PRODUCTLINES = "Productlines '{0}' are not valid.";
        private const string INVALID_PERSONIDS = "PersonIds '{0}' are not valid.";
        private const string INVALID_PERSONID = "PersonId '{0}' does not exists.";

        #endregion

        public DealStatusSummariesManager(IUserManager userManager,
                                          ICacheStoreManager cacheStoreManager,
                                          ILogManager logManager,
                                          IDealStatusSummariesRepository dealStatusSummariesRepository,
                                          IDealStatusSummariesTransformationManager dealStatusSummariesTransformationManager)
            : base(userManager, cacheStoreManager, logManager)
        {
            _dealStatusSummariesRepository = ValidateRepository(dealStatusSummariesRepository);
            _dealStatusSummariesTransformationManager = ValidateManager(dealStatusSummariesTransformationManager);
        }

        public EntityResult<IEnumerable<BLL_DealStatusSummary>> GetAllDealStatusSummaries(SearchCriteria criteria)
        {
            ValidateAdvancedFilter(criteria);
            string expIDList = PopulateExposuretypesforAdvancedFilter(criteria);
            string personIdList = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.PersonIds);
            return new EntityResult<IEnumerable<BLL_DealStatusSummary>>(_dealStatusSummariesTransformationManager.Transform(_dealStatusSummariesRepository.GetAllDealStatusSummaries(expIDList, personIdList)));
        }

        public EntityAction GetEntityActions(BLL_DealStatusSummary entity)
        {
            List<EntityAction> entityActions = new List<EntityAction>();
            if (entity?.Count > 0)
            {
                var searchCriteria = entity.DealStatusSummary?.Count > 0 ?
                                        String.Join(STATUS_CODE_SEPARATOR, entity.DealStatusSummary.Select(a => (int)a.StatusCode).ToList()) : $"{ entity.StatusCode }";

                entityActions.Add(EntityType.Deals, $"?statuscodes={searchCriteria}", EntityActionType.RelatedEntity);
            }

            return new EntityAction(
               entityType: EntityType.DealStatusSummaries,
               entityId: entity?.StatusCode,
               entityActionTypes: new List<EntityActionType>(),
               entityActions: entityActions
           );
        }

        private void ValidateAdvancedFilter(SearchCriteria criteria)
        {
            //New logic
            string subdivisionParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.SubDivisions);
            string exposuresParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.Exposuretypes);
            string exposuresgroupParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.ExposureGroups);
            string productLineParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.ProductLines);
            string personIds = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.PersonIds);

            var expparam = new List<int>();
            var grpparam = new List<int>();
            var prdparam = new List<int>();
            var divparam = new List<int>();
            var pidparam = new List<int>();

            if (!string.IsNullOrEmpty(personIds))
            {
                try { pidparam = personIds.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(INVALID_PERSONIDS, personIds); }

                pidparam.ForEach((personid) =>
                {
                    if (!_dealStatusSummariesRepository.ValidPerson(personid))
                        throw new IllegalArgumentAPIException(string.Format(INVALID_PERSONID, personid));
                });
            }

            if (!string.IsNullOrEmpty(exposuresParam))
            {
                try { expparam = exposuresParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(INVALID_EXPOSURETYPES, exposuresParam); }
            }

            if (!string.IsNullOrEmpty(exposuresgroupParam))
            {
                try { grpparam = exposuresgroupParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(string.Format(INVALID_EXPOSUREGROUPS, exposuresgroupParam)); }
            }

            if (!string.IsNullOrEmpty(productLineParam))
            {
                try { prdparam = productLineParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(string.Format(INVALID_PRODUCTLINES, productLineParam)); }
            }

            if (!string.IsNullOrEmpty(subdivisionParam))
            {
                try { divparam = subdivisionParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(string.Format(INVALID_SUBDIVISIONS, subdivisionParam)); }
            }

            var ExpGlobalReTree = GetGlobalReExposureTree();

            if (expparam.Count > 0)
            {
                if (grpparam.Count < 1)
                    throw new IllegalArgumentAPIException("ExposureGroups parameter is either empty or missing");
                if (prdparam.Count < 1)
                    throw new IllegalArgumentAPIException("Productlines parameter is either empty or missing");
                if (divparam.Count < 1)
                    throw new IllegalArgumentAPIException("Subdivisions parameter is either empty or missing");

                expparam.ForEach((exposure) =>
                {
                    var explist = ExpGlobalReTree.Where(t => (divparam.Contains(t.SubdivisionCode ?? -1)))
                                                 .Where(t => (prdparam.Contains(t.ProductLineCode ?? -1)))
                                                 .Where(t => (grpparam.Contains(t.ExposureGroupCode ?? -1)))
                                                 .Where(t => (t.ExposureTypeCode == exposure))
                                                 .GroupBy(g => g.ExposureTypeCode)
                                                 .Select(e => e.First().ExposureTypeCode).ToList();
                    if (explist.Count < 1) throw new IllegalArgumentAPIException(string.Format(INVALID_EXPOSURETYPE, exposure));
                });
            }

            if (grpparam.Count > 0)
            {
                if (prdparam.Count < 1)
                    throw new IllegalArgumentAPIException("Productlines parameter is either empty or missing");
                if (divparam.Count < 1)
                    throw new IllegalArgumentAPIException("Subdivisions parameter is either empty or missing");


                grpparam.ForEach((group) =>
                {
                    var grplist = ExpGlobalReTree.Where(t => (divparam.Contains(t.SubdivisionCode ?? -1)))
                                                 .Where(t => (prdparam.Contains(t.ProductLineCode ?? -1)))
                                                 .Where(t => (t.ExposureGroupCode == group))
                                                 .GroupBy(g => g.ExposureGroupCode)
                                                 .Select(e => e.First().ExposureGroupCode).ToList();
                    if (grplist.Count < 1) throw new IllegalArgumentAPIException(string.Format(INVALID_EXPOSUREGROUP, group));
                });
            }

            if (prdparam.Count > 0)
            {
                if (divparam.Count < 1)
                    throw new IllegalArgumentAPIException("Subdivisions parameter is either empty or missing");
                prdparam.ForEach((product) =>
                {
                    var prdlist = ExpGlobalReTree.Where(t => (divparam.Contains(t.SubdivisionCode ?? -1)))
                                                 .Where(t => (t.ProductLineCode == product))
                                                 .GroupBy(g => g.ProductLineCode)
                                                 .Select(e => e.First().ProductLineCode).ToList();
                    if (prdlist.Count < 1) throw new IllegalArgumentAPIException(string.Format(INVALID_PRODUCTLINE, product));
                });
            }

            if (divparam.Count > 0)
            {
                divparam.ForEach((division) =>
                {
                    var divlist = ExpGlobalReTree.Where(t => ((t.SubdivisionCode ?? -1) == division))
                                                 .GroupBy(g => g.SubdivisionCode)
                                                 .Select(e => e.First().SubdivisionCode).ToList();
                    if (divlist.Count < 1) throw new IllegalArgumentAPIException(string.Format(INVALID_SUBDIVISION, division));
                });
            }
        }
        private string PopulateExposuretypesforAdvancedFilter(SearchCriteria criteria)
        {
            //populated list
            var PopPL2List = new List<int>();
            var PopExpGrpList = new List<int>();
            var PopExpTypeList = new List<int>();
            //param list
            var SubDivList = new List<int>();
            var PL2List = new List<int>();
            var ExpGrpList = new List<int>();
            var ExpTypeList = new List<int>();

            string subdivisionParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.SubDivisions);
            string productLineParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.ProductLines);
            string exposuresgroupParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.ExposureGroups);
            string exposuresParam = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.Exposuretypes);

            if (!string.IsNullOrEmpty(subdivisionParam))
                SubDivList = subdivisionParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();

            if (!string.IsNullOrEmpty(productLineParam))
                PL2List = productLineParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();

            if (!string.IsNullOrEmpty(exposuresgroupParam))
                ExpGrpList = exposuresgroupParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();

            if (!string.IsNullOrEmpty(exposuresParam))
                ExpTypeList = exposuresParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();


            var ExpGlobalReTree = GetGlobalReExposureTree();

            if (SubDivList.Count > 0)
            {
                SubDivList.ForEach((division) =>
                {
                    var prdl = ExpGlobalReTree.Where(t => ((t.SubdivisionCode ?? -1) == division))
                                               .GroupBy(g => g.ProductLineCode)
                                              .Select(e => e.First().ProductLineCode ?? -1).ToList();

                    bool isselected = false;
                    prdl.ForEach((product) =>
                    {
                        if (PL2List.Contains(product))
                        {
                            isselected = true;
                            PopPL2List.Add(product);
                        }
                    });
                    if (!isselected)
                    {
                        PopPL2List.AddRange(prdl);
                    }
                    PopPL2List = PopPL2List.Distinct().ToList();

                    //populate exp grp
                    PopPL2List.ForEach((product) =>
                    {
                        var grplist = ExpGlobalReTree.Where(t => ((t.SubdivisionCode ?? -1) == division))
                                                .Where(t => (t.ProductLineCode == product))
                                               .GroupBy(g => g.ExposureGroupCode)
                                              .Select(e => e.First().ExposureGroupCode ?? -1).ToList();

                        isselected = false;
                        grplist.ForEach((group) =>
                        {
                            if (ExpGrpList.Contains(group))
                            {
                                isselected = true;
                                PopExpGrpList.Add(group);
                            }
                        });
                        if (!isselected)
                        {
                            PopExpGrpList.AddRange(grplist);
                        }
                        PopExpGrpList = PopExpGrpList.Distinct().ToList();

                        //populate exp type
                        PopExpGrpList.ForEach((group) =>
                        {
                            var explist = ExpGlobalReTree.Where(t => ((t.SubdivisionCode ?? -1) == division))
                                                .Where(t => (t.ProductLineCode == product))
                                                .Where(t => (t.ExposureGroupCode == group))
                                               .GroupBy(g => g.ExposureTypeCode)
                                              .Select(e => e.First().ExposureTypeCode).ToList();
                            isselected = false;
                            explist.ForEach((exposure) =>
                            {
                                if (ExpTypeList.Contains(exposure))
                                {
                                    isselected = true;
                                    PopExpTypeList.Add(exposure);
                                }
                            });
                            if (!isselected)
                            {
                                PopExpTypeList.AddRange(explist);
                            }
                            PopExpTypeList = PopExpTypeList.Distinct().ToList();
                        });

                    });

                    PopPL2List.Clear();
                    PopExpGrpList.Clear();
                });
            }
            return string.Join(",", PopExpTypeList.Select(x => x.ToString()).ToArray());
        }

        private List<BLL_ExposureTree> GetGlobalReExposureTree()
        {
            #region Caching

            List<BLL_ExposureTree> cachedExposureTree = CacheManager.GetItem<List<BLL_ExposureTree>>("ExposureTreeValidation", (action) =>
            {
                return new CacheItem(_dealStatusSummariesTransformationManager.Transform(_dealStatusSummariesRepository.GetGlobalReExposureTree()));
            }, false);
            return cachedExposureTree;

            #endregion
        }
    }
}
