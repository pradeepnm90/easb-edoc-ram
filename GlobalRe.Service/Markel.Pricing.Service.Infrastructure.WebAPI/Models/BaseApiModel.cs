using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Helpers;
using Markel.Pricing.Service.Infrastructure.Interfaces;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    public abstract class BaseApiModel<BLL_CLASS> : IBaseApiModel where BLL_CLASS : IBusinessEntity
    {
        public BaseApiModel() { }
        public BaseApiModel(BLL_CLASS model) { Initialize(model); }

        protected abstract void Initialize(BLL_CLASS model);

        public abstract BLL_CLASS ToBLLModel();

        protected virtual bool HasValue()
        {
            return ObjectExtensions.HasValue(this);
        }
    }
}
