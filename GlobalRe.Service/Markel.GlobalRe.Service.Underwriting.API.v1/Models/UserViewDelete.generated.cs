using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class UserViewDelete
    {
        [JsonProperty("screenname")]
        public string ScreenName { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("keymember")]
        public bool KeyMember { get; set; }
    }
} 
