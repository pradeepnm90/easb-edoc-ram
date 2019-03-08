using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Managers;
using Markel.GlobalRe.Service.Underwriting.BLL.Enums;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Managers;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Cache;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Logging;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;




namespace Markel.GlobalRe.Service.Underwriting.BLL.Managers.Transformations
{
    public class CheckListsTransformationManager : BaseManager, ICheckListsTransformationManager
    {
        #region Constructor

        public CheckListsTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion


        public List<BLL_ChkCategoryTree> Transform(IList<grs_VGrsChecklistsByDeal> dealChklist)
        {
            List<BLL_ChkCategoryTree> data = new List<BLL_ChkCategoryTree>();

            dealChklist.OrderBy(a => a.Catorder).GroupBy(a => a.Category).ToList().ForEach((cat) =>
            {
                var catlist = cat.OrderBy(c => c.Sortorder).ToList();
                //if (catlist.Count > 1)
                {
                    var chkList = new List<BLL_CheckListTree>();
                    catlist.ForEach((category) =>
                    {
                        chkList.Add(new BLL_CheckListTree()
                        {
                            ChkListNum = category.Chklistnum,
                            ChkListName = category.ChecklistName,
                            SortOrder = category.Sortorder ?? int.MaxValue,
                            ReadOnly = category.Readonly ?? false,
                            Checked = category.Checked,
                            PersonId = category.PersonId,
                            PersonName = category.PersonName,
                            CheckedDateTime = category.CheckedDateTime?.ToString(),
                            Note = category.Note,
                            FirstName = category.FirstName,
                            LastName = category.LastName,
                            MiddleName = category.MiddleName
                        });
                    });

                    var parent = catlist.FirstOrDefault();
                    //PS-Note:Set Status code as NULL
                    data.Add(new BLL_ChkCategoryTree() { DealNumber = parent.Dealnum,
                        EntityNum = parent.Entitynum,
                        CategoryID = parent.Category ?? -1,
                        CategoryName = parent.CategoryName,
                        CatOrder = parent.Catorder??int.MaxValue,
                        Checklists = chkList.OrderBy(c => c.SortOrder).ToList()
                    });
                }
            //    else
            //    {
            //        catlist.ForEach((summary) =>
            //        {
            //            //PS-Note:Set value of StatusGroupName as Status name and StatusGroupSortOrder as SortOrder
            //            data.Add(new BLL_ChkCategoryTree() { DealNumber = summary.Dealnum,
            //                EntityNum = summary.Entitynum,
            //                CategoryID = summary.Category ?? -1,
            //                CategoryName = summary.CategoryName,
            //                CatOrder = summary.CatOrder,
            //                Checklists =  null
            //            });
            //        });
            //    }
            });
            return data.ToList();
        }

        public BLL_ChkCategoryTree Transform(IList<grs_VGrsChecklistsByDeal> dealChklist, int count)
        {
            List<BLL_ChkCategoryTree> data = new List<BLL_ChkCategoryTree>();

            dealChklist.OrderBy(a => a.Catorder).GroupBy(a => a.Category).ToList().ForEach((cat) =>
            {
                var catlist = cat.OrderBy(c => c.Sortorder).ToList();
                {
                    var chkList = new List<BLL_CheckListTree>();
                    catlist.ForEach((category) =>
                    {
                        chkList.Add(new BLL_CheckListTree()
                        {
                            ChkListNum = category.Chklistnum,
                            ChkListName = category.ChecklistName,
                            SortOrder = category.Sortorder ?? int.MaxValue,
                            ReadOnly = category.Readonly ?? false,
                            Checked = category.Checked,
                            PersonId = category.PersonId,
                            PersonName = category.PersonName,
                            CheckedDateTime = category.CheckedDateTime?.ToString(),
                            Note = category.Note,
                            FirstName = category.FirstName,
                            LastName = category.LastName,
                            MiddleName = category.MiddleName
                        });
                    });

                    var parent = catlist.FirstOrDefault();
                    //PS-Note:Set Status code as NULL
                    data.Add(new BLL_ChkCategoryTree()
                    {
                        DealNumber = parent.Dealnum,
                        EntityNum = parent.Entitynum,
                        CategoryID = parent.Category ?? -1,
                        CategoryName = parent.CategoryName,
                        CatOrder = parent.Catorder??int.MinValue,
                        Checklists = chkList.OrderBy(c => c.SortOrder).ToList()
                    });
                }
                
            });
            return data[0];
        }
    }
}

