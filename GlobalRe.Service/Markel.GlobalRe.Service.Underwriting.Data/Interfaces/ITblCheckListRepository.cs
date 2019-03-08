
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface ITblCheckListRepository : IGenericRepository<TbChklistval, int>
    {
        IList<grs_VGrsChecklistsByDeal> GetAllDealChecklists(int dealnum);
        IList<grs_VGrsChecklistsByDeal> GetCheckNumByDealChecklists(int dealnum, int chklistnum);
        string GetPersonByUserId(int personId);
        int IsValidDealCheckedStatus(int dealnumber, int checklistnumber);
    }

}
