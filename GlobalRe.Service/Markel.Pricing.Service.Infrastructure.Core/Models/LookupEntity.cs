using Markel.Pricing.Service.Infrastructure.Extensions;
using System;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class LookupEntity
    {
        #region Properties

        public int ID { get; private set; }

        public string Code { get; private set; }

        public string Description { get; private set; }

        public string Group { get; private set; }

        public bool IsActive { get; private set; }

        #endregion Properties

        #region Constructors

        public LookupEntity()
        {
            IsActive = true;
        }

        public LookupEntity(int id, string code)
        {
            ID = id;
            Code = code;
            IsActive = true;
        }

        public LookupEntity(int id, string code, string description)
        {
            ID = id;
            Code = code;
            Description = description;
            IsActive = true;
        }

        public LookupEntity(int id, string code, string description, bool isActive)
        {
            ID = id;
            Code = code;
            Description = description;
            IsActive = isActive;
        }

        public LookupEntity(int id, string code, string description, string group, bool isActive)
        {
            ID = id;
            Code = code;
            Description = description;
            Group = group;
            IsActive = isActive;
        }

        public LookupEntity(Enum enumeration)
        {
            ID = Convert.ToInt32(enumeration);
            Code = enumeration.ToString();
            Description = enumeration.EnumDescription();
            IsActive = true;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Description))
            {
                return string.Format("'ID': '{0}', 'Code': '{1}'", ID, Code);
            }
            else if (string.IsNullOrEmpty(Group))
            {
                return string.Format("'ID': '{0}', 'Code': '{1}', 'Description': '{2}'", ID, Code, Description);
            }
            else
            {
                return string.Format("'ID': '{0}', 'Code': '{1}', 'Description': '{2}', 'Group': '{3}'", ID, Code, Description, Group);
            }
        }

        #endregion Methods
    }
}
