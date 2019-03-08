using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_EntityLock:BaseGlobalReBusinessEntity
    {
		public int EntityID { get; set; }
		public string EntityTypeName { get; set; }
		public string LockedByDisplayName { get; set; }
		public int UserID { get; set; }
		public string LockingUser { get; set; }
		public DateTime LockedTimestamp { get; set; }
    }
}