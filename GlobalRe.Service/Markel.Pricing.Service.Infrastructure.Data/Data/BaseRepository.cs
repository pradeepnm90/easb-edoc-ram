using EntityFramework.DbContextScope.Interfaces;
using Markel.Pricing.Service.Infrastructure.Config;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Markel.Pricing.Service.Infrastructure.Data
{
    public abstract class BaseRepository<DB_CONTEXT> : IBaseRepository
        where DB_CONTEXT : DbContext, IDbContext
    {
        #region Private Varibles

        private int? DBCommandTimeout = MarkelConfiguration.DBCommandTimeout;

        private IAmbientDbContextLocator _ambientDbContextLocator;

        #endregion

        #region Protected Varibles
        protected IUserManager _userManager;

        #endregion

        #region Public Varibles

        protected DB_CONTEXT Context
        {
            get
            {
                // User MUST exist - Connection String
                if (_userManager == null) throw new NullReferenceException(_userManager.GetType().FullName);
                if (_userManager.UserIdentity == null) throw new NullReferenceException($"{_userManager.GetType().FullName}.UserIdentity");

                string userConnectionString = _userManager.UserIdentity.GetConnectionString();
                if (string.IsNullOrEmpty(userConnectionString)) throw new NullReferenceException($"{_userManager.GetType().FullName}.UserIdentity.GetConnectionString()");

                // DB Context
                DB_CONTEXT context = _ambientDbContextLocator?.Get<DB_CONTEXT>();
                if (context == null)
                    throw new InvalidOperationException($"No ambient DbContext of type {typeof(DB_CONTEXT)?.FullName} found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                if (context?.Database == null) throw new NullReferenceException($"{typeof(DB_CONTEXT)?.FullName}.Database");
                if (context?.Database?.Connection == null) throw new NullReferenceException($"{typeof(DB_CONTEXT)?.FullName}.Database.Connection");

                if (context.Database.Connection.State == System.Data.ConnectionState.Closed)
                {
                    context.Database.CommandTimeout = DBCommandTimeout;
                    context.Database.Connection.ConnectionString = userConnectionString;
                    context.Configuration.LazyLoadingEnabled = false;
                }

                return context;
            }
        }

        #endregion

        #region Constructors

        public BaseRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (userManager == null) throw new NullReferenceException(typeof(IUserManager).FullName);
            if (ambientDbContextLocator == null) throw new NullReferenceException(typeof(IAmbientDbContextLocator).FullName);

            _userManager = userManager;
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        #region Dispose

        private bool _disposed;

        ~BaseRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free other managed objects that implement IDisposable only
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Dispose

        #endregion Constructors

        #region Public Methods

        public SqlBulkCopy NewSqlBulkCopy(string destinationTableName)
        {
            string connectionString = _userManager.UserIdentity.GetConnectionString();
            return new SqlBulkCopy(connectionString)
            {
                DestinationTableName = destinationTableName
            };
        }

        public void SetTimeout(int seconds)
        {
            Context.Database.CommandTimeout = seconds;
        }

        #endregion

        #region Protected Properties

        protected string UserName
        {
            get
            {
                return _userManager.UserIdentity.UserName;
            }
        }

        #endregion Protected Properties
    }
}