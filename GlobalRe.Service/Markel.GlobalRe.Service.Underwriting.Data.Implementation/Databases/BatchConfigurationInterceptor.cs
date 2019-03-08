using Markel.GlobalRe.Service.Underwriting.Data.BatchCommand;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Databases
{
    public class BatchConfigurationInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
			//ERMSDbContext dbContext = interceptionContext.DbContexts.ToList().FirstOrDefault() as ERMSDbContext;
			//if (dbContext != null)
			//{
			//	if (!dbContext.BatchConfiguration.ContainsKey(typeof(UnderlyingCoverageExposureDetail).ToString()))
			//	{
			//		dbContext.BatchConfiguration.TryAdd(typeof(UnderlyingCoverageExposureDetail).ToString(),
			//													new BatchEntityConfiguration()
			//													{
			//														EntityMappedTable = String.Format("[dbo].[{0}]", dbContext.GetMappedTableName(typeof(UnderlyingCoverageExposureDetail)).First()),
			//														BatchEntityCommands = new List<BatchCommandType>() { BatchCommandType.DELETE, BatchCommandType.INSERT }
			//													});
			//	}
			//	if (!dbContext.BatchConfiguration.ContainsKey(typeof(UcedPropertyRatingUnit).ToString()))
			//	{
			//		dbContext.BatchConfiguration.TryAdd(typeof(UcedPropertyRatingUnit).ToString(),
			//												new BatchEntityConfiguration()
			//												{
			//													EntityMappedTable = String.Format("[dbo].[{0}]", dbContext.GetMappedTableName(typeof(UcedPropertyRatingUnit)).First()),
			//													BatchEntityCommands = new List<BatchCommandType>() { BatchCommandType.DELETE, BatchCommandType.INSERT }
			//												});
			//	}
			//}

		}
    }
}
