using System;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    [Serializable]
    public class BLL_ExposureTree : BaseGlobalReBusinessEntity
    {
        public int ? SubdivisionCode { get; set; }
        public string SubdivisionName { get; set; }
        public int ? ProductLineCode { get; set; }
        public string ProductLineName { get; set; }
        public int ? ExposureGroupCode { get; set; }
        public string ExposureGroupName { get; set; }
        public int ExposureTypeCode { get; set; }
        public string ExposureTypeName { get; set; }
    }
}

