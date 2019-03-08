using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;
using Markel.Pricing.Service.Infrastructure.Data;
using System.Collections.Generic;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface INotesRepository : ISearchRepository<grs_VGrsNote, int> {
       IList<grs_VGrsNote> GetNotes(int dealnumber);
        IList<grs_VGrsNote> GetNotebyNoteNumber(int notenumber);
      // IList<grs_VGrsNote> GetNotes(SearchCriteria searchCriteria);

    }
}
