using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    public class NameValuePair : IBaseApiModel
    {
        #region Properties

        public string Name { get; private set; }

        public object Value { get; private set; }

        #endregion Properties

        #region Constructors

        public NameValuePair(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public NameValuePair(KeyValuePair<string, object> keyValuePair)
        {
            Name = keyValuePair.Key;
            Value = keyValuePair.Value;
        }

        #endregion Constructors

        #region Methods

        public override string ToString()
        {
            return string.Format("'name': '{0}', 'value': '{1}'", Name, Value);
        }

        #endregion Methods
    }
}
