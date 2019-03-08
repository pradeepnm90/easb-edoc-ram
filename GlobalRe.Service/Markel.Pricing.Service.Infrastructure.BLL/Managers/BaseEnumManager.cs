using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseEnumManager<T> : BaseLookupManager where T : struct
    {
        #region Constructors

        public BaseEnumManager(IUserManager userManager) : base(userManager) { }

        #endregion

        protected internal override IList<LookupEntity> GetLookupData()
        {
            return EnumExtension.ToLookupEntity<T>();
        }
    }
}
