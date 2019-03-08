
using LinqKit;
using Markel.GlobalRe.Service.Underwriting.Data.Databases;
using Markel.GlobalRe.Service.Underwriting.Data.Interfaces;
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Data.Extensions;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Exceptions;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using EntityFramework.DbContextScope.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Implementation.Repositories
{

	public class BrokerRepository : GenericRepository<ERMSDbContext, grs_VGrsBroker, int>, IBrokerRepository
	{

		public BrokerRepository(IUserManager userManager, IAmbientDbContextLocator ambientDbContextLocator) : base(userManager, ambientDbContextLocator) { }

	}
}
