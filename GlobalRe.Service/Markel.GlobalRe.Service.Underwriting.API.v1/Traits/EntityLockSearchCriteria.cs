using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Extensions;
using Markel.Pricing.Service.Infrastructure.Traits;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Traits
{
	public class EntityLockSearchCriteria : BaseSearchCriteria
	{
		public int? CategoryId { get; set; }
		public int? ItemId { get; set; }
		public int? UserId { get; set; }
		public SearchCriteria ToSearchCriteria()
		{
			List<Parameter> parameters = new List<Parameter>();

			parameters.AddIf("Category", CategoryId);
			parameters.AddIf("Item", ItemId);
			parameters.AddIf("User", UserId);

			return new SearchCriteria(
				sortedFields: this.ToSortedFieldCollection(),
				pageIndex: this.PageIndex,
				pageSize: this.PageSize,
				parameters: parameters
			);
		}
	}
}