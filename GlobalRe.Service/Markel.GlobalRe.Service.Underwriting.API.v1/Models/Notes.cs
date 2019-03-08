using Markel.GlobalRe.Service.Underwriting.API.v1.Helper;
using Markel.GlobalRe.Service.Underwriting.BLL.Models;
using Markel.Pricing.Service.Infrastructure.Models;
using System;

namespace Markel.GlobalRe.Service.Underwriting.API.v1.Models
{
    partial class Notes : BaseApiModel<BLL_Notes>
    {
        public Notes() { }
        public Notes(BLL_Notes model) : base(model) { }

        public override BLL_Notes ToBLLModel()
        {
            BLL_Notes bLL_DealNotes = new BLL_Notes()
            {
                Notenum = Notenum,
                Dealnum = DealNumber,
                Notedate = Notedate,
                Notetype = Notetype,
                Notes = NoteText,
                Whoentered = Whoentered,
                Name = Name,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                CreatedBy = CreatedBy
            };

            return bLL_DealNotes;
        }

        protected override bool HasValue()
        {
            throw new NotImplementedException();
        }

        protected override void Initialize(BLL_Notes model)
        {

            Notenum = model.Notenum;
            DealNumber = model.Dealnum;
            Notedate = model.Notedate;
            Notetype = model.Notetype;
            NoteText = model.Notes;
            Whoentered = model.Whoentered;
            Name = model.Name;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            CreatedBy = model.CreatedBy;
        }
    }
}