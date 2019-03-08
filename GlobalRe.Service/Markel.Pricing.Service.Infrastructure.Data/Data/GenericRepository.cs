using EntityFramework.DbContextScope.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    public abstract class GenericRepository<DB_CONTEXT, CLASS, PRIMARY_KEY> : BaseRepository<DB_CONTEXT>, IGenericRepository<CLASS, PRIMARY_KEY>
        where DB_CONTEXT : DbContext, IDbContext
        where CLASS : class
        where PRIMARY_KEY : IComparable
    {
        #region Constructors

        /// <summary>
        /// Generic Repository Constructor
        /// Note: LazyLoadingEnabled and ProxyCreationEnabled on the Context.Configuration
        /// must be set to true for Entity mapping to fully work.
        /// </summary>
        /// <param name="userManager">User Manager</param>
        /// <param name="ambientDbContextLocator">DB Context Scope, Ambient DB Context Locator</param>
        public GenericRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        #endregion Constructors

        #region Add

        public virtual CLASS Create()
        {
            return DBSet().Create();
        }

        public virtual void Add(CLASS entity)
        {
            DBSet().Add(entity);
        }

        public virtual void AddRange(IEnumerable<CLASS> entities)
        {
            Context.Set<CLASS>().AddRange(entities);
        }

        public virtual async Task<CLASS> AddAsync(CLASS entity)
        {
            return await (Task<CLASS>.Factory.StartNew(() => DBSet().Add(entity)));
        }

        #endregion Add

        #region Delete

        public virtual void Delete(CLASS entity)
        {
            DBSet().Remove(entity);            
        }

        public virtual void Delete(PRIMARY_KEY id)
        {
            CLASS entity = Get(id);
            DBSet().Remove(entity);
        }

        public virtual void Delete(Expression<Func<CLASS, bool>> where)
        {
            IDbSet<CLASS> dbSet = DBSet();
            IEnumerable<CLASS> objects = dbSet.Where<CLASS>(where).AsEnumerable();
            foreach (CLASS entity in objects)
            {
                dbSet.Remove(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<CLASS> entities)
        {
            Context.Set<CLASS>().RemoveRange(entities);
        }

        #endregion Delete

        #region Get

        public virtual CLASS Get(PRIMARY_KEY id)
        {
            return DBSet().Find(id);
        }

        public virtual CLASS Get(Expression<Func<CLASS, bool>> where)
        {
            return FindBy(where).FirstOrDefault();
        }

        public CLASS Get(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            return FindBy(where, includeProperties).FirstOrDefault();
        }

        public async Task<CLASS> GetAsync(Expression<Func<CLASS, bool>> where)
        {
            return await DBSet().Where(where).FirstOrDefaultAsync();
        }

        public virtual CLASS GetNoTracking(PRIMARY_KEY id)
        {
            CLASS entity = Get(id);
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual CLASS GetNoTracking(Expression<Func<CLASS, bool>> where)
        {
            return FindBy(where).AsNoTracking().FirstOrDefault();
        }

        public CLASS GetNoTracking(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            return FindBy(where, includeProperties).AsNoTracking().FirstOrDefault();
        }

        public async Task<CLASS> GetAsyncNoTracking(Expression<Func<CLASS, bool>> where)
        {
            return await DBSet().AsNoTracking().Where(where).FirstOrDefaultAsync<CLASS>();
        }

        #endregion Get

        #region Get Many

        public virtual IList<CLASS> GetAllNoTracking()
        {
            return DBSet().AsNoTracking().ToList();
        }

        public virtual IList<CLASS> GetAllNoTracking(params Expression<Func<CLASS, object>>[] includeProperties)
        {
            IQueryable<CLASS> query = DBSet().AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public virtual IList<CLASS> GetMany(Expression<Func<CLASS, bool>> where)
        {
            return FindBy(where).ToList();
        }

        public virtual IList<CLASS> GetMany(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            return FindBy(where, includeProperties).ToList();
        }

        public virtual async Task<IList<CLASS>> GetManyAsync(Expression<Func<CLASS, bool>> where)
        {
            return await DBSet().Where(where).ToListAsync();
        }

        public virtual IList<CLASS> GetManyNoTracking(Expression<Func<CLASS, bool>> where)
        {
            return FindByNoTracking(where).ToList();
        }

        public virtual IList<CLASS> GetManyNoTracking(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            return FindByNoTracking(where, includeProperties).ToList();
        }

        public virtual async Task<IList<CLASS>> GetManyAsyncNoTracking(Expression<Func<CLASS, bool>> where)
        {
            return await FindByNoTracking(where).ToListAsync();
        }

        public IQueryable<CLASS> FindBy(Expression<Func<CLASS, bool>> predicate)
        {
            return DBSet().Where(predicate);
        }

        /// <summary>
        /// Returns an IQueryable of items of type TEntity
        /// </summary>
        /// <param name="predicate">A where condition to limit the items being returned</param>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>An IQueryable of the requested type CLASS.</returns>
        public IQueryable<CLASS> FindBy(Expression<Func<CLASS, bool>> predicate, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            IQueryable<CLASS> query = DBSet().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate);
        }

        public IQueryable<CLASS> FindByNoTracking(Expression<Func<CLASS, bool>> predicate)
        {
            return FindBy(predicate).AsNoTracking();
        }

        public IQueryable<CLASS> FindByNoTracking(Expression<Func<CLASS, bool>> predicate, params Expression<Func<CLASS, object>>[] includeProperties)
        {
            return FindBy(predicate, includeProperties).AsNoTracking();
        }

        #endregion Get

        #region Public Methods

        public virtual void Save(CLASS entity, bool doRefresh=false)  // mp... 03/01/2019 added option regarding reloading data after save
        {
            Context.SaveChanges();
            ExecuteBatchCommands();
            if (doRefresh) Refresh(entity);   // mp... don't refresh by default anymore.  Pricing was doing this mainly over concern with triggers 
        }                                     // but it is expensive and unnecessary 99% of the time. Consumers should pass "true" as a second param to .Save() when they want a refresh  

        public void Refresh(CLASS entity)
        {
            Context.Entry(entity).Reload();
        }

        #endregion Public Methods

        #region Protected Methods

        protected IDbSet<CLASS> DBSet()
        {
            return Context.Set<CLASS>();
        }

        protected virtual void ExecuteBatchCommands() { }

        protected void Refresh(object entity)
        {
            try
            {
                var objectContext = ((IObjectContextAdapter)Context).ObjectContext;
                objectContext.Refresh(RefreshMode.StoreWins, entity);
            }
            catch (Exception ex)
            {
                Debugger.Log(1, "Error", ex.Message);
            }
        }

        #endregion Protected Methods
    }
}