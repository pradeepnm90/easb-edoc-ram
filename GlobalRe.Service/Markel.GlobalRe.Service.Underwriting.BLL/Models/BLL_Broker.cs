using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_Broker : BaseGlobalReBusinessEntity
    {
        public int? Brokerid { get; set; } // Brokerid
        public string Broker { get; set; } // Broker
        public int? Parentgrouptypeid { get; set; } // parentgrouptypeid
        public string Parentgrouptype { get; set; } // parentgrouptype
        public int? Brokergroupid { get; set; } // Brokergroupid
        public string Brokergroupname { get; set; } // Brokergroupname
        public int Locationid { get; set; } // locationid
        public string Locationname { get; set; } // locationname
        public string Locationaddress { get; set; } // locationaddress
        public string Locationcity { get; set; } // locationcity
        public string Locationstate { get; set; } // locationstate
        public string Locationpostcode { get; set; } // locationpostcode
        public string Country { get; set; } // country
        public string Parentcompanyname { get; set; } // parentcompanyname
        public string Brokercategories { get; set; } // Brokercategories
        public string Brokercategoryid { get; set; } // Brokercategoryid
    }
}