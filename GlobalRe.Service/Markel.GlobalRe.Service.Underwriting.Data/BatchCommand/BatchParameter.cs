using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.BatchCommand
{
	[ExcludeFromCodeCoverage]
	public class BatchParameter : DbParameter
    {
        public BatchParameter(string parameter, string value)
        {
            ParameterName = parameter;
            Value = value;
        }
        public override DbType DbType { get; set; }

        public override ParameterDirection Direction { get; set; }

        public override bool IsNullable { get; set; }

        public override string ParameterName { get; set; }

        public override int Size { get; set; }

        public override string SourceColumn { get; set; }

        public override bool SourceColumnNullMapping { get; set; }

        public override object Value { get; set; }

        public override void ResetDbType()
        {

        }
    }
}
