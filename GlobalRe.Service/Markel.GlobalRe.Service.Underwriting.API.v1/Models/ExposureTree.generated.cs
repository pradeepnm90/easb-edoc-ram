using Newtonsoft.Json;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    public partial class ExposureTree
    {

        [JsonProperty("subdivisioncode")]
        public int ? SubdivisionCode { get; set; }

        [JsonProperty("subdivisionname")]
        public string SubdivisionName { get; set; }

        [JsonProperty("productLinecode")]
        public int ? ProductLineCode { get; set; }

        [JsonProperty("productLinename")]
        public string ProductLineName { get; set; }

        [JsonProperty("exposuregroupcode")]
        public int ? ExposureGroupCode { get; set; }

        [JsonProperty("exposuregroupname")]
        public string ExposureGroupName { get; set; }

        [JsonProperty("exposuretypecode")]
        public int ExposureTypeCode { get; set; }

        [JsonProperty("exposuretypename")]
        public string ExposureTypeName { get; set; }
    }
    // end class

} // end Models namespace


