using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseManager : IBaseManager
    {
        #region Constants

        protected const string ERROR_ID_NOT_FOUND = "'{0}' was not found in the system!";
        protected const string ERROR_TYPE_ID_NOT_FOUND = "'{0}' with ID '{1}' was not found in the system!";
        protected const string ERROR_NO_CONTENT = "Could not create '{0}'!";
        protected const string ERROR_ADD_NO_CONTENT = "Could not add '{0}' to '{1}'!";
        protected const string ERROR_DELETE_NO_CONTENT = "Could not delete '{0}' from '{1}'!";
        protected const string ERROR_SAVE_NO_CONTENT = "Could not save '{0}'!";
        protected const string ERROR_UPDATE_NO_CONTENT = "Could not update sub-entity '{0}' of '{1}'!";

        protected const string MESSAGE_SUCCESSFULLY_DELETED = "{0} '{1}' was successfully deleted!";

        #endregion Constants

        #region Properties

        private IUserManager UserManager;

        protected UserIdentity UserIdentity
        {
            get
            {
                if (UserManager.UserIdentity == null) throw new UnauthorizedAPIException();
                return UserManager.UserIdentity;
            }
        }

        protected ServiceToken GetServiceToken(string serviceName)
        {
            // TODO: Service Name should be an enum but it's weird having Service Names in Infrastructure. Need to revisit.
            ServiceToken serviceToken = UserManager.GetServiceToken(serviceName);
            if (serviceToken == null) throw new UnauthorizedAPIException();
            return serviceToken;
        }

        protected bool IsBackgroundProcess { get { return UserManager.IsBackgroundProcess; } }

        #endregion Properties

        #region Constructors

        public BaseManager(IUserManager userManager)
        {
            UserManager = ValidateObject(userManager);
        }

        protected static string GetFieldName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;
            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }
            string propertyName = me.Member.Name;
            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }

        #endregion Constructors

        #region Validation Methods

        /// <summary>
        /// Validates manager is not null and returns instance of manager
        /// </summary>
        /// <typeparam name="T">Type of Manager</typeparam>
        /// <param name="manager">Instance of Manager</param>
        /// <returns>Original instance of Manager</returns>
        protected T ValidateManager<T>(T manager) where T : IBaseManager
        {
            return ValidateObject(manager);
        }

        /// <summary>
        /// Validates repository is not null and returns instance of repository
        /// </summary>
        /// <typeparam name="T">Type of Repository</typeparam>
        /// <param name="repository">Instance of Repository</param>
        /// <returns>Original instance of Repository</returns>
        protected T ValidateRepository<T>(T repository) where T : IBaseRepository
        {
            return ValidateObject(repository);
        }

        /// <summary>
        /// Validates object is not null and returns object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="instance">Instance of Object</param>
        /// <returns>Original instance of object</returns>
        protected T ValidateObject<T>(T instance)
        {
            return instance.ValidateObject();
        }

        #endregion Validation Methods

        #region DBContext Scope Methods

        #region Variables

        private readonly IDbContextScopeFactory _dbContextScopeFactory = new DbContextScopeFactory();

        #endregion Variables

        protected void RunInContextScope(Action action, bool isReadOnly = false)
        {
            RunInContextScope<object>(() =>
            {
                action();

                return null;
            }, isReadOnly);
        }

        protected T RunInContextScope<T>(Func<T> function, bool isReadOnly = false)
        {
            if (isReadOnly)
            {
                using (IDbContextReadOnlyScope dbContextScope = _dbContextScopeFactory.CreateReadOnly())
                {
                    return function();
                }
            }
            else
            {
                using (IDbContextScope dbContextScope = _dbContextScopeFactory.Create())
                {
                    T result = function();
                    dbContextScope.SaveChanges();
                    return result;
                }
            }
        }

        protected T RunInNewContextScope<T>(Func<T> function)
        {
            using (IDbContextScope dbContextScope = _dbContextScopeFactory.Create(DbContextScopeOption.ForceCreateNew))
            {
                T result = function();
                dbContextScope.SaveChanges();
                return result;
            }
        }

        #endregion DBContext Scope Methods
    }
}
