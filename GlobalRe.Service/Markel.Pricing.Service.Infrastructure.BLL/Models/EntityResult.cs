using Markel.Pricing.Service.Infrastructure.Interfaces;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public class EntityResult<T> : Result where T : class
    {
        #region Properties

        public T Data { get; private set; }

        #endregion Properties

        #region Constructors

        public EntityResult() { }

        public EntityResult(T data)
        {
            Data = data;
        }

        #endregion Constructors
    }
}
