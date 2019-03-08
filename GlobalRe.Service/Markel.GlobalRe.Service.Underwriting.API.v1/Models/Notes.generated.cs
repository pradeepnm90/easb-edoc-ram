    
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
    {
    public partial class Notes
    {

        // [Required]
        [JsonProperty("notenum")]
        public int Notenum { get; set; }

        [Required]
        [JsonProperty("dealNumber")]
        public int? DealNumber { get; set; }

        [JsonProperty("notedate")]
        public DateTime? Notedate { get; set; }

        [JsonProperty("notetype")]
        public int? Notetype { get; set; }

        [JsonProperty("notes")]
        public string NoteText { get; set; }
               
        [JsonProperty("whoentered")]
        public int? Whoentered { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } // Name
        [JsonProperty("FirstName")]
        public string FirstName { get; set; } // FirstName
        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; } // MiddleName
        [JsonProperty("LastName")]
        public string LastName { get; set; } // LastName

        [JsonProperty("createdBy")]
        public int? CreatedBy { get; set; }

    }
       // end class

} // end Models namespace


