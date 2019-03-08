using System;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Models
{
    public class SubDivisionComparer : IEqualityComparer<SubDivision>
    {
        public bool Equals(SubDivision x, SubDivision y)
        {
            return x.ExposureGroupId == y.ExposureGroupId;
        }

        public int GetHashCode(SubDivision subDivision)
        {
            if (Object.ReferenceEquals(subDivision, null)) return 0;
            return subDivision.ExposureGroupId.GetHashCode();
        }
    }
    public class SubDivision
    {
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public int ExposureGroupId { get; set; }
        public string ExposureGroup { get; set; }
        public int SubdivisionSortOrder { get; set; }
        public int ExposureGroupSortOrder { get; set; }
    }
}
