using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    [Serializable]
    public class BLL_SubDivision : BaseGlobalReBusinessEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public IList<BLL_SubDivision> SubDivisions { get; set; }
    }
}
