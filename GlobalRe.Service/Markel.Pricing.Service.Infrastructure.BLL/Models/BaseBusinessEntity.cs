using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Models
{
    [Serializable]
    [DataContract]
    public abstract class BaseBusinessEntity : IBusinessEntity
    {
    }
}
