using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.ComponentModel;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_CheckListParameter : BaseGlobalReBusinessEntity
    {
        //[DefaultValue(1)]
        public int? Entitynum { get; set; } = 1; // entitynum (Primary key)
        public int Checklistnum { get; set; } // chklistnum (Primary key)
        public int Dealnumber { get; set; } // key1 (Primary key)
        public DateTime? Completed { get; set; } // completed
        public int? PersonId { get; set; } // PersonId
        public string Notes { get; set; } // Notes
        public bool? check { get; set; }
        public string PersonName { get; set; } // Notes
        public string CompletedDateTime { get; set; }
    }
}


