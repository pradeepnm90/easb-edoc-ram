using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Transformations
{
    public interface ITransformationManager<BLL_CLASS, ENTITY> 
        where BLL_CLASS : BaseGlobalReBusinessEntity
        where ENTITY : class
    {
        BLL_CLASS Transform(ENTITY entity);
        IList<BLL_CLASS> Transform(IList<ENTITY> entities);
    }
}
