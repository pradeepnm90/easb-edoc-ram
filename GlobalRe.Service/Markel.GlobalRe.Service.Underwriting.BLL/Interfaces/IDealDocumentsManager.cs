using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{
    public interface IDealDocumentsManager : ISearchableManager<BLL_KeyDocuments>
    {
        EntityAction GetEntityActions(BLL_KeyDocuments bLL_KeyDocuments);

        EntityResult<IEnumerable<BLL_KeyDocuments>> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure);

        Boolean CheckIfDealExistsInSystem(int filenumber);

        string GetFileType(int filenumber);

        HttpResponseMessage GetRegisterToken(string baseUrl, string apiUser, string apiPwd);

        HttpResponseMessage GetDocumentSchema(string baseUrl, string token, string dealNumber);

        HttpResponseMessage GetDocuments(string baseurl, string token, string documentid, Boolean includeFileContents);

        HttpResponseMessage GetDocumentContent(string baseUrl, string token, string documentId, Boolean includeFileContents, int pageNumber);

        //EntityResult<BLL_KeyDocuments> AddKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);

        //EntityResult<BLL_KeyDocuments> DeleteKeyDocuments(BLL_KeyDocuments bLL_KeyDocuments);
    }
}
