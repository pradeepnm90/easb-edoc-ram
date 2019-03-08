
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class DealDocumentsTransformationManager : BaseManager, IDealDocumentsTransformationManager
    {
        #region Constructor

        public DealDocumentsTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        #region Transform

        public BLL_KeyDocuments Transform(grs_VKeyDocument dbModel)
        {
            return new BLL_KeyDocuments()
            {
                KeyDocid = dbModel.Keydocid,
                FileNumber = dbModel.Filenumber,
                Producer = dbModel.Producer,
                Docid = dbModel.Docid,
                DocName = dbModel.Docname,
                SortOrder = dbModel.Sortorder,
                Location = dbModel.Location,
                Drawer = dbModel.Drawer,
                Folderid = dbModel.Folderid,
                FolderName = dbModel.Foldername,
                DocType = dbModel.Doctype,
                ErmsClassType = dbModel.Ermsclasstype,
                FileType = dbModel.Filetype,
                DmsPath = dbModel.Dmspath,
                ItemCategoryid = dbModel.Itemcategoryid,
                ErmsCategory = dbModel.Ermscategory,
                LastUpdatedUser = dbModel.Lastupdateduser,
                LastTimeStamp = dbModel.Lasttimestamp,
                DmsCreated= dbModel.Dmscreated,
                DmsUpdated= dbModel.Dmsupdated
            };
        }

        public List<BLL_KeyDocuments> Transform(IList<grs_VKeyDocument> CoverageList)
        {

            List<BLL_KeyDocuments> coverDataItems = new List<BLL_KeyDocuments>();

            CoverageList.GroupBy(a => a.Filenumber).ToList().ForEach((sGroup) =>
            {
                var statusGroupData = sGroup.ToList();
                statusGroupData.ForEach((summary) =>
                {
                    coverDataItems.Add(new BLL_KeyDocuments()
                    {
                        KeyDocid = summary.Keydocid,
                        FileNumber = summary.Filenumber,
                        Producer = summary.Producer,
                        Docid = summary.Docid,
                        DocName = summary.Docname,
                        SortOrder = summary.Sortorder,
                        Location = summary.Location,
                        Drawer = summary.Drawer,
                        Folderid = summary.Folderid,
                        FolderName = summary.Foldername,
                        DocType = summary.Doctype,
                        ErmsClassType = summary.Ermsclasstype,
                        FileType = summary.Filetype,
                        DmsPath = summary.Dmspath,
                        ItemCategoryid = summary.Itemcategoryid,
                        ErmsCategory = summary.Ermscategory,
                        LastUpdatedUser = summary.Lastupdateduser,
                        LastTimeStamp = summary.Lasttimestamp,
                        DmsCreated = summary.Dmscreated,
                        DmsUpdated = summary.Dmsupdated
                    });
                });
            });

            return coverDataItems.OrderBy(c => c.SortOrder).ToList();
        }

        #endregion Transform

    }
}
