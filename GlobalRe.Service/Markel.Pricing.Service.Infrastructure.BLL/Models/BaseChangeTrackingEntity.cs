using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    [DataContract]
    public abstract class BaseChangeTrackingEntity : BaseMessageEntity, IChangeTrackingEntity
    {
        private List<string> _changedFields = new List<string>();

        public BaseChangeTrackingEntity() { }

        public virtual void AddChangedFields(IList<string> changedFields, bool reset = false)
        {
            if (reset) _changedFields.Clear();
            _changedFields.AddRange(changedFields);
        }

        public virtual ReadOnlyCollection<string> ChangedFields
        {
            get
            {
                return _changedFields.AsReadOnly();
            }
        }
    }
}
