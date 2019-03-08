using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
	public interface IGlobalReBusinessManager<T> : IBaseManager where T : BaseGlobalReBusinessEntity
	{
		EntityResult<T> Get(int entityId);
		EntityResult<E> GetEntity<E>(T parentEntity, long? childEntityId = null) where E : BaseGlobalReBusinessEntity;
		EntityResult<IEnumerable<E>> GetAll<E>(T entity) where E : BaseGlobalReBusinessEntity;
		EntityResult<T> Create(object data = null);
		EntityResult<T> Update(T currentEntity, T changedEntity);
		EntityResult<T> Delete<E>(T entity, E deleteEntity) where E : BaseGlobalReBusinessEntity;
		EntityResult<T> Add<E>(T entity, E newEntity) where E : BaseGlobalReBusinessEntity;
		EntityResult<T> Save(T entity);
	}
}
