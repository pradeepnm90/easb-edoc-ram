using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    public enum CrudAction
    {
        None,
        Create,
        Update,
        Delete,
        Lock,
        Unlock,
        RenewLease,
    }
}
