using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_Person : BaseGlobalReBusinessEntity
    {
        public int PersonId { get; set; } 
        public int? LocationId { get; set; } 
        public int? Nationality { get; set; } 
        public string LastName { get; set; } 
        public string FirstName { get; set; } 
        public string MiddleName { get; set; } 
        public string Prefix { get; set; } 
        public string Suffix { get; set; }
        public string Ssn { get; set; } 
        public DateTime? AppointDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; } 
        public string PersonAddress { get; set; } 
        public string PersonCity { get; set; } 
        public string PersonState { get; set; } 
        public string PersonPostCode { get; set; } 
        public int? PersonCountryId { get; set; } 
        public string ManagerName { get; set; } 
        public int? ManagerId { get; set; } 
        public string AssistantName { get; set; } 
        public int? AssistantId { get; set; } 
        public string PersonNotes { get; set; } 
        public string PersonAddress2 { get; set; } 
        public string PersonCity2 { get; set; } 
        public string PersonState2 { get; set; } 
        public string PersonPostCode2 { get; set; } 
        public int? PersonCountryId2 { get; set; } 
        public string CreatedBy { get; set; } 
        public DateTime? CreatedOn { get; set; } 
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string PersonName { get; set; } 
        public bool? Deleted { get; set; } 
        public string Biography { get; set; } 
        public string CommonName { get; set; } 
        public string FunctionalTitle { get; set; } 
        public int PersonSid { get; set; } 
        public int? CompanyId { get; set; } 
        public Guid? AmsContactGuid { get; set; } 
        public int? PersonStatus { get; set; } 
    }
}
