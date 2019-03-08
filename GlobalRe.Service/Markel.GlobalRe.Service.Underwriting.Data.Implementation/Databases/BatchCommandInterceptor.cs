using Markel.GlobalRe.Service.Underwriting.Data.BatchCommand;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Markel.GlobalRe.Service.Underwriting.Data.Databases
{
    public class BatchCommandInterceptor : DbCommandInterceptor
    {
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            ERMSDbContext dbContext = interceptionContext.DbContexts.ToList().FirstOrDefault() as ERMSDbContext;
            if (dbContext == null)
                return;

            if (ShouldBatchCommand(dbContext, command.CommandText))
            {
                ICollection<BatchParameter> parameters = new List<BatchParameter>();
                foreach (DbParameter par in command.Parameters)
                {
                    parameters.Add(new BatchParameter(par.ParameterName, par.Value.ToString()));
                }

                dbContext.BatchQueries.Add(new BatchQuery(command.CommandText, parameters));

                interceptionContext.Result = -1;
            }

            base.NonQueryExecuting(command, interceptionContext);
        }

        private bool ShouldBatchCommand(ERMSDbContext dbContext, string command)
        {
            bool isBatchCommand = false;
            foreach (var item in dbContext.BatchConfiguration.Values)
            {
                if (command.Contains(item.EntityMappedTable) && item.BatchEntityCommands.ToList().Exists(c => command.Contains(c.ToString())))
                {
                    isBatchCommand = true;
                    break;
                }
            }
            return isBatchCommand;
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //PS-Note : This is temporary solution to fix performance issue
            if(command.CommandText.EndsWith("OFFSET 0 ROWS FETCH NEXT 100 ROWS ONLY "))
            {
                command.CommandText = $"{command.CommandText} OPTION (MAXDOP 1)";
            }
            base.ReaderExecuting(command, interceptionContext);
        }

    }
}
