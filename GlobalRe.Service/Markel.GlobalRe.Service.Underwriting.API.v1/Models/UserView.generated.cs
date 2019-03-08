using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class UserView
    {

        // [Required]
        [JsonProperty("viewId")]
        public int ViewId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [Required]
        [JsonProperty("screenName")]
        public string ScreenName { get; set; }

        [Required]
        [JsonProperty("viewname")]
        public string ViewName { get; set; }

        [JsonProperty("default")]
        public bool ? Default { get; set; }

        [Required]
        [JsonProperty("layout")]
        public string Layout { get; set; }
       // public DateTime UserViewCreationDate { get; set; }
        [JsonProperty("customView")]
        public bool? CustomView { get; set; }

        [JsonProperty("sortOrder")]
        public int? SortOrder { get; set; }
    }
    // end class
} // end Models namespace
