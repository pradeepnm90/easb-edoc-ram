using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System.Collections.Generic;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_DealCoverages : BaseGlobalReBusinessEntity
    {
        public int Dealnum { get; set; } // dealnum
        public int Cover_Id { get; set; } // cover_id
        public string Cover_Name { get; set; } // cover_name        
    }

}
