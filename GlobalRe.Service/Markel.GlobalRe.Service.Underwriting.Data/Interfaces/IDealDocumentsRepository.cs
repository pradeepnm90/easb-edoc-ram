
using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface IDealDocumentsRepository : ISearchRepository<grs_VKeyDocument, int>
    {
        IList<grs_VKeyDocument> GetKeyDocuments(string filenumber, string producer, Boolean isDocTypeStructure);
        Boolean IsDealExists(int filenumber);
        string GetFileType(int filenumber);
    }
}
