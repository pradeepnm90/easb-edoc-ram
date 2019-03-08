
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class ContractTypes
    {

        [JsonProperty("name")]
        public string name { get; set; } //exposurename
        [JsonProperty("value")]
        public string value { get; set; } // exposuretype
        [JsonProperty("group")]
        public string group { get; set; } // AssumedName
        [JsonProperty("isActive")]
        public bool isActive { get; set; } //// active AND AssumedFlag






    }
}