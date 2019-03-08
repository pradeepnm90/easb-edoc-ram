using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class CheckListTree
    {
        [JsonProperty("chkListNum")]
        public int ChkListNum { get; set; }

        [JsonProperty("chkListName")]
        public string ChkListName { get; set; }

        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }

        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; }

        [JsonProperty("checked")]
        public bool? Checked { get; set; }

        [JsonProperty("personId")]
        public int? PersonId { get; set; }

        [JsonProperty("personName")]
        public string PersonName { get; set; }

        [JsonProperty("checkedDateTime")]
        public string CheckedDateTime { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("personFirstName")]
        public string FirstName { get; set; }

        [JsonProperty("personLastName")]
        public string LastName { get; set; }

        [JsonProperty("personMiddleName")]
        public string MiddleName { get; set; }
    }
}