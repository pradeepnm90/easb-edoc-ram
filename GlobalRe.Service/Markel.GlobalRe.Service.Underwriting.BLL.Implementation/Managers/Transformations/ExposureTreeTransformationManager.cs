using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class ExposureTreeTransformationManager : BaseManager, IExposureTreeTransformationManager
    {
        #region Constructor

        public ExposureTreeTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        public IList<BLL_ExposureTree> Transform(IList<grs_VExposureTreeExt> exposureTree)
        {
            List<BLL_ExposureTree> data = new List<BLL_ExposureTree>();
            if (exposureTree.Count > 0)
            {
                exposureTree.ToList().ForEach((sGroup) =>
                {
                  
                    data.Add(new BLL_ExposureTree() {
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

    }
}
