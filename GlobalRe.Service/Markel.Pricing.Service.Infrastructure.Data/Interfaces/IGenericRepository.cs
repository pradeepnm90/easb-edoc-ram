using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    /// <summary>
    /// Generic Repository interface.
    /// </summary>
    public interface IGenericRepository<CLASS, PRIMARY_KEY> : IBaseRepository
        where CLASS : class
        where PRIMARY_KEY : IComparable
    {
        #region Add

        CLASS Create();
        void Add(CLASS entity);
        void AddRange(IEnumerable<CLASS> entities);
        Task<CLASS> AddAsync(CLASS entity);

        #endregion Add

        #region Delete

        void Delete(CLASS entity);
        void Delete(PRIMARY_KEY id);
        void Delete(Expression<Func<CLASS, bool>> where);
        void DeleteRange(IEnumerable<CLASS> entities);

        #endregion Delete

        #region Get

        CLASS Get(PRIMARY_KEY id);
        CLASS Get(Expression<Func<CLASS, bool>> where);
        CLASS Get(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties);
        Task<CLASS> GetAsync(Expression<Func<CLASS, bool>> where);

        CLASS GetNoTracking(PRIMARY_KEY id);
        CLASS GetNoTracking(Expression<Func<CLASS, bool>> where);
        CLASS GetNoTracking(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties);
        Task<CLASS> GetAsyncNoTracking(Expression<Func<CLASS, bool>> where);

        #endregion Get

        #region Get Many

        IList<CLASS> GetAllNoTracking();
        IList<CLASS> GetAllNoTracking(params Expression<Func<CLASS, object>>[] includeProperties);

        IList<CLASS> GetMany(Expression<Func<CLASS, bool>> where);
        IList<CLASS> GetMany(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties);
        Task<IList<CLASS>> GetManyAsync(Expression<Func<CLASS, bool>> where);

        IList<CLASS> GetManyNoTracking(Expression<Func<CLASS, bool>> where);
        IList<CLASS> GetManyNoTracking(Expression<Func<CLASS, bool>> where, params Expression<Func<CLASS, object>>[] includeProperties);
        Task<IList<CLASS>> GetManyAsyncNoTracking(Expression<Func<CLASS, bool>> where);

        IQueryable<CLASS> FindBy(Expression<Func<CLASS, bool>> predicate);
        IQueryable<CLASS> FindBy(Expression<Func<CLASS, bool>> predicate, params Expression<Func<CLASS, object>>[] includeProperties);

        IQueryable<CLASS> FindByNoTracking(Expression<Func<CLASS, bool>> predicate);
        IQueryable<CLASS> FindByNoTracking(Expression<Func<CLASS, bool>> predicate, params Expression<Func<CLASS, object>>[] includeProperties);

        #endregion Get Many

        #region Public Methods

        void Save(CLASS entity, bool doRefresh=false);

        void Refresh(CLASS entity);

        #endregion Public Methods
    }
}
