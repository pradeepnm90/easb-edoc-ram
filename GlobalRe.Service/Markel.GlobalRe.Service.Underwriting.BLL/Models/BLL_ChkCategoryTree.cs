using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    [Serializable]
    public class BLL_ChkCategoryTree : BaseGlobalReBusinessEntity
    {
        public int DealNumber { get; set; }
        public int? EntityNum { get; set; } = 1;
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CatOrder { get; set; }
        public IList<BLL_CheckListTree> Checklists { get; set; }
    }
}

