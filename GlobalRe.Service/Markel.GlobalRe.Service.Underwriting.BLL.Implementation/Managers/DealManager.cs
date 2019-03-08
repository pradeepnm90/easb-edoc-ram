using Markel.GlobalRe.Service.Underwriting.BLL.Constants;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
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
using Markel.Pricing.Service.Infrastructure.Models.ResponseMessageTypes;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{
	public class DealManager : BaseGlobalReManager<BLL_Deal>, IDealManager
	{
		#region Constants

		protected const char COMMA_SEPARATOR = ',';
		private const string INVALID_STATUS_NAME = "Status name '{0}' is not valid.";
		private const string INVALID_STATUS_CODE = "Status code '{0}' is not valid.";
		private const string INVALID_STATUS_CODES = "Status codes '{0}' are not valid.";
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
		private const string BYPASS_CEDANT_GROUP_VALIDATION = "Bypass Reinsurance Cedant Group Type Validation";
		private const string BYPASS_BROKER_GROUP_VALIDATION = "Bypass Reinsurance Broker Group Type Validation";


		#endregion

		#region Private Variable 

		private IDealRepository _dealRepository;
		private IWorkbenchDealsRepository _workbenchDealsRepository;
		private ITblDealRepository _tblDealRepository;
		private IDealStatusesLookupManager _dealStatusesLookupManager;
		private IDealTransformationManager _dealTransformationManager;
		private IEntityLockManager _dealLockManager;
		private ICedantManager _cedantManager;
		private IBrokerManager _brokerManager;
		//Add GRS-708
		private ITblClausesDealRepository _tblClausesdealRepository;


		private enum FilterParameters
		{
			StatusName,
			Status,
			StatusCodes,
			GlobalReData,
			SubDivisions,
			Exposuretypes,
			ProductLines,
			ExposureGroups,
			PersonIds
		}
		#endregion

		#region Constructors

		public DealManager(IUserManager userManager,
							ICacheStoreManager cacheStoreManager,
							ILogManager logManager,
							IDealRepository dealRepository,
							IWorkbenchDealsRepository workbenchDealsRepository,
							ITblDealRepository tblDealRepository,
							IDealStatusesLookupManager dealStatusesLookupManager,
							IEntityLockManager dealLockManager,
							IDealTransformationManager dealsTransformationManager,
							ICedantManager cedantManager,
							IBrokerManager brokerManager
						  , ITblClausesDealRepository tblClausesDealRepository
			)
			: base(userManager, cacheStoreManager, logManager)
		{
			_dealRepository = ValidateRepository(dealRepository);
			_workbenchDealsRepository = ValidateRepository(workbenchDealsRepository);
			_tblDealRepository = ValidateRepository(tblDealRepository);
			_dealStatusesLookupManager = ValidateManager(dealStatusesLookupManager);
			_dealLockManager = ValidateManager(dealLockManager);
			_dealTransformationManager = ValidateManager(dealsTransformationManager);
			_cedantManager = ValidateManager(cedantManager);
			_brokerManager = ValidateManager(brokerManager);
			_tblClausesdealRepository = ValidateRepository(tblClausesDealRepository);
		}

		#endregion

		#region Entity Actions
		public EntityAction GetEntityActions(BLL_Deal entity)
		{
			List<EntityActionType> entityActionTypes = new List<EntityActionType>() { EntityActionType.Entity };
			entityActionTypes.Add(EntityActionType.Action_Update);

			return new EntityAction(
			   entityType: EntityType.Deals,
			   entityId: entity.Dealnum,
			   entityActionTypes: entityActionTypes
		   );
		}

		#endregion

		public IPaginatedList<BLL_Deal> Search(SearchCriteria criteria)
		{
			bool globalReData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.GlobalReData);
			//bool globalReData = criteria.Parameters.GetParam<bool>(FilterParameters.GlobalReData);

			ValidateSearchCriteria(globalReData, criteria);

			ValidateStatusNameAndCode(globalReData, criteria);

			//if (!globalReData) ValidateSubDivisions(criteria);

			return globalReData ? Transform(_dealRepository.Search(criteria)) : Transform(_workbenchDealsRepository.Search(criteria));

		}

		public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
		{
			throw new NotImplementedException();
		}


		public EntityResult<IEnumerable<BLL_Deal>> GetWorkbenchDeals(SearchCriteria criteria)
		{
			#region Validate Criteria

			bool globalReData = criteria.Parameters.FirstOrDefaultValue<bool>(FilterParameters.GlobalReData);
			ValidateSearchCriteria(globalReData, criteria);
			ValidateStatusNameAndCode(globalReData, criteria);
			//     ValidateSubDivisions(criteria);

			ValidateAdvancedFilter(criteria);
			PopulateExposuretypesforAdvancedFilter(criteria);
			#endregion

			var data = _workbenchDealsRepository.GetDeals(criteria);
			if (data.Count() == 0) throw new NotFoundAPIException("Deal not found");
			return new EntityResult<IEnumerable<BLL_Deal>>(data.Select(s => _dealTransformationManager.Transform(s as grs_VGrsDealsByStatu)).ToList());
		}

		public EntityResult<BLL_Deal> GetDeal(int dealNumber)
		{
			var data = _dealRepository.Get(FilterDealNumber(dealNumber));
			if (data == null) throw new NotFoundAPIException("Deal not found");
			return new EntityResult<BLL_Deal>(_dealTransformationManager.Transform(data));
		}

		public bool Lock(int dealNumber)
		{
			bool result = _dealLockManager.Lock((int)EntityLockType.Deals, dealNumber, UserIdentity.UserId);
			return result;
		}
		public bool Unlock(int dealNumber)
		{
			bool result = _dealLockManager.Unlock((int)EntityLockType.Deals, dealNumber, UserIdentity.UserId);
			return result;
		}
		public EntityResult<BLL_EntityLock> GetLocks(int dealNumber)
		{
			var data = _dealLockManager.GetLocks((int)EntityLockType.Deals, dealNumber, UserIdentity.UserId);
			if (data == null) return null;

			var result = new EntityResult<BLL_EntityLock>(data.FirstOrDefault());
			if (result.Data != null)
			{
				result.Data.EntityTypeName = EntityType.Deals.ToString();
				result.Data.Add(new Warning(() => result.Data.EntityID, $"{result.Data.LockedByDisplayName} has locked this deal for edit"));
			}
			return result;
		}

		public EntityResult<BLL_Deal> UpdateDeal(BLL_Deal entity)
		{
			EntityResult<BLL_Deal> result = null;
			if (ValidateBusinessRules(entity))
			{
				result = SaveDeal(entity);
			}
			else if (entity.HasFatal())
			{
				throw new IllegalArgumentAPIException("Save Deal Failed.", entity.GetFatalMessages());
			}
			return result;
		}

		public EntityResult<BLL_Deal> SaveDeal(BLL_Deal entity)
		{
			bool locked = false;
			EntityResult<BLL_Deal> result;
			try
			{
				TblDeal tblDeal = _tblDealRepository.Get(d => d.Dealnum == entity.Dealnum, d => d.TbDealPipeline);
				if (tblDeal == null) { throw new NotFoundAPIException($"Deal number '{entity.Dealnum}' is not available in database."); }

				locked = Lock(entity.Dealnum);
				OnApplyChanges(tblDeal, entity);
				_tblDealRepository.Save(tblDeal);
				UpdateReasonfordecline(entity);
				result = GetDeal(entity.Dealnum);
				result.Data.Add(new Information(entity.Dealname, "Deal Was Saved."));				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (locked) Unlock(entity.Dealnum);
			}
			return result;
		}

        //Add for GRS-708
        private void UpdateReasonfordecline(BLL_Deal entity)
        {
            try
            {
                TbClaus tbclause = null;
                if (!string.IsNullOrWhiteSpace(entity.decreason))
                {
                    tbclause = _tblClausesdealRepository.Get(entity.Dealnum);
                    if (tbclause != null)
                    {
                        tbclause.Decreason = entity.decreason;
                        _tblClausesdealRepository.Save(tbclause);
                    }
                    else
                    {
                        tbclause = new TbClaus();
                        tbclause.Dealnum = entity.Dealnum;
                        tbclause.Decreason = entity.decreason;
                        _tblClausesdealRepository.Add(tbclause);
                        _tblClausesdealRepository.Save(tbclause);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotFoundAPIException("Invalid value of Reasonfordecline");
            }
        }

        protected bool ValidateBusinessRules(BLL_Deal deal)
        {
			bool result = true;
			//// Cedants Business Rule Validatins : GRS-612
			//         var cedantSearchResult = _cedantRepository.Get(c => c.Cedantid == deal.Cedant);
			//         if (cedantSearchResult.IsNullOrEmpty())
			//{
			//	result.Data.Add(new Fatal(() => deal.Dealname, "Cedant must have a Group Type of Reinsurance Cedant Group."));
			//         }
			//         else
			//         {
			//             var searchCedantLocationResult = _cedantRepository.Get(c => (c.Locationid == deal.CedentLocation  && c.Cedantid == deal.Cedant));
			//             if (searchCedantLocationResult.IsNullOrEmpty())
			//             {
			//		result.Data.Add(new Fatal(() => deal.Dealname, "Cedant Location is not related to the Cedant"));
			//             }
			//             else
			//             {
			//                 if (!(searchCedantLocationResult.Cedantcategoryid.Contains(CompanyCategory.GlobalReCedant.ToString())))           // Validate Cedant has the ‘Global Re Cedant’ category selected : 90
			//                 {
			//			result.Data.Add(new Fatal(() => deal.Dealname, "Cedant must have the Global Re Cedant category selected"));
			//                 }
			//                 else
			//                 {
			//                     if (searchCedantLocationResult.Cedantcategoryid.Contains(CompanyCategory.ReinsuranceCedantGroup.ToString()))             // Validate Cedant does not have the ‘Reinsurance Cedant Group’ category selected : 10011
			//                     {
			//				result.Data.Add(new Fatal(() => deal.Dealname, "Cedant cannot have the Reinsurance Cedant Group company category selected"));
			//                     }
			//                     else if (searchCedantLocationResult.Cedantcategoryid.Contains(CompanyCategory.ReinsuranceBrokerGroup.ToString()))        // Validate Cedant does not have the ‘Reinsurance Broker Group’ category selected : 10012
			//                     {
			//				result.Data.Add(new Fatal(() => deal.Dealname, "Cedant cannot have the Reinsurance Broker Group company category selected"));
			//                     }
			//                 }
			//             }
			//         }

			if (CompanyGroupValidationRequired(deal)) 
			{
				if (!ByPassCedantCompanyGroupValidaion(deal))
				{
					if (!_cedantManager.CedantHasReinsuranceCedantGroup(deal.Cedant))
					{
						deal.Add(new Fatal(() => deal.Cedant, "Cedant must have a Group Type of Reinsurance Cedant Group."));
						result = false;
					}
				}

				if (!ByPassBrokerCompanyGroupValidaion(deal))
				{
					if (!_brokerManager.BrokerHasReinsuranceBrokerGroup(deal.Broker))
					{
						deal.Add(new Fatal(() => deal.Broker, "Broker must have a Group Type of Reinsurance Broker Group."));
						result = false;
					}
				}
			}

			return result;
        }

		private bool ByPassCedantCompanyGroupValidaion(BLL_Deal deal)
		{
			string cfgResult = _dealRepository.EvaluateConfigSetting(deal.Dealnum, BYPASS_CEDANT_GROUP_VALIDATION);
			if (cfgResult.IsNullOrEmpty() || cfgResult != "1")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		private bool ByPassBrokerCompanyGroupValidaion(BLL_Deal deal)
		{
			string cfgResult = _dealRepository.EvaluateConfigSetting(deal.Dealnum, BYPASS_BROKER_GROUP_VALIDATION);
			if (cfgResult.IsNullOrEmpty() || cfgResult != "1")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		private bool CompanyGroupValidationRequired(BLL_Deal deal)
		{
			return _dealRepository.GetDealStatusesRequiredInCompanyGroupValidation().ToList().Exists(s => s.StatusId == deal.Status);
		}

		#region Private Methods

		private void OnApplyChanges(TblDeal currentDeal, BLL_Deal changedEntity)
        {
            currentDeal.Dealname = changedEntity.Dealname;
            currentDeal.Status = changedEntity.Status;
            currentDeal.Targetdt = changedEntity.Targetdt;
            currentDeal.Uw1 = changedEntity.Uw1;
            currentDeal.Uw2 = changedEntity.Uw2;
            currentDeal.Modeller = changedEntity.Modeller;
            currentDeal.Act1 = changedEntity.Act1;
            currentDeal.TbDealPipeline.ModelPriority = changedEntity.ModelPriority;
            currentDeal.TbDealPipeline.Ta = changedEntity.Ta;
            currentDeal.Cpartynum = changedEntity.Cedant;
            currentDeal.Cpartylocation = changedEntity.CedentLocation;

            // Defaulting based on user story - GRS-522
            currentDeal.Riskcatagory = RiskCategoryType.TraditionalRisk;
            currentDeal.Liabtype = LiabilityType.PropertyAndCasualty;
            currentDeal.Busclass = BusinessClassCode.Others;  // defaulting to class code 10 = Others

            // Resetting Business Class Code based on 1st level exposure selection by Users on UI screen 
            if (changedEntity.Exposuretype == 1125 || changedEntity.Exposuretype == 1126 || changedEntity.Exposuretype == 1128 || changedEntity.Exposuretype == 5916 || changedEntity.Exposuretype == 5922)
                currentDeal.Busclass = BusinessClassCode.Casualty;  // Casualty
            else if(changedEntity.Exposuretype == 5795 || changedEntity.Exposuretype == 5827)
                currentDeal.Busclass = BusinessClassCode.Property;  // Property
            
            //return true;
        }

        private IPaginatedList<BLL_Deal> Transform<T>(IPaginatedList<T> dbResults) where T : class
        {
            var results = new PaginatedList<BLL_Deal>()
            {
                PageCount = dbResults.PageCount,
                PageIndex = dbResults.PageIndex,
                PageSize = dbResults.PageSize,
                TotalRecordCount = dbResults.TotalRecordCount,
                Items = dbResults.Items.Select(s => s is grs_VGrsDeal ? _dealTransformationManager.Transform(s as grs_VGrsDeal) : _dealTransformationManager.Transform(s as grs_VGrsDealsByStatu)).ToList()
            };

            return results;
        }

        private void ValidateSearchCriteria(bool globalReData, SearchCriteria criteria)
        {
            IList<string> validFilterParameters = new List<string>();
            IList<string> validSortParameters = new List<string>();
            if (globalReData)
            {
                validFilterParameters = _dealRepository.GetFilterParameters();
                validSortParameters = _dealRepository.GetSortParameters();
            }
            else
            {
                validFilterParameters = _workbenchDealsRepository.GetFilterParameters();
                validSortParameters = _workbenchDealsRepository.GetSortParameters();
            }
            criteria.ValidateSearchCriteria(validFilterParameters, validSortParameters);
        }

        private void ValidateStatusNameAndCode(bool globalReData, SearchCriteria criteria)
        {
            if (globalReData)
            {
                //Validate Status Code
                int? statusCode = criteria.Parameters.FirstOrDefaultValue<int?>(FilterParameters.Status);
                if (statusCode.HasValue)
                {
                    if (!_dealStatusesLookupManager.GetGlobalReDealStatusCodes().Contains((int)statusCode))
                    {
                        throw new IllegalArgumentAPIException(string.Format(INVALID_STATUS_CODE, statusCode));
                    }
                }

                // Validate Status Name
                string status = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.StatusName);
                if (!string.IsNullOrEmpty(status))
                {
                    if (!_dealStatusesLookupManager.GetGlobalReDealStatusNames().Contains(status.ToLower())) { throw new IllegalArgumentAPIException(INVALID_STATUS_NAME, status); }
                }
            }
            // Validate Status Codes 
            string codes = criteria.Parameters.FirstOrDefaultValue<string>(FilterParameters.StatusCodes);
            if (!string.IsNullOrEmpty(codes))
            {
                var statusCodes = globalReData ? _dealStatusesLookupManager.GetGlobalReDealStatusCodes() : GetStatusCodes();

                if (codes.Contains(COMMA_SEPARATOR))
                {
                    var scodes = new List<int>();
                    try { scodes = codes.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                    catch { throw new IllegalArgumentAPIException(INVALID_STATUS_CODES, codes); }
                    scodes.ForEach((c) => { if (!statusCodes.Contains(c)) { throw new IllegalArgumentAPIException(string.Format(INVALID_STATUS_CODE, c)); } });
                }
                else
                {
                    try { if (!statusCodes.Contains(Convert.ToInt32(codes))) { throw new IllegalArgumentAPIException(INVALID_STATUS_CODE, codes); } }
                    catch { throw new IllegalArgumentAPIException(INVALID_STATUS_CODE, codes); }
                }
            }

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
                    if (!_workbenchDealsRepository.ValidPerson(personid))
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
                catch { throw new IllegalArgumentAPIException(INVALID_EXPOSUREGROUPS, exposuresgroupParam); }
            }

            if (!string.IsNullOrEmpty(productLineParam))
            {
                try { prdparam = productLineParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(INVALID_PRODUCTLINES, productLineParam); }
            }

            if (!string.IsNullOrEmpty(subdivisionParam))
            {
                try { divparam = subdivisionParam.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList(); }
                catch { throw new IllegalArgumentAPIException(INVALID_SUBDIVISIONS, subdivisionParam); }
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
        private void PopulateExposuretypesforAdvancedFilter(SearchCriteria criteria)
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

            criteria.Add(new FilterParameter("PopulatedExposuretypes", string.Join(",", PopExpTypeList.Select(x => x.ToString()).ToArray())));
            //throw new IllegalArgumentAPIException("count ={0} list is "+string.Join(",", PopExpTypeList.Select(x => x.ToString()).ToArray()), PopExpTypeList.Count());
        }
        private IList<int> GetStatusCodes()
        {
            //Note - GRS Status Codes (Need to create Lookup for this)
            #region Caching 

            IList<int> cachedStatusCodes = CacheManager.GetItem<IList<int>>("GRS_StatusCodes", (action) =>
            {
                return new CacheItem(_workbenchDealsRepository.GetStatusCodes());
            }, false);
            return new List<int>(cachedStatusCodes);

            #endregion

            //return _workbenchDealsRepository.GetStatusCodes();
        }

        public List<BLL_ExposureTree> GetGlobalReExposureTree()
        {
            #region Caching

            List<BLL_ExposureTree> cachedExposureTree = CacheManager.GetItem<List<BLL_ExposureTree>>("PopulateExposureTree", (action) =>
            {
                return new CacheItem(_dealTransformationManager.Transform(_workbenchDealsRepository.GetGlobalReExposureTree()));
            }, false);
            return cachedExposureTree;

            #endregion
        }

        private Expression<Func<grs_VGrsDeal, bool>> FilterDealNumber(int dealNumber)
        {
            return s => s.Dealnum == dealNumber;
        }

        #endregion
    }
}
