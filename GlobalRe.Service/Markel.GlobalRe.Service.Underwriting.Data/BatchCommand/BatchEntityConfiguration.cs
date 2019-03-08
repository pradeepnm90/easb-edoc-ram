using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.BatchCommand
{
    public class BatchEntityConfiguration
    {
        public string EntityMappedTable { get; set; }
        public IList<BatchCommandType> BatchEntityCommands { get; set; }
    }
}
