using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class DealStatusSummariesTransformationManager : BaseManager, IDealStatusSummariesTransformationManager
    {

        #region Constructor

        public DealStatusSummariesTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public List<BLL_DealStatusSummary> Transform(IList<grs_PrGetGrsDealCountByStatus> dealStatusSummary)
        {
            //PS: Refactoring
            List<BLL_DealStatusSummary> data = new List<BLL_DealStatusSummary>();

            dealStatusSummary.GroupBy(a => a.StatusGroup).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                if (statusGroupData.Count > 1)
                {
                    var childData = new List<BLL_DealStatusSummary>();
                    statusGroupData.ForEach((summary) =>
                    {
                        childData.Add(new BLL_DealStatusSummary() { StatusCode = summary.StatusCode, StatusName = summary.StatusName, SortOrder = summary.StatusSortOrder, Count = summary.Count, WorkflowId = summary.WorkflowID, WorkflowName = summary.WorkflowName, DealStatusSummary = null });
                    });

                    var parent = statusGroupData.FirstOrDefault();
                    //PS-Note:Set Status code as NULL
                    data.Add(new BLL_DealStatusSummary() { StatusCode = null, StatusName = parent.StatusGroupName, SortOrder = parent.StatusGroupSortOrder, WorkflowId = parent.WorkflowID, WorkflowName = parent.WorkflowName, Count = statusGroupData.Select(a => a.Count).Sum(), DealStatusSummary = childData.OrderBy(c => c.SortOrder).ToList() });
                }
                else
                {
                    statusGroupData.ForEach((summary) =>
                    {
                        //PS-Note:Set value of StatusGroupName as Status name and StatusGroupSortOrder as SortOrder
                        data.Add(new BLL_DealStatusSummary() { StatusCode = summary.StatusCode, StatusName = summary.StatusGroupName, SortOrder = summary.StatusGroupSortOrder, Count = summary.Count, WorkflowId = summary.WorkflowID, WorkflowName = summary.WorkflowName, DealStatusSummary = null });
                    });
                }
            });
            return data.OrderBy(c => c.SortOrder).ToList();
        }

        public List<BLL_ExposureTree> Transform(IList<grs_VExposureTreeExt> exposureTree)
        {
            List<BLL_ExposureTree> data = new List<BLL_ExposureTree>();
            if (exposureTree.Count > 0)
            {
                exposureTree.ToList().ForEach((sGroup) =>
                {

                    data.Add(new BLL_ExposureTree()
                    {
                        SubdivisionCode = sGroup.SubdivisionCode,
                        SubdivisionName = sGroup.SubdivisionName,
                        ProductLineCode = sGroup.ProductLineCode,
                        ProductLineName = sGroup.ProductLineName,
                        ExposureGroupCode = sGroup.ExposureGroupCode,
                        ExposureGroupName = sGroup.ExposureGroupName,
                        ExposureTypeCode = sGroup.ExposureTypeCode,
                        ExposureTypeName = sGroup.ExposureTypeName
                    });

                });
                return data.OrderBy(c => c.ExposureTypeName).ToList();
            }
            return null;
        }

        #endregion Transform



    }
}
