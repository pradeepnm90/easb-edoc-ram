using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Managers
{
    public abstract class BaseLookupManager : BaseManager, ILookupsManager
    {
        #region Constructors

        public BaseLookupManager(IUserManager userManager) : base(userManager) { }

        #endregion

        protected internal abstract IList<LookupEntity> GetLookupData();

        public IList<LookupEntity> GetAll()
        {
            return RunInContextScope(() =>
            {
                return GetLookupData();
            }, true);
        }

        public IEnumerable<LookupEntity> GetByGroupName(string groupName)
        {
            IList<LookupEntity> lookupData = GetLookupData();
            //return lookupData.Where(pf => pf.Group.Equals(groupName));
            return lookupData.Where(pf => groupName.Contains(pf.Group));
        }

        public LookupEntity GetByID(int? id)
        {
            if (!id.HasValue) return null;

            IList<LookupEntity> lookupData = GetLookupData();
            return lookupData.FirstOrDefault(l => l.ID == id);
        }

        public LookupEntity GetByCode(string code)
        {
            IList<LookupEntity> lookupData = GetLookupData();
            return lookupData.FirstOrDefault(l => l.Code.Equals(code));
        }

        public LookupEntity GetByCode(string group, string code)
        {
            IList<LookupEntity> lookupData = GetLookupData();
            return lookupData.FirstOrDefault(l => l.Group.Equals(group) && l.Code.Equals(code));
        }

        public int? GetIDByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;

            return GetByCode(code)?.ID;
        }

        public int? GetIDByCode(string group, string code)
        {
            IList<LookupEntity> lookupData = GetLookupData();
            return lookupData.FirstOrDefault(l =>
                l.Group.Equals(group) &&
                l.Code.Equals(code)
            )?.ID;
        }

        public int? GetIDByDescription(string group, string description)
        {
            IList<LookupEntity> lookupData = GetLookupData();
            return lookupData.FirstOrDefault(l =>
                l.Group.Equals(group) &&
                l.Description.Equals(description)
            )?.ID;
        }

        

    }
}
