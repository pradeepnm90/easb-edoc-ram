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
    public class ContractTypesTransformationManager : BaseManager, IContractTypesTransformationManager
    {
        #region Constructor

        public ContractTypesTransformationManager(IUserManager userManager) : base(userManager) { }

        #endregion

        public List<BLL_ContractTypes> Transform(IList<grs_VGrsContractType> contractType)
        {
            List<BLL_ContractTypes> data = new List<BLL_ContractTypes>();
            if (contractType.Count > 0)
            {
                contractType.GroupBy(a => a.RowId).ToList().ForEach((sContract) =>
                {
                    var contractGroupData = sContract.ToList();
                    contractGroupData.ForEach((summary) =>
                    {
                        data.Add(new BLL_ContractTypes() { name = Convert.ToString(summary.AssumedName), value = Convert.ToString(summary.Code), group = Convert.ToString(summary.Exposuretype), isActive = (Convert.ToBoolean(summary.AssumedFlag) && Convert.ToBoolean(summary.Active)) });
                    });
                });
                return data.ToList();
            }
            return null;
        }



    }
}
