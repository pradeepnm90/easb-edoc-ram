using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System.Collections.Generic;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_ContractTypes: BaseGlobalReBusinessEntity
    {
        public string name { get; set; } // AssumedName
        public string value { get; set; } // code
        public string group { get; set; } // exposuretype
        public bool isActive { get; set; } // active AND AssumedFlag
    }

}
