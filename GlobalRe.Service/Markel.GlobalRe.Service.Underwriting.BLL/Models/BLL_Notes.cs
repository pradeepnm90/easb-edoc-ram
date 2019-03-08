using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_Notes : BaseGlobalReBusinessEntity
    {
        public int Notenum { get; set; } // notenum
        public int? Dealnum { get; set; } // dealnum
        public DateTime? Notedate { get; set; } // notedate
        public int? Notetype { get; set; } // notetype
        public string Notes { get; set; } // notes
        public int? Whoentered { get; set; } // whoentered
        public string Name { get; set; } // Name
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public int? CreatedBy { get; set; }
    }
}
