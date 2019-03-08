using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces.Lookups;
using Markel.GlobalRe.Service.Underwriting.BLL.Interfaces;
using Markel.Pricing.Service.Infrastructure.Controllers;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.OutputCache.V2;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Controllers.Lookups
{
	[Authorize]
	[CacheOutput(NoCache = true)]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	[RoutePrefix(RouteHelper.LookupsAttachmentBasisRoutePrefix)]
	public class AttachmentBasisLookupController : BaseLookupsController
	{
		public AttachmentBasisLookupController(IUserManager userManager, IAttachmentBasisLookupManager attachmentBasisLookupManager) : base(userManager, attachmentBasisLookupManager) { }

	}
}