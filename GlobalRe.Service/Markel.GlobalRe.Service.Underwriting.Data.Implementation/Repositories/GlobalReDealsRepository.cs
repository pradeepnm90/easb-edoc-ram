using LinqKit;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Enums;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Extensions;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class GlobalReDealsRepository : GenericRepository<ERMSDbContext, grs_VGrsDealsByStatu, int>, IGlobalReDealsRepository
    {
        #region Constants

        protected const char COMMA_SEPARATOR = ',';

        #endregion

        private IUserManager _userManager;

        #region Constructor
        public GlobalReDealsRepository(IUserManager userManager, ERMSDbContext context) : base(userManager, context)
        {
            if (userManager == null) throw new NullReferenceException(typeof(IUserManager).ToString());
            _userManager = userManager;
        }

        #endregion

        private enum FilterParameters
        {
            StatusCodes,
            GlobalReData,
            SubDivisions,
            Exposuretypes,
            ProductLines,
            ExposureGroups,
            PopulatedExposuretypes,
            PersonIds
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

        public IPaginatedList<grs_VGrsDealsByStatu> Search(SearchCriteria searchCriteria)
        {
            var data = Context.grs_VGrsDealsByStatus;

            int personId = this._userManager.UserIdentity.NameId;

            var predicate = PredicateBuilder.New<grs_VGrsDealsByStatu>(true);
            predicate = predicate.AndIf(FilterStatusCodes(searchCriteria.Parameters));
            Expression<Func<grs_VGrsDealsByStatu, bool>> expression = FilterExposuretypes(searchCriteria.Parameters);
            if (expression != null)
                predicate = predicate.AndIf(expression);
            else
            {
                Expression<Func<grs_VGrsDealsByStatu, bool>> roleExpression = FilterBasedOnUserRole(personId);
                if (roleExpression != null)
                    predicate = predicate.AndIf(roleExpression);
            }

            PaginatedList<grs_VGrsDealsByStatu> pagingatedResult = new PaginatedList<grs_VGrsDealsByStatu>();
            pagingatedResult.AddRange(data.Where(predicate), searchCriteria.ToFilterCriteria("Dealnum"));
            return pagingatedResult;
        }

        private Expression<Func<grs_VGrsDealsByStatu, bool>> FilterBasedOnUserRole(int personId)
        {
            Expression<Func<grs_VGrsDealsByStatu, bool>> roleExpression = null;
            GetUserRoles().ToList().ForEach(userRole =>
            {
                if (!string.IsNullOrEmpty(userRole))
                {
                    Role role = userRole.ToEnumFromDescription<Role>();

                    switch (role)
                    {
                        case Role.None:
                            break;
                        case Role.Actuary:
                            roleExpression = (roleExpression == null) ? s => s.Act1 == personId : roleExpression.Or(s => s.Act1 == personId);
                            break;
                        case Role.ActuaryManager:
                            List<int> actuaryIds = GetAllMyEmployees(personId).ToList();
                            roleExpression = (roleExpression == null) ? s => actuaryIds.Contains((int)s.Act1) : roleExpression.Or(s => actuaryIds.Contains((int)s.Act1));
                            break;
                        case Role.CAT_Portfolio_Management:
                            break;
                        case Role.Modeler:
                            break;
                        case Role.ModelerManager:
                            break;
                        case Role.Property_UATA:
                            roleExpression = (roleExpression == null) ? s => s.Ta == personId : roleExpression.Or(s => s.Ta == personId);
                            break;
                        case Role.UATA:
                            roleExpression = (roleExpression == null) ? s => s.Ta == personId : roleExpression.Or(s => s.Ta == personId);
                            break;
                        case Role.Underwriter:
                            roleExpression = (roleExpression == null) ? s => s.Uw1 == personId || s.Uw2 == personId : roleExpression.Or(s => s.Uw1 == personId || s.Uw2 == personId);
                            break;
                        case Role.UnderwriterManager:
                            List<int> underwriterIds = GetAllMyEmployees(personId).ToList();
                            roleExpression = (roleExpression == null) ? s => underwriterIds.Contains((int)s.Uw1) || underwriterIds.Contains((int)s.Uw2) : roleExpression.Or(s => underwriterIds.Contains((int)s.Uw1) || underwriterIds.Contains((int)s.Uw2));
                            break;
                        default:
                            break;
                    }
                }
            });
            return roleExpression;
        }

        private Expression<Func<grs_VGrsDealsByStatu, bool>> FilterStatusCodes(IEnumerable<Parameter> parameters)
        {
            //TODO-PS : Refactor
            string statusCodes = parameters.GetParam<string>(FilterParameters.StatusCodes);
            if (!string.IsNullOrEmpty(statusCodes))
            {
                if (statusCodes.Contains(COMMA_SEPARATOR))
                {
                    var codes = statusCodes.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();
                    return s => codes.Contains(s.StatusCode);
                }
                else
                {
                    //To handle single status code
                    int statusCode = Convert.ToInt32(statusCodes);
                    return s => s.StatusCode == statusCode;
                }
            }
            return null;
        }

        private Expression<Func<grs_VGrsDealsByStatu, bool>> FilterExposuretypes(IEnumerable<Parameter> parameters)
        {
            string exposuretypes = parameters.GetParam<string>(FilterParameters.Exposuretypes);
            if (!string.IsNullOrEmpty(exposuretypes))
            {
                if (exposuretypes.Contains(COMMA_SEPARATOR))
                {
                    var exTypes = exposuretypes.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();
                    return s => exTypes.Contains((int)s.Exposuretype);
                }
                else
                {
                    int exposuretype = Convert.ToInt32(exposuretypes);
                    return s => s.Exposuretype == exposuretype;
                }
            }
            return null;
        }

        private Expression<Func<grs_VGrsDealsByStatu, bool>> FilterPopulatedExposuretypes(IEnumerable<Parameter> parameters)
        {
            string exposuretypes = parameters.GetParam<string>(FilterParameters.PopulatedExposuretypes);
            if (!string.IsNullOrEmpty(exposuretypes))
            {
                if (exposuretypes.Contains(COMMA_SEPARATOR))
                {
                    var exTypes = exposuretypes.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();
                    return s => exTypes.Contains((int)s.Exposuretype);
                }
                else
                {
                    int exposuretype = Convert.ToInt32(exposuretypes);
                    return s => s.Exposuretype == exposuretype;
                }
            }
            return null;
        }
        public IList<int> GetStatusCodes()
        {
            //Get status codes from grs.v_GRDealStatus
            var query = (from v in Context.grs_VGrDealStatus
                         select v.StatusCode).Distinct();
            return query.ToList();
        }

        

        private IList<string> GetUserRoles()
        {
            var query = from role in Context.CfgRoles
                        join rolePerson in Context.CfgRolePersons on role.RolePk equals rolePerson.RoleFk
                        where rolePerson.PersonNameFk == _userManager.UserIdentity.NameId
                        select role.Name;
            return query.ToList();
        }

        private IList<int> GetAllMyEmployees(int managerId)
        {
            var query = from person in Context.TbPersons
                        where person.ManagerId == managerId
                        select person.PersonId;
            return query.ToList();
        }

        public IList<grs_VGrsDealsByStatu> GetDeals(SearchCriteria searchCriteria)
        {
            int personId = this._userManager.UserIdentity.NameId;
            var predicate = PredicateBuilder.New<grs_VGrsDealsByStatu>(true);
            predicate = predicate.AndIf(FilterStatusCodes(searchCriteria.Parameters));
            Expression<Func<grs_VGrsDealsByStatu, bool>> expression = FilterPopulatedExposuretypes(searchCriteria.Parameters);
            if (expression != null)
                predicate = predicate.AndIf(expression);

            Expression<Func<grs_VGrsDealsByStatu, bool>> personexpression = FilterBasedOnPersonIds(searchCriteria.Parameters);
            if (personexpression != null)
                predicate = predicate.AndIf(personexpression);

            return Context.grs_VGrsDealsByStatus.Where(predicate).OrderByDescending(p => p.Dealnum).ToList();

        }

        private Expression<Func<grs_VGrsDealsByStatu, bool>> FilterBasedOnPersonIds(IEnumerable<Parameter> parameters)
        {

            Expression<Func<grs_VGrsDealsByStatu, bool>> personExpression = null;
            string personids = parameters.GetParam<string>(FilterParameters.PersonIds);
            if (!string.IsNullOrEmpty(personids))
            {
                var pIds = personids.Split(COMMA_SEPARATOR).Select(a => Convert.ToInt32(a)).ToList();
                pIds.ForEach((personId) =>
                {
                    personExpression = (personExpression == null) ? s => s.Act1 == personId || s.Uw1 == personId || s.Uw2 == personId || s.Ta == personId || s.Modeller == personId
                    : personExpression.Or(s => s.Act1 == personId || s.Uw1 == personId || s.Uw2 == personId || s.Ta == personId || s.Modeller == personId);

                });
            }
            return personExpression;
        }

        private bool ApplyFilter()
        {
            bool result = false;
            _userManager.UserIdentity.Permissions.Where(p => p.Value == true).ToList().ForEach(s =>
            {
                Rules rule = s.Key.ToEnumFromDescription<Rules>();
                switch (rule)
                {
                    case Rules.ShowSubmissionsForCurrentUser:
                    case Rules.ShowSubmissionsForMyEmployees:
                        result = true;
                        break;
                    default:
                        break;
                }
            });
            return result;
        }
       public IList<grs_VExposureTreeExt> GetGlobalReExposureTree()
        {
            try
            {
                var tree = Context.grs_VExposureTreeExts.AsNoTracking().ToList();
                return tree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidPerson(int personId)
        {
            var id  = Context.TbPersons.Where(p => p.PersonId == personId).Select(p => p.PersonId).ToList();
            return id.Count > 0;
        }
    }
}
