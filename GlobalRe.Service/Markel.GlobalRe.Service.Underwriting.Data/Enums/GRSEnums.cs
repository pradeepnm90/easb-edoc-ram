using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Markel.GlobalRe.Service.Underwriting.Data.Enums
{
    public enum Role
    {
        None=0,
        [Description("GlobalRe.Actuary")]
        Actuary = 1,
        [Description("GlobalRe.Actuary Manager")]
        ActuaryManager = 2,
        [Description("GlobalRe.CAT Portfolio Management")]
        CAT_Portfolio_Management = 3,
        [Description("GlobalRe.Modeler")]
        Modeler = 4,
        [Description("GlobalRe.Modeler Manager")]
        ModelerManager = 5,
        [Description("GlobalRe.Property UA/TA")]
        Property_UATA = 6,
        [Description("GlobalRe.UA/TA")]
        UATA = 7,
        [Description("GlobalRe.Underwriter")]
        Underwriter = 8,
        [Description("GlobalRe.Underwriter Manager")]
        UnderwriterManager = 9
    }

    public enum NotetypeCategory
    {
        None = 0,
        [Description("NoteType")]
        CatID = 100,
        [Description("Activity")]
        Activity = 1
    }
    public enum NotetypeActive
    {
        None = 0,
        [Description("Activity")]
        Activity = 1
    }
}
