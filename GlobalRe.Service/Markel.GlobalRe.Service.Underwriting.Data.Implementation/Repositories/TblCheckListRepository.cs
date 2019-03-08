using System.Collections.Generic;
using System.Linq;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using EntityFramework.DbContextScope.Interfaces;
using System;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{
    public class TblCheckListRepository : GenericRepository<ERMSDbContext, TbChklistval, int>, ITblCheckListRepository
    {
        public TblCheckListRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

        public IList<grs_VGrsChecklistsByDeal> GetAllDealChecklists(int dealnum)
        {
            return Context.grs_VGrsChecklistsByDeals.AsNoTracking().Where(c => c.Dealnum == dealnum && c.Entitynum ==1).ToList();
        }
        public IList<grs_VGrsChecklistsByDeal> GetCheckNumByDealChecklists(int dealnum, int chklistnum)
        {
            return Context.grs_VGrsChecklistsByDeals.AsNoTracking().Where(c => c.Dealnum == dealnum && c.Chklistnum == chklistnum).ToList();
        }
        public string GetPersonByUserId(int personId)
        {
            var username = Context.TbPersons.Where(p => p.PersonId == personId).Select(p => p.PersonName).ToList();
            if (username.Count > 0)
            {
                return username[0].ToString();
            }
            return null;
        }

        public int IsValidDealCheckedStatus(int dealnumber, int checklistnumber)
        {
            int status = -1;
            var dealnumberdata = Context.grs_VGrsChecklistsByDeals.Where(p => p.Dealnum == dealnumber && p.Chklistnum == checklistnumber).Select(p => p.Checked).ToList();
            if (dealnumberdata.Count > 0)
            {
                bool statuschecked = Convert.ToBoolean(dealnumberdata[0].Value);
                if (statuschecked == true)
                {
                    status = 1;
                }
                else 
                {
                    status = 0;
                }
            }
            return status;
        }
    }

}
