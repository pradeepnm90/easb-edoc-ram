using System;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class GroupNameValuePair : NameValuePair
    {
        #region Properties

        public string Group { get; private set; }

        public bool IsActive { get; private set; }

        #endregion Properties

        #region Constructors

        public GroupNameValuePair(string name, string value) : base(name, value)
        {
            IsActive = true;
        }

        public GroupNameValuePair(string name, string value, string group, bool isActive) : base(name, value)
        {
            Group = group;
            IsActive = isActive;
        }

        public GroupNameValuePair(LookupEntity lookupEntity) : base(lookupEntity.Description, lookupEntity.Code)
        {
            Group = lookupEntity.Group;
            IsActive = lookupEntity.IsActive;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Group))
            {
                return string.Format("'name': '{0}', 'value': '{1}'", Name, Value);
            }
            else
            {
                return string.Format("'name': '{0}', 'value': '{1}', 'group': '{2}'", Name, Value, Group);
            }
        }

        #endregion Methods
    }
}
