using Markel.GlobalRe.Service.Underwriting.Data.BatchCommand;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;

namespace Markel.GlobalRe.Service.Underwriting.Data.Databases
{
	public partial class ERMSDbContext
	{
		private ConcurrentDictionary<string, BatchEntityConfiguration> _batchConfiguration = null;

		[NotMapped]
		public ConcurrentDictionary<string, BatchEntityConfiguration> BatchConfiguration
		{
			get
			{
				if (_batchConfiguration == null)
					_batchConfiguration = new ConcurrentDictionary<string, BatchEntityConfiguration>();
				return _batchConfiguration;
			}
		}

		private ICollection<BatchQuery> _batchQueries = null;

		[NotMapped]
		public ICollection<BatchQuery> BatchQueries
		{
			get
			{
				if (_batchQueries == null)
					_batchQueries = new List<BatchQuery>();
				return _batchQueries;
			}
		}
	}
}
