using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Data;
using Markel.Pricing.Service.Infrastructure.Interfaces;
using Markel.Pricing.Service.Infrastructure.Models;
using Markel.Pricing.Service.Infrastructure.ResourceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.GlobalRe.Service.Underwriting.BLL.Interfaces
{

    public interface INotesManager : ISearchableManager<BLL_Notes>
    {
        //Added for GR-678
        EntityAction GetEntityActions(BLL_Notes entity);
        EntityResult<IEnumerable<BLL_Notes>> GetNotebyNoteNumber(int noteNumber);
        EntityResult<IEnumerable<BLL_Notes>> GetNotes(int dealNumber);
        EntityResult<BLL_Notes> AddDealNotes(BLL_Notes bLL_DealNotes);
        EntityResult<BLL_Notes> UpdateDealNotes(BLL_Notes bLL_DealNotes);
    }
}
