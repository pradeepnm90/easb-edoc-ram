using Markel.GlobalRe.Service.Underwriting.Data.Models;
using Markel.Pricing.Service.Infrastructure.Data.Interfaces;

namespace Markel.GlobalRe.Service.Underwriting.Data.Interfaces
{
    public interface ITbDealNotesRepository : IGenericRepository<TbDealnote, int> {

        int GetNextNoteNumber();
    }

}

