using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    [Serializable]
    public class BLL_CheckListTree : BaseGlobalReBusinessEntity
    {
        public int ChkListNum { get; set; }
        public string ChkListName { get; set; }
        public int SortOrder { get; set; }
        public bool ReadOnly { get; set; }
        public bool? Checked { get; set; }
        public int? PersonId { get; set; }
        public string PersonName { get; set; }
        public string CheckedDateTime { get; set; }
        public string Note { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}


