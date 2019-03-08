using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.Pricing.Service.Infrastructure.Models;
using System;
using System.Collections.Generic;
using Markel.Pricing.Service.Infrastructure.Logging;
using System.Diagnostics.CodeAnalysis;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers
{

	[ExcludeFromCodeCoverage]
	public abstract class BaseGlobalReManager<CLASS> : BaseBusinessManager, IGlobalReBusinessManager<CLASS>
		 where CLASS : BaseGlobalReBusinessEntity
    {
		#region Constants

		protected static readonly string BLL_TYPE_PREFIX = "BLL_";
		protected static readonly string BusinessEntityTypeName = typeof(CLASS).Name.Replace(BLL_TYPE_PREFIX, string.Empty);

		protected readonly string ADD_DELETE_ENTITY = "ADD_DELETE_ENTITY";
		protected readonly string ValidateMethod = string.Format("{0}.Validate", BusinessEntityTypeName);
		protected readonly string PreValidateUpdateMethod = string.Format("{0}.Update.PreValidate", BusinessEntityTypeName);
		protected readonly string PreValidateSaveMethod = string.Format("{0}.Save.PreValidate", BusinessEntityTypeName);
		protected readonly string AddDeleteGetAllMethodFormat = "{0}.{1}";
		protected readonly string PreValidateAddMethod = string.Format("{0}.Add.PreValidate", BusinessEntityTypeName);      //Entity added appended during method call
		protected readonly string PreValidateDeleteMethod = string.Format("{0}.Delete.PreValidate", BusinessEntityTypeName);//Entity deleted appended during method call
		protected readonly string ValidateGetAllMethod = string.Format("{0}.GetAll.Validate", BusinessEntityTypeName);      //Entity appended during method call
		protected readonly string ValidateEntityMethodFormat = "{0}.Validate";                                              //Entity appended during method call
		#endregion Constants

		#region Properties
		protected GlobalReContext GlobalReContext { get { return GlobalReContextManager.GlobalReContext; } }
		protected IGlobalReContextManager GlobalReContextManager { get; private set; }

		#endregion Properties

		#region Constructors

		public BaseGlobalReManager(IUserManager userManager,
		 						   ICacheStoreManager cacheStoreManager,
								   ILogManager logManager)
			: base(userManager, cacheStoreManager, logManager)
		{
		}

		public EntityResult<CLASS> Get(int entityId)
		{
			throw new NotImplementedException();
		}

		public EntityResult<E> GetEntity<E>(CLASS parentEntity, long? childEntityId = null) where E : BaseGlobalReBusinessEntity
		{
			throw new NotImplementedException();
		}

		public EntityResult<IEnumerable<E>> GetAll<E>(CLASS entity) where E : BaseGlobalReBusinessEntity
		{
			throw new NotImplementedException();
		}

		public EntityResult<CLASS> Create(object data = null)
		{
			throw new NotImplementedException();
		}

		public EntityResult<CLASS> Update(CLASS currentEntity, CLASS changedEntity)
		{
			throw new NotImplementedException();
		}

		public EntityResult<CLASS> Delete<E>(CLASS entity, E deleteEntity) where E : BaseGlobalReBusinessEntity
		{
			throw new NotImplementedException();
		}

		public EntityResult<CLASS> Add<E>(CLASS entity, E newEntity) where E : BaseGlobalReBusinessEntity
		{
			throw new NotImplementedException();
		}

		public EntityResult<CLASS> Save(CLASS entity)
		{
			throw new NotImplementedException();
		}


        //protected virtual bool OnApplyPropertyChange(CLASS currentEntity, CLASS changedEntity, string propertyName)
        //{
        //    return currentEntity.CopyPropertyValue(changedEntity, propertyName);
        //}

        #endregion Constructors



    }
}
