using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.BatchCommand
{
	[ExcludeFromCodeCoverage]
	public class BatchQuery
    {
        public BatchQuery()
        {
            CommandParameters = new List<BatchParameter>();
        }

        public BatchQuery(string commandText, IEnumerable<DbParameter> parameters)
        {
            CommandText = commandText;
            CommandParameters = parameters;
        }
        public string CommandText { get; set; }
        public IEnumerable<DbParameter> CommandParameters { get; set; }
    }
}
