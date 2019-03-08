using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_Cedant : BaseGlobalReBusinessEntity
    {
        public int? Cedantid { get; set; } // cedantid
        public string Cedant { get; set; } // cedant
        public int? Parentgrouptypeid { get; set; } // parentgrouptypeid
        public string Parentgrouptype { get; set; } // parentgrouptype
        public int? Cedantgroupid { get; set; } // Cedantgroupid
        public string Cedantgroupname { get; set; } // Cedantgroupname
        public int Locationid { get; set; } // locationid
        public string Locationname { get; set; } // locationname
        public string Locationaddress { get; set; } // locationaddress
        public string Locationcity { get; set; } // locationcity
        public string Locationstate { get; set; } // locationstate
        public string Locationpostcode { get; set; } // locationpostcode
        public string Country { get; set; } // country
        public string Parentcompanyname { get; set; } // parentcompanyname
        public string Cedantcategories { get; set; } // cedantcategories
        public string Cedantcategoryid { get; set; } // cedantcategoryid
    }
}