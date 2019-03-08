using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Repositories.Lookups
{
	/// <summary>
	/// Abstract base class for any repositories based on Extended Catalog Items
	/// </summary>
	//[ExcludeFromCodeCoverage]
	public abstract class CatalogItemsExtRepository : GenericRepository<ERMSDbContext, grs_VCatalogItemsExt, int>, ILookupsRepository
	{
        protected const char COMMA_SEPARATOR = ',';
        public CatalogItemsExtRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        protected abstract string CatalogItemName { get; }

		public override IList<grs_VCatalogItemsExt> GetAllNoTracking()
		{
			string lookupName = CatalogItemName;
			var catalogDefinition = Context.TbCatalogdefs.AsNoTracking().FirstOrDefault(cd => cd.Catname.Equals(lookupName));
			if (catalogDefinition == null)
			{
				throw new KeyNotFoundException(string.Format("Catalog Definition for '{0}' was not found!", lookupName));
			}

			IQueryable<grs_VCatalogItemsExt> lookupData = Context.grs_VCatalogItemsExts.AsNoTracking().Where(ci =>
				(ci.Catid == catalogDefinition.Catid) 
			).OrderBy(ci => ci.AssumedSortOder);

			return lookupData.ToList();
		}

		public IList<LookupEntity> GetLookupData()
		{
			IList<grs_VCatalogItemsExt> lookupData = GetAllNoTracking();
			return lookupData.Select(l => new LookupEntity(
				id: l.Catid,
				code: l.Code.ToString(),
				description: (l.AssumedName),
				isActive: l.Active && (l.AssumedFlag == 1)
			)).ToList();
		}
        
        public IList<LookupEntity> GetLookupDataByConfig(string configSetting)
        {
            // Planning to use procedure for now till we setup some common manager class for configuration.
            string cfgResult;// = "6,65,66,67";
            Context.grs_EvaluateConfigSetting(-1, configSetting, out cfgResult);
            var statuscodes = new List<string>();
            if (!string.IsNullOrEmpty(cfgResult))
            statuscodes = cfgResult.Split(COMMA_SEPARATOR).Select(a => a.ToLower().Trim()).ToList(); 

            IList<grs_VCatalogItemsExt> lookupData = GetAllNoTracking();
            return lookupData.Select(l => new LookupEntity(
                id: l.Catid,
                code: l.Code.ToString(),
                description: (l.AssumedName),
                isActive: l.Active
            )).Where(l => statuscodes.Contains(l.Code)).ToList();
        }
        
        #region Not Implemented

        public override void Add(grs_VCatalogItemsExt entity) { throw new NotImplementedException(); }
        public override void Delete(grs_VCatalogItemsExt entity) { throw new NotImplementedException(); }
        public override void Delete(Expression<Func<grs_VCatalogItemsExt, bool>> where) { throw new NotImplementedException(); }
        public override grs_VCatalogItemsExt Get(int id) { throw new NotImplementedException(); }
        public new grs_VCatalogItemsExt Get(Expression<Func<grs_VCatalogItemsExt, bool>> where) { throw new NotImplementedException(); }
        public new Task<grs_VCatalogItemsExt> GetAsync(Expression<Func<grs_VCatalogItemsExt, bool>> where) { throw new NotImplementedException(); }
        public override IList<grs_VCatalogItemsExt> GetMany(Expression<Func<grs_VCatalogItemsExt, bool>> where) { throw new NotImplementedException(); }
        public override Task<IList<grs_VCatalogItemsExt>> GetManyAsync(Expression<Func<grs_VCatalogItemsExt, bool>> where) { throw new NotImplementedException(); }
        public new IList<grs_VCatalogItemsExt> FindAllWithIncludes(params string[] associations) { throw new NotImplementedException(); }
        public new IList<grs_VCatalogItemsExt> FindAllWithIncludes(Expression<Func<grs_VCatalogItemsExt, bool>> where, params string[] associations) { throw new NotImplementedException(); }
        public new Task<grs_VCatalogItemsExt> FindWithIncludesAsync(Expression<Func<grs_VCatalogItemsExt, bool>> where, params string[] associations) { throw new NotImplementedException(); }
        public new grs_VCatalogItemsExt FindWithIncludes(Expression<Func<grs_VCatalogItemsExt, bool>> where, params string[] associations) { throw new NotImplementedException(); }
        public new IQueryable<grs_VCatalogItemsExt> FindBy(Expression<Func<grs_VCatalogItemsExt, bool>> predicate) { throw new NotImplementedException(); }
        public new List<IEnumerable> GetDynamicResultSets(string commandString) { throw new NotImplementedException(); }
        public new IList<grs_VCatalogItemsExt> ExecuteQuery(string query) { throw new NotImplementedException(); }
        public new IDbSet<grs_VCatalogItemsExt> DBSet() { throw new NotImplementedException(); }

        #endregion Not Implemented
    }
}

