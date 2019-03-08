using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class ChkCategoryTree
    {
        [JsonProperty("dealNum")]
        public int DealNumber { get; set; }

        [JsonProperty("entityNum")]
        public int? EntityNum { get; set; } = 1;

        [JsonProperty("categoryId")]
        public int CategoryID { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("catOrder")]
        public int CatOrder { get; set; }

        [JsonProperty("Checklists")]
        public IList<CheckListTree> Checklists { get; set; }
    }
}