using LinqKit;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Extensions;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class DealRepository : GenericRepository<ERMSDbContext, grs_VGrsDeal, int>, IDealRepository
    {
        #region Constants

        private const string INVALID_STATUS_NAME = "Status name '{0}' is not valid.";
        protected const char STATUS_CODE_SEPARATOR = ',';

        #endregion


        public DealRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        private enum FilterParameters
        {
            StatusName,
            Status,
            StatusCodes,
            GlobalReData // Flag to switch deal repositories
        }

        private enum SortParameters
        {
            StatusName,
            Dealnum,
            Dealname,
            Status,
            Contractnum,
            Inceptdate,
            Targetdt,
            ModelPriority,
            Submissiondate,
            Uw1,
            Uw1Name,
            Uw2,
            Uw2Name,
            Ta,
            TaName,
            Modeller,
            ModellerName,
            Act1,
            Act1Name,
            Expirydate,
            Broker,
            BrokerName,
            BrokerContact,
            BrokerContactName,
			Cedant,
			CedantName,
			Continuous
		}

		public IList<string> GetFilterParameters()
        {
            return Enum.GetNames(typeof(FilterParameters)).ToList();
        }

        public IList<string> GetSortParameters()
        {
            return Enum.GetNames(typeof(SortParameters)).ToList();
        }

        public IPaginatedList<grs_VGrsDeal> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsDeals;

            var predicate = PredicateBuilder.New<grs_VGrsDeal>(true);

            //predicate = predicate.AndIf(FilterStatus(searchCriteria.Parameters));
            //predicate = predicate.AndIf(FilterStatusCode(searchCriteria.Parameters));
            predicate = predicate.AndIf(FilterStatusCodes(searchCriteria.Parameters));

            PaginatedList<grs_VGrsDeal> pagingatedResult = new PaginatedList<grs_VGrsDeal>();
            pagingatedResult.AddRange(FindByNoTracking(predicate), searchCriteria, "Dealnum");
            return pagingatedResult;
        }
		public IList<TbComGrpReq> GetDealStatusesRequiredInCompanyGroupValidation()
		{
			return Context.TbComGrpReqs.Where(g => g.Active == true).ToList();
		}

        public System.Collections.IEnumerable SearchDistinct(SearchCriteria criteria, string distinctColumn)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<grs_VGrsDeal, bool>> FilterStatus(IEnumerable<FilterParameter> parameters)
        {
            string status = parameters.FirstOrDefaultValue<string>(FilterParameters.StatusName);
            if (!string.IsNullOrEmpty(status))
            {
                return s => s.StatusName == status;
            }
            return null;
        }

        private Expression<Func<grs_VGrsDeal, bool>> FilterStatusCode(IEnumerable<FilterParameter> parameters)
        {
            int? id = parameters.FirstOrDefaultValue<int?>(FilterParameters.Status);
            if (id.HasValue)
                return s => s.Status == id.Value;
            return null;
        }

        private Expression<Func<grs_VGrsDeal, bool>> FilterStatusCodes(IEnumerable<FilterParameter> parameters)
        {
            //TODO-PS : Refactor
            string statusCodes = parameters.FirstOrDefaultValue<string>(FilterParameters.StatusCodes);
            if (!string.IsNullOrEmpty(statusCodes))
            {
                if (statusCodes.Contains(STATUS_CODE_SEPARATOR))
                {
                    var codes = statusCodes.Split(STATUS_CODE_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();
                    return s => codes.Contains((int)s.Status);
                }
                else
                {
                    //To handle single status code
                    int statusCode = Convert.ToInt32(statusCodes);
                    return s => s.Status == statusCode;
                }
            }
            return null;
        }

        public string EvaluateConfigSetting(int dealnum, string settingName)
        {
            string cfgResult;
            Context.grs_EvaluateConfigSetting(dealnum, settingName, out cfgResult);
            return cfgResult;
        }

    }
}
