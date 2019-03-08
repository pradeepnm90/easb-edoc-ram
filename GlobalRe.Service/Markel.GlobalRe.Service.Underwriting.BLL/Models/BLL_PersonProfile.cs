using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Models
{
    public class BLL_PersonProfile : BaseGlobalReBusinessEntity
    {
        public IList<int> DefaultSubDivisions { get; set; }
    }
}
