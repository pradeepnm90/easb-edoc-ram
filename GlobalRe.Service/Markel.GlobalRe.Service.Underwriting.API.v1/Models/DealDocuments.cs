using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class DealDocuments : BaseApiModel<BLL_KeyDocuments>
    {
        public DealDocuments() { }
        public DealDocuments(BLL_KeyDocuments model) : base(model) { }

        public override BLL_KeyDocuments ToBLLModel()
        {
            BLL_KeyDocuments bLL_KeyDocuments = new BLL_KeyDocuments()
            {
                
                KeyDocid = KeyDocid,
                FileNumber = FileNumber,
                Producer = Producer,
                Docid = Docid,
                DocName = DocName,
                SortOrder = SortOrder,
                Location = Location,
                Drawer = Drawer,
                Folderid = Folderid,
                FolderName = FolderName,
                DocType = DocType,
                ErmsClassType = ErmsClassType,
                FileType = FileType,
                DmsPath = DmsPath,
                ItemCategoryid = ItemCategoryid,
                ErmsCategory = ErmsCategory,
                LastUpdatedUser = LastUpdatedUser,
                LastTimeStamp = LastTimeStamp,
                DmsCreated = DmsCreated,
                DmsUpdated = DmsUpdated
            };
            return bLL_KeyDocuments;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_KeyDocuments model)
        {
            KeyDocid = model.KeyDocid;
            FileNumber = model.FileNumber;
            Producer = model.Producer;
            Docid = model.Docid;
            DocName = model.DocName;
            SortOrder = model.SortOrder;
            Location = model.Location;
            Drawer = model.Drawer;
            Folderid = model.Folderid;
            FolderName = model.FolderName;
            DocType = model.DocType;
            ErmsClassType = model.ErmsClassType;
            FileType = model.FileType;
            DmsPath = model.DmsPath;
            ItemCategoryid = model.ItemCategoryid;
            ErmsCategory = model.ErmsCategory;
            LastUpdatedUser = model.LastUpdatedUser;
            LastTimeStamp = model.LastTimeStamp;
            DmsCreated = model.DmsCreated;
            DmsUpdated = model.DmsUpdated;
        }
    }
}