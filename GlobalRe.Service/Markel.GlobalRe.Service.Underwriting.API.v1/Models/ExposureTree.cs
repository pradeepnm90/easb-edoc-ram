using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class ExposureTree : BaseApiModel<BLL_ExposureTree>
    {
        public ExposureTree() { }
        public ExposureTree(BLL_ExposureTree model) : base(model) { }

        public override BLL_ExposureTree ToBLLModel()
        {
            BLL_ExposureTree bLL_ExposureTree = new BLL_ExposureTree()
            {
                SubdivisionCode = SubdivisionCode,
                SubdivisionName = SubdivisionName,
                ProductLineCode = ProductLineCode,
                ProductLineName = ProductLineName,
                ExposureGroupCode = ExposureGroupCode,
                ExposureGroupName = ExposureGroupName,
                ExposureTypeCode = ExposureTypeCode,
                ExposureTypeName = ExposureTypeName,
            };

            return bLL_ExposureTree;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_ExposureTree model)
        {

            SubdivisionCode = model.SubdivisionCode;
            SubdivisionName = model.SubdivisionName;
            ProductLineCode = model.ProductLineCode;
            ProductLineName = model.ProductLineName;
            ExposureGroupCode = model.ExposureGroupCode;
            ExposureGroupName = model.ExposureGroupName;
            ExposureTypeCode = model.ExposureTypeCode;
            ExposureTypeName = model.ExposureTypeName;
        }
    }
}