using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Enums
{
    public enum Rules
    {
        [Description("Default view based on Subdivision/Exposure Groupings")]
        DefaultSubDivisionsAtStart,
        [Description("Default view based on current user")]
        ShowSubmissionsForCurrentUser,
        [Description("Default view based on manager")]
        ShowSubmissionsForMyEmployees
    }
}
