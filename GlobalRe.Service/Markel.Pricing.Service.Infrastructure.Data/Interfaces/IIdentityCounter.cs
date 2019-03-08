using System;

namespace Markel.Pricing.Service.Infrastructure.Data.Interfaces
{
    public interface IIdentityCounter<PRIMARY_KEY>
        where PRIMARY_KEY : IComparable
    {
        PRIMARY_KEY GetIdentity(string counterTypeName, int reserveCount = 1);
    }
}
